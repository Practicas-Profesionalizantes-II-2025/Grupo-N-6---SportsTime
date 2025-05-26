namespace WinForm.Form_Home
{
    partial class FormCanchas
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
            comboBoxDeporte = new ComboBox();
            label1 = new Label();
            buttonGuardar = new Button();
            buttonModificar = new Button();
            buttonEliminar = new Button();
            dataGridView1 = new DataGridView();
            buttonVolver = new Button();
            label5 = new Label();
            textBoxBuscar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboBoxDeporte
            // 
            comboBoxDeporte.FormattingEnabled = true;
            comboBoxDeporte.Location = new Point(12, 108);
            comboBoxDeporte.Name = "comboBoxDeporte";
            comboBoxDeporte.Size = new Size(121, 23);
            comboBoxDeporte.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 90);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 2;
            label1.Text = "Deporte ID";
            // 
            // buttonGuardar
            // 
            buttonGuardar.BackColor = Color.FromArgb(21, 109, 99);
            buttonGuardar.FlatStyle = FlatStyle.Popup;
            buttonGuardar.Location = new Point(12, 158);
            buttonGuardar.Name = "buttonGuardar";
            buttonGuardar.Size = new Size(75, 28);
            buttonGuardar.TabIndex = 4;
            buttonGuardar.Text = "Guardar";
            buttonGuardar.UseVisualStyleBackColor = false;
            buttonGuardar.Click += buttonGuardar_Click;
            // 
            // buttonModificar
            // 
            buttonModificar.BackColor = Color.FromArgb(21, 109, 99);
            buttonModificar.FlatStyle = FlatStyle.Popup;
            buttonModificar.Location = new Point(12, 210);
            buttonModificar.Name = "buttonModificar";
            buttonModificar.Size = new Size(75, 28);
            buttonModificar.TabIndex = 5;
            buttonModificar.Text = "Modificar";
            buttonModificar.UseVisualStyleBackColor = false;
            buttonModificar.Click += buttonModificar_Click;
            // 
            // buttonEliminar
            // 
            buttonEliminar.BackColor = Color.FromArgb(21, 109, 99);
            buttonEliminar.FlatStyle = FlatStyle.Popup;
            buttonEliminar.Location = new Point(12, 264);
            buttonEliminar.Name = "buttonEliminar";
            buttonEliminar.Size = new Size(75, 28);
            buttonEliminar.TabIndex = 6;
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.UseVisualStyleBackColor = false;
            buttonEliminar.Click += buttonEliminar_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(153, 108);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(457, 150);
            dataGridView1.TabIndex = 7;
            // 
            // buttonVolver
            // 
            buttonVolver.BackColor = Color.FromArgb(21, 109, 99);
            buttonVolver.FlatStyle = FlatStyle.Popup;
            buttonVolver.Location = new Point(568, 264);
            buttonVolver.Name = "buttonVolver";
            buttonVolver.Size = new Size(75, 30);
            buttonVolver.TabIndex = 8;
            buttonVolver.Text = "Volver";
            buttonVolver.UseVisualStyleBackColor = false;
            buttonVolver.Click += buttonVolver_Click;
            // 
            // label5
            // 
            label5.BackColor = Color.FromArgb(21, 109, 99);
            label5.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ImageAlign = ContentAlignment.BottomLeft;
            label5.Location = new Point(-1, -4);
            label5.Name = "label5";
            label5.Size = new Size(644, 51);
            label5.TabIndex = 20;
            label5.Text = "Canchas";
            label5.TextAlign = ContentAlignment.BottomLeft;
            // 
            // textBoxBuscar
            // 
            textBoxBuscar.Location = new Point(510, 82);
            textBoxBuscar.Name = "textBoxBuscar";
            textBoxBuscar.Size = new Size(100, 23);
            textBoxBuscar.TabIndex = 21;
            // 
            // FormCanchas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(11, 204, 181);
            ClientSize = new Size(643, 306);
            Controls.Add(textBoxBuscar);
            Controls.Add(label5);
            Controls.Add(buttonVolver);
            Controls.Add(dataGridView1);
            Controls.Add(buttonEliminar);
            Controls.Add(buttonModificar);
            Controls.Add(buttonGuardar);
            Controls.Add(label1);
            Controls.Add(comboBoxDeporte);
            Name = "FormCanchas";
            Text = "FormCanchas";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxDeporte;
        private Label label1;
        private Button buttonGuardar;
        private Button buttonModificar;
        private Button buttonEliminar;
        private DataGridView dataGridView1;
        private Button buttonVolver;
        private Label label5;
        private TextBox textBoxBuscar;
    }
}