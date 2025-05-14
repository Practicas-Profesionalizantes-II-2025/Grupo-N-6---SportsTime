using Negocio.Implementations;
using Negocio.Repositorys;
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


namespace WinForm.Form_Home
{
    public partial class FormCliente : Form
    {
        private readonly ClientesLogic _clientesLogic;
        private List<ClienteDTO> _clientesOriginales = new List<ClienteDTO>();

        public FormCliente()
        {
            InitializeComponent();
            _clientesLogic = new ClientesLogic();
            this.Load += FormCliente_Load;
            textBoxBuscar.TextChanged += textBoxBuscar_TextChanged;
        }

        private async void FormCliente_Load(object sender, EventArgs e)
        {
            try
            {
                _clientesOriginales = await _clientesLogic.ObtenerTodosLosClientes(); // Guarda los clientes originales
                ActualizarDataGridView(_clientesOriginales); // Muestra los clientes en el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonGuardar_Click(object sender, EventArgs e)
        {

            if (!ValidarCampos(out int numeroTelefono)) return;

            var nuevoCliente = new ClienteDTO
            {
                Nombre = textBoxNombre.Text,
                NumeroTelefono = numeroTelefono
            };

            var (success, message) = await _clientesLogic.AltaCliente(nuevoCliente);
            MessageBox.Show(message);

            if (success)
            {
                _clientesOriginales.Add(nuevoCliente);
                ActualizarDataGridView(_clientesOriginales);
            }
            else
            {

            }

        }


        private async void buttonModificar_Click(object sender, EventArgs e)
        {
            // Obtener el cliente seleccionado por su ID
            var clienteId = ObtenerClienteSeleccionado();
            if (clienteId == null) return;

            // Validar campos, y obtener el número de teléfono
            if (!ValidarCampos(out int numeroTelefono)) return;

            // Obtener el cliente actual
            var cliente = await _clientesLogic.ObtenerClientePorId(clienteId.Value);
            if (cliente == null)
            {
                MessageBox.Show("El cliente seleccionado no existe.");
                return;
            }

            // Verificar si el número de teléfono ya existe para otro cliente
            var (success, message) = await _clientesLogic.ValidarTelefonoUnico(clienteId.Value, numeroTelefono);
            if (!success)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si pasa la validación, actualizar los datos del cliente
            cliente.Nombre = textBoxNombre.Text;
            cliente.NumeroTelefono = numeroTelefono;

            try
            {
                // Llamar a la lógica de modificación del cliente
                await _clientesLogic.ModificarCliente(clienteId.Value, cliente);
                MessageBox.Show("Cliente actualizado correctamente.");

                // Actualizar el cliente en la lista original
                var clienteActualizado = _clientesOriginales.FirstOrDefault(c => c.Cliente_ID == clienteId.Value);
                if (clienteActualizado != null)
                {
                    clienteActualizado.Nombre = cliente.Nombre;
                    clienteActualizado.NumeroTelefono = cliente.NumeroTelefono;
                }

                // Actualizar la vista del DataGridView
                ActualizarDataGridView(_clientesOriginales);
            }
            catch (ArgumentException ex)
            {
                // Mostrar error si la modificación falla por validación
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error genérico si algo falla
                MessageBox.Show($"Ocurrió un error al modificar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void buttonEliminar_Click(object sender, EventArgs e)
        {
            var clienteId = ObtenerClienteSeleccionado();
            if (clienteId == null) return;

            var confirmacion = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este cliente?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.No) return;

            try
            {
                await _clientesLogic.BajaCliente(clienteId.Value);
                MessageBox.Show("Cliente eliminado correctamente.");
                // Eliminar el cliente de la lista original
                _clientesOriginales.RemoveAll(c => c.Cliente_ID == clienteId.Value);
                ActualizarDataGridView(_clientesOriginales); // Actualizar la vista del DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el cliente: {ex.Message}");
            }
        }

        private void ActualizarDataGridView(List<ClienteDTO> clientes)
        {
            dataGridView1.DataSource = clientes.Select(c => new
            {
                c.Cliente_ID,
                c.Nombre,
                c.NumeroTelefono
            }).ToList();
        }

        private bool ValidarCampos(out int numeroTelefono)
        {
            numeroTelefono = 0;

            if (string.IsNullOrEmpty(textBoxNombre.Text))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                return false;
            }

            if (!int.TryParse(textBoxTelefono.Text, out numeroTelefono))
            {
                MessageBox.Show("El número de teléfono debe ser un valor numérico válido.");
                return false;
            }

            return true;
        }

        private int? ObtenerClienteSeleccionado()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                return (int)dataGridView1.SelectedRows[0].Cells["Cliente_ID"].Value;
            }

            MessageBox.Show("Seleccione un cliente.");
            return null;
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = textBoxBuscar.Text.Trim().ToLower();  // Trim para evitar espacios en blanco al principio o al final
            if (string.IsNullOrWhiteSpace(filtro))
            {
                ActualizarDataGridView(_clientesOriginales); // Muestra todos los clientes si el filtro está vacío
            }
            else
            {
                // Realiza el filtrado de manera eficiente y con un manejo más preciso de los números
                var clientesFiltrados = _clientesOriginales
                    .Where(c => c.Nombre.ToLower().Contains(filtro) ||
                             c.NumeroTelefono.ToString().Contains(filtro))  // Aquí comprobamos la coincidencia del número
                    .ToList();

                ActualizarDataGridView(clientesFiltrados);
            }
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
