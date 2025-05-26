namespace WinForm.FormAdmin
{
    partial class InicoSesion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InicoSesion));
            label1 = new Label();
            label2 = new Label();
            textBoxEmail = new TextBox();
            label3 = new Label();
            textBoxPassword = new TextBox();
            buttonInSc = new Button();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("MS PGothic", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 30);
            label1.Name = "label1";
            label1.Size = new Size(382, 92);
            label1.TabIndex = 0;
            label1.Text = "Bienvenidos a SportTime";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Font = new Font("MS PGothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(26, 132);
            label2.Name = "label2";
            label2.Size = new Size(87, 22);
            label2.TabIndex = 1;
            label2.Text = "Email";
            // 
            // textBoxEmail
            // 
            textBoxEmail.Location = new Point(26, 157);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(269, 23);
            textBoxEmail.TabIndex = 2;
            // 
            // label3
            // 
            label3.Font = new Font("MS PGothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(26, 220);
            label3.Name = "label3";
            label3.Size = new Size(87, 22);
            label3.TabIndex = 3;
            label3.Text = "Contraseña";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(26, 256);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(269, 23);
            textBoxPassword.TabIndex = 4;
            // 
            // buttonInSc
            // 
            buttonInSc.Font = new Font("MS PGothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonInSc.Location = new Point(51, 326);
            buttonInSc.Name = "buttonInSc";
            buttonInSc.Size = new Size(220, 35);
            buttonInSc.TabIndex = 5;
            buttonInSc.Text = "Iniciar Sesion";
            buttonInSc.UseVisualStyleBackColor = true;
            buttonInSc.Click += buttonInSc_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(400, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 453);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Font = new Font("MS PGothic", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(51, 387);
            button1.Name = "button1";
            button1.Size = new Size(220, 35);
            button1.TabIndex = 6;
            button1.Text = "Registrarse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // InicoSesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Controls.Add(buttonInSc);
            Controls.Add(textBoxPassword);
            Controls.Add(label3);
            Controls.Add(textBoxEmail);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "InicoSesion";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBoxEmail;
        private Label label3;
        private TextBox textBoxPassword;
        private Button buttonInSc;
        private PictureBox pictureBox1;
        private Button button1;
    }
}