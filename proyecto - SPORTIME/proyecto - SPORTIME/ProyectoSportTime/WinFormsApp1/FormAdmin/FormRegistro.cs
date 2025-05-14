using Negocio.Implementations;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.FormAdmin
{
    public partial class FormRegistro : Form
    {
        public FormRegistro()
        {
            InitializeComponent();
            textBoxContraseña.UseSystemPasswordChar = true;
        }

        private async void buttonRegistrar_Click(object sender, EventArgs e)
        {
            var adminDTO = new AdministradorDTO
            {
                Nombre = textBoxNombre.Text,
                Email = textBoxCorreo.Text, 
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(textBoxContraseña.Text), 
                IsSuperAdmin = true 
            };

            AdministradorLogic administradorLogic = new AdministradorLogic();

            var (success, message) = await administradorLogic.AltaAdministrador(adminDTO);

            if (success)
            {
                MessageBox.Show("Administrador registrado correctamente");
               
            }
            else
            {
                MessageBox.Show($"Error: {message}");
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
