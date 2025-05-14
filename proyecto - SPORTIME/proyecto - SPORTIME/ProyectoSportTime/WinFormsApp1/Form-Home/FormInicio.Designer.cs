namespace WinForm.Form_Home
{
    partial class FormInicio
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
            label1 = new Label();
            label2 = new Label();
            buttonTurno = new Button();
            buttonCanchas = new Button();
            buttonClientes = new Button();
            buttonProductos = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Aqua;
            label1.Font = new Font("MS PGothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, -1);
            label1.Name = "label1";
            label1.Size = new Size(914, 65);
            label1.TabIndex = 0;
            label1.Text = "SportTime";
            label1.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            label2.Location = new Point(0, 557);
            label2.Name = "label2";
            label2.Size = new Size(114, 31);
            label2.TabIndex = 0;
            // 
            // buttonTurno
            // 
            buttonTurno.BackColor = Color.FromArgb(21, 109, 99);
            buttonTurno.FlatStyle = FlatStyle.Popup;
            buttonTurno.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonTurno.Location = new Point(39, 91);
            buttonTurno.Margin = new Padding(3, 4, 3, 4);
            buttonTurno.Name = "buttonTurno";
            buttonTurno.Size = new Size(787, 77);
            buttonTurno.TabIndex = 11;
            buttonTurno.Text = "Turnos";
            buttonTurno.UseVisualStyleBackColor = false;
            buttonTurno.Click += buttonTurno_Click;
            // 
            // buttonCanchas
            // 
            buttonCanchas.BackColor = Color.FromArgb(21, 109, 99);
            buttonCanchas.FlatStyle = FlatStyle.Popup;
            buttonCanchas.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            buttonCanchas.Location = new Point(39, 176);
            buttonCanchas.Margin = new Padding(3, 4, 3, 4);
            buttonCanchas.Name = "buttonCanchas";
            buttonCanchas.Size = new Size(787, 77);
            buttonCanchas.TabIndex = 12;
            buttonCanchas.Text = "Canchas";
            buttonCanchas.UseVisualStyleBackColor = false;
            buttonCanchas.Click += buttonCanchas_Click;
            // 
            // buttonClientes
            // 
            buttonClientes.BackColor = Color.FromArgb(21, 109, 99);
            buttonClientes.FlatStyle = FlatStyle.Popup;
            buttonClientes.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            buttonClientes.Location = new Point(39, 261);
            buttonClientes.Margin = new Padding(3, 4, 3, 4);
            buttonClientes.Name = "buttonClientes";
            buttonClientes.Size = new Size(787, 77);
            buttonClientes.TabIndex = 13;
            buttonClientes.Text = "Clientes";
            buttonClientes.UseVisualStyleBackColor = false;
            buttonClientes.Click += buttonClientes_Click;
            // 
            // buttonProductos
            // 
            buttonProductos.BackColor = Color.FromArgb(21, 109, 99);
            buttonProductos.FlatStyle = FlatStyle.Popup;
            buttonProductos.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            buttonProductos.Location = new Point(39, 347);
            buttonProductos.Margin = new Padding(3, 4, 3, 4);
            buttonProductos.Name = "buttonProductos";
            buttonProductos.Size = new Size(787, 77);
            buttonProductos.TabIndex = 14;
            buttonProductos.Text = "Productos";
            buttonProductos.UseVisualStyleBackColor = false;
            buttonProductos.Click += buttonProductos_Click;
            // 
            // FormInicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 204, 181);
            ClientSize = new Size(914, 439);
            Controls.Add(buttonProductos);
            Controls.Add(buttonClientes);
            Controls.Add(buttonCanchas);
            Controls.Add(buttonTurno);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormInicio";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button buttonTurno;
        private Button buttonCanchas;
        private Button buttonClientes;
        private Button buttonProductos;
    }
}