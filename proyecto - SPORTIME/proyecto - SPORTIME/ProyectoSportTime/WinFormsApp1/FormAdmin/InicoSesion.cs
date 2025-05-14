using Newtonsoft.Json;
using Shared.Dtos;
using System.Text;
using WinForm.Form_Home;

namespace WinForm.FormAdmin
{
    public partial class InicoSesion : Form
    {
        public InicoSesion()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void buttonInSc_Click(object sender, EventArgs e)
        {
            LoginDTO login = new LoginDTO
            {
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text,
            };

            HttpClient httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:7094/api/administrador/login", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserializar la respuesta para obtener el ID del admin
                var result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                int adminId = result.adminId;

                // Guardar el ID en la sesión
                SesionAdministrador.IniciarSesion(adminId);

                MessageBox.Show("Inicio de sesión exitoso.");

                var homeForm = new FormInicio();
                homeForm.Show();
                this.Hide(); // Oculta el formulario de inicio de sesión
            }
            else
            {
                MessageBox.Show("Error en el inicio de sesión: " + jsonResponse);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRegistro formRegistro = new FormRegistro();
            formRegistro.Show();
        }
    }
}
