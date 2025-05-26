using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.Form_Home
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }


        private void AbrirFormulario(Form formulario)
        {
            this.Hide();
            formulario.FormClosed += (s, args) => this.Show(); // Muestra FormInicio cuando el formulario se cierra
            formulario.Show();
        }

        private void buttonTurno_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new TurnoForm());
        }

        private void buttonCanchas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormCanchas());
        }

        private void buttonClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormCliente());
        }

        private void buttonProductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FormProductos());
        }
    }

}

