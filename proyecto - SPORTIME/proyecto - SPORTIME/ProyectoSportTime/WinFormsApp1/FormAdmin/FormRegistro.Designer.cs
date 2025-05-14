namespace WinForm.FormAdmin
{
    partial class FormRegistro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegistro));
            label1 = new Label();
            textBoxCorreo = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBoxContraseña = new TextBox();
            buttonRegistrar = new Button();
            buttonCancelar = new Button();
            pictureBox1 = new PictureBox();
            textBoxNombre = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 156);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 0;
            label1.Text = "Correo electronico";
            // 
            // textBoxCorreo
            // 
            textBoxCorreo.Location = new Point(12, 174);
            textBoxCorreo.Name = "textBoxCorreo";
            textBoxCorreo.Size = new Size(157, 23);
            textBoxCorreo.TabIndex = 1;
            // 
            // label2
            // 
            label2.Font = new Font("MS PGothic", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(3, 9);
            label2.Name = "label2";
            label2.Size = new Size(437, 92);
            label2.TabIndex = 2;
            label2.Text = "Registraro de administrador";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 216);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 3;
            label3.Text = "Contraseña";
            // 
            // textBoxContraseña
            // 
            textBoxContraseña.Location = new Point(12, 234);
            textBoxContraseña.Name = "textBoxContraseña";
            textBoxContraseña.Size = new Size(157, 23);
            textBoxContraseña.TabIndex = 4;
            // 
            // buttonRegistrar
            // 
            buttonRegistrar.Location = new Point(12, 313);
            buttonRegistrar.Name = "buttonRegistrar";
            buttonRegistrar.Size = new Size(75, 23);
            buttonRegistrar.TabIndex = 5;
            buttonRegistrar.Text = "Guardar";
            buttonRegistrar.UseVisualStyleBackColor = true;
            buttonRegistrar.Click += buttonRegistrar_Click;
            // 
            // buttonCancelar
            // 
            buttonCancelar.Location = new Point(162, 313);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(75, 23);
            buttonCancelar.TabIndex = 6;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = true;
            buttonCancelar.Click += buttonCancelar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(429, -39);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 453);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // textBoxNombre
            // 
            textBoxNombre.Location = new Point(12, 120);
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(157, 23);
            textBoxNombre.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 102);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 9;
            label4.Text = "Nombre";
            // 
            // FormRegistro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(803, 345);
            Controls.Add(label4);
            Controls.Add(textBoxNombre);
            Controls.Add(pictureBox1);
            Controls.Add(buttonCancelar);
            Controls.Add(buttonRegistrar);
            Controls.Add(textBoxContraseña);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBoxCorreo);
            Controls.Add(label1);
            Name = "FormRegistro";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxCorreo;
        private Label label2;
        private Label label3;
        private TextBox textBoxContraseña;
        private Button buttonRegistrar;
        private Button buttonCancelar;
        private PictureBox pictureBox1;
        private TextBox textBoxNombre;
        private Label label4;
    }
}