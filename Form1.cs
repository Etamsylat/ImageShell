using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading;



namespace ImageShell
{
    public partial class Form1 : Form
    {

        Bitmap baseImage, imageClone;
        public List<Color> pixelList = new List<Color>();
        public byte[] pixels;
        public int width, height;
        public List<int> seeds = new List<int>();
        public List<int> strengths = new List<int>();
        bool isWorking;
        bool isPlaceholderTextDisplayed = true;


        public Form1()
        {
            InitializeComponent();
        }

        private void selectImageBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                baseImage = new Bitmap(ofd.FileName);
                imageBox.Image = baseImage;
                imageClone = (Bitmap)baseImage.Clone();
                Thread thread = new Thread(() => ImageToList(imageClone));
                thread.Start();
            }
            

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "BMP(*.bmp)|*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                baseImage.Save(ofd.FileName, ImageFormat.Png);
            }
        }


        private void shuffleBtn_Click(object sender, EventArgs e)
        {
            if (ReadSequence(paramsTxt.Text) && !isWorking)
            {
                isWorking = true;
                Thread thread = new Thread(shuffleBtnn);
                thread.Start();
            }

        }

        private void shuffleBtnn()
        {


            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < seeds.Count; i++)
            {
                pixels = altShuffle(seeds[i], strengths[i], pixels);
            }

            PixelsToBitmap();
            Debug.WriteLine("Shuffle: " + sw.ElapsedMilliseconds);
            isWorking = false;
        }



        private void unshuffleBtn_Click(object sender, EventArgs e)
        {
            imageBox.Image = baseImage;
            if (ReadSequence(paramsTxt.Text) && !isWorking)
            {
                isWorking = true;
                Thread thread = new Thread(unshuffleBtnn);
                thread.Start();
            }
        }

        private void unshuffleBtnn()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = seeds.Count - 1; i >= 0; i--)
            {
                pixels = altUnshuffle(seeds[i], strengths[i], pixels);
            }

            PixelsToBitmap();
            Debug.WriteLine("Unshuffle: " + sw.ElapsedMilliseconds);
            isWorking = false;
        }



        private void ImageToList(Bitmap bitmap)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            isWorking = true;

            width = bitmap.Width;
            height = bitmap.Height;

            // Lock the original bitmap's bits for reading
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb
            );

            int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * bitmap.Height;
            pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;

            // Copy the pixel data from the original bitmap into the byte array
            System.Runtime.InteropServices.Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);

            Debug.WriteLine(pixels[1231].ToString());

            // Unlock the original bitmap
            bitmap.UnlockBits(bitmapData);
            Debug.WriteLine("ImageToList: " + sw.ElapsedMilliseconds.ToString());
            isWorking = false;
        }

        /*private byte[] Shuffle(int seed, int strenght, byte[] bytes)
        {
            for (int n = 0; n < strenght; n++)
            {
                Random rng = new Random(seed + n);
                int[] index = new int[bytes.Length];
                for (int i = 0; i < bytes.Length; i++)
                {
                    index[i] = rng.Next(bytes.Length - 1);
                }

                for (int i = 0; i < index.Length; i++)
                {
                    byte holder = bytes[i];
                    bytes[i] = bytes[index[i]];
                    bytes[index[i]] = holder;
                }
            }
            return bytes;
        }*/

        private byte[] altShuffle(int seed, int strenght, byte[] bytes)
        {
            for (int n = 0; n < strenght; n++)
            {
                Random rng = new Random(seed + n);


                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = (byte)((bytes[i] + rng.Next(256)) % 256);
                }
            }
            return bytes;
        }



        /*private byte[] Unshuffle(int seed, int strenght, byte[] bytes)
        {
            for (int n = strenght - 1; n >= 0; n--)
            {
                Random rng = new Random(seed + n);
                int[] index = new int[bytes.Length];

                for (int i = 0; i < bytes.Length; i++)
                {
                    index[i] = rng.Next(bytes.Length - 1);
                }

                for (int i = index.Length - 1; i >= 0; i--)
                {
                    byte holder = bytes[i];
                    bytes[i] = bytes[index[i]];
                    bytes[index[i]] = holder;
                }
            }
            return bytes;
        }*/

        private byte[] altUnshuffle(int seed, int strenght, byte[] bytes)
        {
            for (int n = strenght - 1; n >= 0; n--)
            {
                Random rng = new Random(seed + n);


                for (int i = 0; i < bytes.Length; i++)
                {
                    int holder = (bytes[i] - rng.Next(256)) % 256;
                    if (holder < 0)
                    {
                        holder += 256;
                    }

                    bytes[i] = (byte)(holder);
                }
            }
            return bytes;
        }


        private void PixelsToBitmap()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();


            Bitmap newBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            // Lock the bitmap's bits
            BitmapData bmpData = newBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newBitmap.PixelFormat);

            // Copy the byte array into the bitmap
            IntPtr ptr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, pixels.Length);

            // Unlock the bits
            newBitmap.UnlockBits(bmpData);

            // Optional: Display the new bitmap (for example, in a PictureBox)
            // Replace 'imageBox' with your PictureBox control's name
            if (imageBox.InvokeRequired)
            {
                baseImage = newBitmap;
                imageBox.Invoke(new Action(() => imageBox.Image = baseImage));
            }
            else
            {
                baseImage = newBitmap;
                imageBox.Image = newBitmap;
            }
            Debug.WriteLine("ToBitmap: " + sw.ElapsedMilliseconds);
        }

        private bool ReadSequence(string sequence)
        {
            seeds.Clear();
            strengths.Clear();
            string[] substrings = sequence.Split(';');



            if (substrings.Length % 2 == 1 || substrings.Length == 0)
            {
                MessageBox.Show($"Invalid sequence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            else
            {

                for (int i = 0; i < substrings.Length; i++)
                {
                    if (!int.TryParse(substrings[i], out int result))
                    {

                        seeds.Clear();
                        strengths.Clear();
                        MessageBox.Show($"Invalid sequence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        if (i % 2 == 1)
                        {
                            if (int.Parse(substrings[i]) > 0)
                            {
                                strengths.Add(int.Parse(substrings[i]));
                            }
                            else
                            {

                                seeds.Clear();
                                strengths.Clear();
                                MessageBox.Show($"Invalid encryption level in sequence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }

                        }
                        else
                        {
                            seeds.Add(int.Parse(substrings[i]));

                        }
                    }
                }
            }

            return true;
        }




        static Bitmap ScaleImage(Bitmap bmp)
        {
            var ratioX = (double)600 / bmp.Width;
            var ratioY = (double)450 / bmp.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(bmp.Width * ratio);
            var newHeight = (int)(bmp.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(bmp, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private void paramsTxt_Enter(object sender, EventArgs e)
        {
            if (isPlaceholderTextDisplayed)
            {
                paramsTxt.Text = "";
                paramsTxt.ForeColor = SystemColors.WindowText; // or any other color you prefer
                isPlaceholderTextDisplayed = false;
            }
        }

        private void paramsTxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(paramsTxt.Text))
            {
                paramsTxt.Text = "Enter string...";
                paramsTxt.ForeColor = Color.Gray;
                isPlaceholderTextDisplayed = true;
            }
        }
    }
}
