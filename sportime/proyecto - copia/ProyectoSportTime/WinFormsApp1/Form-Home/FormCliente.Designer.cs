namespace WinForm.Form_Home
{
    partial class FormCliente
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
            textBoxNombre = new TextBox();
            label2 = new Label();
            textBoxTelefono = new TextBox();
            buttonGuardar = new Button();
            dataGridView1 = new DataGridView();
            buttonModificar = new Button();
            buttonEliminar = new Button();
            buttonVolver = new Button();
            label5 = new Label();
            textBoxBuscar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 159);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre";
            // 
            // textBoxNombre
            // 
            textBoxNombre.Location = new Point(12, 177);
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(100, 23);
            textBoxNombre.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 214);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 2;
            label2.Text = "Telefono";
            // 
            // textBoxTelefono
            // 
            textBoxTelefono.Location = new Point(12, 232);
            textBoxTelefono.Name = "textBoxTelefono";
            textBoxTelefono.Size = new Size(100, 23);
            textBoxTelefono.TabIndex = 3;
            // 
            // buttonGuardar
            // 
            buttonGuardar.BackColor = Color.FromArgb(21, 109, 99);
            buttonGuardar.FlatStyle = FlatStyle.Popup;
            buttonGuardar.Location = new Point(12, 270);
            buttonGuardar.Name = "buttonGuardar";
            buttonGuardar.Size = new Size(75, 23);
            buttonGuardar.TabIndex = 4;
            buttonGuardar.Text = "Guardar";
            buttonGuardar.UseVisualStyleBackColor = false;
            buttonGuardar.Click += buttonGuardar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(173, 103);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(336, 161);
            dataGridView1.TabIndex = 5;
            // 
            // buttonModificar
            // 
            buttonModificar.BackColor = Color.FromArgb(21, 109, 99);
            buttonModificar.FlatStyle = FlatStyle.Popup;
            buttonModificar.Location = new Point(173, 270);
            buttonModificar.Name = "buttonModificar";
            buttonModificar.Size = new Size(75, 23);
            buttonModificar.TabIndex = 6;
            buttonModificar.Text = "Modificar";
            buttonModificar.UseVisualStyleBackColor = false;
            buttonModificar.Click += buttonModificar_Click;
            // 
            // buttonEliminar
            // 
            buttonEliminar.BackColor = Color.FromArgb(21, 109, 99);
            buttonEliminar.FlatStyle = FlatStyle.Popup;
            buttonEliminar.Location = new Point(434, 270);
            buttonEliminar.Name = "buttonEliminar";
            buttonEliminar.Size = new Size(75, 23);
            buttonEliminar.TabIndex = 7;
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.UseVisualStyleBackColor = false;
            buttonEliminar.Click += buttonEliminar_Click;
            // 
            // buttonVolver
            // 
            buttonVolver.BackColor = Color.FromArgb(21, 109, 99);
            buttonVolver.FlatStyle = FlatStyle.Popup;
            buttonVolver.Location = new Point(581, 312);
            buttonVolver.Name = "buttonVolver";
            buttonVolver.Size = new Size(75, 23);
            buttonVolver.TabIndex = 8;
            buttonVolver.TabStop = false;
            buttonVolver.Text = "Volver";
            buttonVolver.UseVisualStyleBackColor = false;
            buttonVolver.Click += buttonVolver_Click;
            // 
            // label5
            // 
            label5.BackColor = Color.FromArgb(21, 109, 99);
            label5.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ImageAlign = ContentAlignment.BottomLeft;
            label5.Location = new Point(0, -1);
            label5.Name = "label5";
            label5.Size = new Size(610, 49);
            label5.TabIndex = 20;
            label5.Text = "Clientes";
            label5.TextAlign = ContentAlignment.BottomLeft;
            // 
            // textBoxBuscar
            // 
            textBoxBuscar.Location = new Point(547, 84);
            textBoxBuscar.Name = "textBoxBuscar";
            textBoxBuscar.Size = new Size(100, 23);
            textBoxBuscar.TabIndex = 21;
            // 
            // FormCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 204, 181);
            ClientSize = new Size(668, 347);
            Controls.Add(textBoxBuscar);
            Controls.Add(label5);
            Controls.Add(buttonVolver);
            Controls.Add(buttonEliminar);
            Controls.Add(buttonModificar);
            Controls.Add(dataGridView1);
            Controls.Add(buttonGuardar);
            Controls.Add(textBoxTelefono);
            Controls.Add(label2);
            Controls.Add(textBoxNombre);
            Controls.Add(label1);
            Name = "FormCliente";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxNombre;
        private Label label2;
        private TextBox textBoxTelefono;
        private Button buttonGuardar;
        private DataGridView dataGridView1;
        private Button buttonModificar;
        private Button buttonEliminar;
        private Button buttonVolver;
        private Label label5;
        private TextBox textBoxBuscar;
    }
}