namespace WinForm.Form_Home
{
    partial class TurnoForm
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
            dataGridViewTurnos = new DataGridView();
            comboBoxCancha = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            buttonGuardar = new Button();
            buttonModificar = new Button();
            buttonEliminar = new Button();
            buttonLimpiar = new Button();
            buttonVolver = new Button();
            comboBoxCliente = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            comboBoxConsumicion = new ComboBox();
            label2 = new Label();
            numericUpDownCantidad = new NumericUpDown();
            buttonAgregar = new Button();
            buttonQuitar = new Button();
            buttonMostrar = new Button();
            textBoxBuscar = new TextBox();
            label7 = new Label();
            buttonGenerarEstadisticas = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTurnos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidad).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewTurnos
            // 
            dataGridViewTurnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTurnos.Location = new Point(41, 98);
            dataGridViewTurnos.Name = "dataGridViewTurnos";
            dataGridViewTurnos.Size = new Size(653, 210);
            dataGridViewTurnos.TabIndex = 0;
            // 
            // comboBoxCancha
            // 
            comboBoxCancha.FormattingEnabled = true;
            comboBoxCancha.Location = new Point(140, 348);
            comboBoxCancha.Name = "comboBoxCancha";
            comboBoxCancha.Size = new Size(121, 23);
            comboBoxCancha.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "HH:mm";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(568, 348);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Size = new Size(98, 23);
            dateTimePicker1.TabIndex = 4;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(672, 348);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.Size = new Size(93, 23);
            dateTimePicker2.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(141, 333);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 6;
            label1.Text = "Cancha";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(568, 333);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 8;
            label3.Text = "Inicio";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(672, 333);
            label4.Name = "label4";
            label4.Size = new Size(23, 15);
            label4.TabIndex = 9;
            label4.Text = "Fin";
            // 
            // buttonGuardar
            // 
            buttonGuardar.BackColor = Color.FromArgb(21, 109, 99);
            buttonGuardar.FlatStyle = FlatStyle.Popup;
            buttonGuardar.ForeColor = Color.Black;
            buttonGuardar.Location = new Point(24, 391);
            buttonGuardar.Margin = new Padding(0);
            buttonGuardar.Name = "buttonGuardar";
            buttonGuardar.Size = new Size(75, 23);
            buttonGuardar.TabIndex = 10;
            buttonGuardar.Text = "Guardar";
            buttonGuardar.UseVisualStyleBackColor = false;
            buttonGuardar.Click += buttonGuardar_Click;
            // 
            // buttonModificar
            // 
            buttonModificar.BackColor = Color.FromArgb(21, 109, 99);
            buttonModificar.FlatStyle = FlatStyle.Popup;
            buttonModificar.Location = new Point(113, 391);
            buttonModificar.Name = "buttonModificar";
            buttonModificar.Size = new Size(75, 23);
            buttonModificar.TabIndex = 11;
            buttonModificar.Text = "Modificar";
            buttonModificar.UseVisualStyleBackColor = false;
            buttonModificar.Click += buttonModificar_Click;
            // 
            // buttonEliminar
            // 
            buttonEliminar.BackColor = Color.FromArgb(21, 109, 99);
            buttonEliminar.FlatStyle = FlatStyle.Popup;
            buttonEliminar.Location = new Point(203, 391);
            buttonEliminar.Name = "buttonEliminar";
            buttonEliminar.Size = new Size(75, 23);
            buttonEliminar.TabIndex = 12;
            buttonEliminar.Text = "Eliminar";
            buttonEliminar.UseVisualStyleBackColor = false;
            buttonEliminar.Click += buttonEliminar_Click;
            // 
            // buttonLimpiar
            // 
            buttonLimpiar.BackColor = Color.FromArgb(21, 109, 99);
            buttonLimpiar.FlatStyle = FlatStyle.Popup;
            buttonLimpiar.Location = new Point(715, 391);
            buttonLimpiar.Name = "buttonLimpiar";
            buttonLimpiar.Size = new Size(75, 23);
            buttonLimpiar.TabIndex = 13;
            buttonLimpiar.Text = "Limpiar";
            buttonLimpiar.UseVisualStyleBackColor = false;
            buttonLimpiar.Click += buttonLimpiar_Click;
            // 
            // buttonVolver
            // 
            buttonVolver.BackColor = Color.FromArgb(21, 109, 99);
            buttonVolver.FlatStyle = FlatStyle.Popup;
            buttonVolver.Location = new Point(796, 391);
            buttonVolver.Name = "buttonVolver";
            buttonVolver.Size = new Size(75, 23);
            buttonVolver.TabIndex = 14;
            buttonVolver.Text = "Volver";
            buttonVolver.UseVisualStyleBackColor = false;
            buttonVolver.Click += buttonVolver_Click;
            // 
            // comboBoxCliente
            // 
            comboBoxCliente.FormattingEnabled = true;
            comboBoxCliente.Location = new Point(12, 348);
            comboBoxCliente.Name = "comboBoxCliente";
            comboBoxCliente.Size = new Size(110, 23);
            comboBoxCliente.TabIndex = 17;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 333);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 18;
            label6.Text = "Cliente";
            // 
            // label5
            // 
            label5.BackColor = Color.FromArgb(21, 109, 99);
            label5.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(41, 9);
            label5.Name = "label5";
            label5.Size = new Size(653, 75);
            label5.TabIndex = 19;
            label5.Text = "Turnos";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // comboBoxConsumicion
            // 
            comboBoxConsumicion.FormattingEnabled = true;
            comboBoxConsumicion.Location = new Point(293, 348);
            comboBoxConsumicion.Name = "comboBoxConsumicion";
            comboBoxConsumicion.Size = new Size(121, 23);
            comboBoxConsumicion.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(293, 333);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 21;
            label2.Text = "Producto";
            // 
            // numericUpDownCantidad
            // 
            numericUpDownCantidad.Location = new Point(420, 348);
            numericUpDownCantidad.Name = "numericUpDownCantidad";
            numericUpDownCantidad.Size = new Size(33, 23);
            numericUpDownCantidad.TabIndex = 22;
            // 
            // buttonAgregar
            // 
            buttonAgregar.BackColor = Color.FromArgb(21, 109, 99);
            buttonAgregar.FlatStyle = FlatStyle.Popup;
            buttonAgregar.Location = new Point(470, 347);
            buttonAgregar.Name = "buttonAgregar";
            buttonAgregar.Size = new Size(75, 23);
            buttonAgregar.TabIndex = 23;
            buttonAgregar.Text = "Agregar";
            buttonAgregar.UseVisualStyleBackColor = false;
            buttonAgregar.Click += buttonAgregar_Click;
            // 
            // buttonQuitar
            // 
            buttonQuitar.BackColor = Color.FromArgb(21, 109, 99);
            buttonQuitar.FlatStyle = FlatStyle.Popup;
            buttonQuitar.Location = new Point(470, 391);
            buttonQuitar.Name = "buttonQuitar";
            buttonQuitar.Size = new Size(75, 23);
            buttonQuitar.TabIndex = 24;
            buttonQuitar.Text = "Quitar";
            buttonQuitar.UseVisualStyleBackColor = false;
            buttonQuitar.Click += buttonQuitar_Click;
            // 
            // buttonMostrar
            // 
            buttonMostrar.BackColor = Color.FromArgb(21, 109, 99);
            buttonMostrar.FlatStyle = FlatStyle.Popup;
            buttonMostrar.Location = new Point(360, 391);
            buttonMostrar.Name = "buttonMostrar";
            buttonMostrar.Size = new Size(93, 23);
            buttonMostrar.TabIndex = 25;
            buttonMostrar.Text = "Mostrar";
            buttonMostrar.UseVisualStyleBackColor = false;
            buttonMostrar.Click += buttonMostrar_Click;
            // 
            // textBoxBuscar
            // 
            textBoxBuscar.Location = new Point(700, 116);
            textBoxBuscar.Name = "textBoxBuscar";
            textBoxBuscar.Size = new Size(126, 23);
            textBoxBuscar.TabIndex = 26;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(700, 98);
            label7.Name = "label7";
            label7.Size = new Size(42, 15);
            label7.TabIndex = 27;
            label7.Text = "Buscar";
            // 
            // buttonGenerarEstadisticas
            // 
            buttonGenerarEstadisticas.BackColor = Color.FromArgb(21, 109, 99);
            buttonGenerarEstadisticas.FlatStyle = FlatStyle.Popup;
            buttonGenerarEstadisticas.Location = new Point(700, 159);
            buttonGenerarEstadisticas.Name = "buttonGenerarEstadisticas";
            buttonGenerarEstadisticas.Size = new Size(75, 23);
            buttonGenerarEstadisticas.TabIndex = 28;
            buttonGenerarEstadisticas.Text = "Estadisticas";
            buttonGenerarEstadisticas.UseVisualStyleBackColor = false;
            buttonGenerarEstadisticas.Click += buttonGenerarEstadisticas_Click;
            // 
            // TurnoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(11, 204, 181);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(877, 450);
            Controls.Add(buttonGenerarEstadisticas);
            Controls.Add(label7);
            Controls.Add(textBoxBuscar);
            Controls.Add(buttonMostrar);
            Controls.Add(buttonQuitar);
            Controls.Add(buttonAgregar);
            Controls.Add(numericUpDownCantidad);
            Controls.Add(label2);
            Controls.Add(comboBoxConsumicion);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(comboBoxCliente);
            Controls.Add(buttonVolver);
            Controls.Add(buttonLimpiar);
            Controls.Add(buttonEliminar);
            Controls.Add(buttonModificar);
            Controls.Add(buttonGuardar);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(comboBoxCancha);
            Controls.Add(dataGridViewTurnos);
            ForeColor = SystemColors.WindowText;
            Name = "TurnoForm";
            Padding = new Padding(3);
            Text = "TurnoForm";
            TopMost = true;
            Load += TurnoForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewTurnos).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewTurnos;
        private ComboBox comboBoxCancha;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private Label label1;
        private Label label3;
        private Label label4;
        private Button buttonGuardar;
        private Button buttonModificar;
        private Button buttonEliminar;
        private Button buttonLimpiar;
        private Button buttonVolver;
        private ComboBox comboBoxCliente;
        private Label label6;
        private Label label5;
        private ComboBox comboBoxConsumicion;
        private Label label2;
        private NumericUpDown numericUpDownCantidad;
        private Button buttonAgregar;
        private Button buttonQuitar;
        private Button buttonMostrar;
        private TextBox textBoxBuscar;
        private Label label7;
        private Button buttonGenerarEstadisticas;
    }
}