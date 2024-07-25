namespace ImageShell
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            imageBox = new PictureBox();
            selectImageBtn = new Button();
            unshuffleBtn = new Button();
            shuffleBtn = new Button();
            paramsTxt = new TextBox();
            saveBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)imageBox).BeginInit();
            SuspendLayout();
            // 
            // imageBox
            // 
            imageBox.BackColor = Color.Transparent;
            imageBox.BackgroundImageLayout = ImageLayout.Zoom;
            imageBox.Image = Properties.Resources.tutel;
            imageBox.Location = new Point(222, 6);
            imageBox.Name = "imageBox";
            imageBox.Size = new Size(600, 450);
            imageBox.SizeMode = PictureBoxSizeMode.Zoom;
            imageBox.TabIndex = 0;
            imageBox.TabStop = false;
            // 
            // selectImageBtn
            // 
            selectImageBtn.BackgroundImage = Properties.Resources.button;
            selectImageBtn.Location = new Point(31, 61);
            selectImageBtn.Name = "selectImageBtn";
            selectImageBtn.Size = new Size(120, 40);
            selectImageBtn.TabIndex = 1;
            selectImageBtn.Text = "Load image";
            selectImageBtn.UseVisualStyleBackColor = true;
            selectImageBtn.Click += selectImageBtn_Click;
            // 
            // unshuffleBtn
            // 
            unshuffleBtn.BackgroundImage = Properties.Resources.button;
            unshuffleBtn.Location = new Point(31, 153);
            unshuffleBtn.Name = "unshuffleBtn";
            unshuffleBtn.Size = new Size(120, 40);
            unshuffleBtn.TabIndex = 2;
            unshuffleBtn.Text = "Decrypt";
            unshuffleBtn.UseVisualStyleBackColor = true;
            unshuffleBtn.Click += unshuffleBtn_Click;
            // 
            // shuffleBtn
            // 
            shuffleBtn.BackgroundImage = Properties.Resources.button;
            shuffleBtn.Location = new Point(31, 107);
            shuffleBtn.Name = "shuffleBtn";
            shuffleBtn.Size = new Size(120, 40);
            shuffleBtn.TabIndex = 3;
            shuffleBtn.Text = "Encrypt";
            shuffleBtn.UseVisualStyleBackColor = true;
            shuffleBtn.Click += shuffleBtn_Click;
            // 
            // paramsTxt
            // 
            paramsTxt.ForeColor = Color.Gray;
            paramsTxt.Location = new Point(31, 279);
            paramsTxt.Name = "paramsTxt";
            paramsTxt.Size = new Size(100, 23);
            paramsTxt.TabIndex = 4;
            paramsTxt.Text = "Enter string...";
            paramsTxt.Enter += paramsTxt_Enter;
            paramsTxt.Leave += paramsTxt_Leave;
            // 
            // saveBtn
            // 
            saveBtn.BackgroundImage = Properties.Resources.button;
            saveBtn.Location = new Point(31, 199);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(120, 40);
            saveBtn.TabIndex = 5;
            saveBtn.Text = "Save to file";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += saveBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg;
            ClientSize = new Size(834, 461);
            Controls.Add(saveBtn);
            Controls.Add(paramsTxt);
            Controls.Add(shuffleBtn);
            Controls.Add(unshuffleBtn);
            Controls.Add(selectImageBtn);
            Controls.Add(imageBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)imageBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imageBox;
        private Button selectImageBtn;
        private Button unshuffleBtn;
        private Button shuffleBtn;
        private TextBox paramsTxt;
        private Button saveBtn;
    }
}
