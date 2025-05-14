using System.Net.Http.Json;
using Shared.Entidades;
using System.Data;
using Shared.Dtos;
using System.Text.Json;
using Newtonsoft.Json;
using Negocio.Implementations;
using Negocio.Repositorys;
using WinForm.FormAdmin;
using System.Runtime.CompilerServices;

namespace WinForm.Form_Home
{
    public partial class TurnoForm : Form
    {
        private readonly TurnosLogic _turnosLogic;
        private readonly HttpClient _httpClient;
        private readonly ConsumicionXturnoLogic _consumicionXturnoLogic;

        public TurnoForm()
        {
            InitializeComponent();
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7094/api/") };
            _turnosLogic = new TurnosLogic();
            _consumicionXturnoLogic = new ConsumicionXturnoLogic();
            ActualizarDataGridView();
            textBoxBuscar.TextChanged += textBoxFiltro_TextChanged;
        }

        private async void TurnoForm_Load(object sender, EventArgs e)
        {
            var canchas = await _httpClient.GetFromJsonAsync<List<Canchas>>("Canchas");
            comboBoxCancha.DataSource = canchas;
            comboBoxCancha.DisplayMember = "DisplayName";
            comboBoxCancha.ValueMember = "Cancha_ID";
            comboBoxCancha.SelectedIndex = -1;

            var clientes = await _httpClient.GetFromJsonAsync<List<Clientes>>("clientes");
            comboBoxCliente.DataSource = clientes;
            comboBoxCliente.DisplayMember = "Nombre";
            comboBoxCliente.ValueMember = "Cliente_ID";
            comboBoxCliente.SelectedIndex = -1;

            var productos = await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("productos");
            comboBoxConsumicion.DataSource = productos;
            comboBoxConsumicion.DisplayMember = "Descripcion";
            comboBoxConsumicion.ValueMember = "Producto_ID";
            comboBoxConsumicion.SelectedIndex = -1;
        }

        private async void buttonGuardar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un valor válido en el ComboBox de Cancha
            if (comboBoxCancha.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cancha.");
                return;
            }

            // Verificar si se ha seleccionado un valor válido en el ComboBox de Cliente
            if (comboBoxCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un cliente.");
                return;
            }

            // Verificar si se ha seleccionado un valor válido en el ComboBox de Consumición
            if (comboBoxConsumicion.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto para la consumición.");
                return;
            }

            int canchaId = (int)comboBoxCancha.SelectedValue;
            int clienteId = (int)comboBoxCliente.SelectedValue;
            int productoId = (int)comboBoxConsumicion.SelectedValue;
            int cantidad = (int)numericUpDownCantidad.Value;

            var consumicion = new ConsumicionProductoDTO { Producto_ID = productoId, Cantidad = cantidad };

            var nuevoTurno = new TurnoDTO
            {
                Cancha_ID = canchaId,
                HoraInicio = dateTimePicker1.Value,
                HoraFin = dateTimePicker2.Value,
                Cliente_ID = clienteId,
                ConsumicionProductos = new List<ConsumicionProductoDTO>(), // Para evitar error SQL
                Admin_ID = SesionAdministrador.Admin_ID ?? throw new Exception("No hay administrador autenticado")
            };

            var response = await _turnosLogic.CrearTurno(nuevoTurno);
            MessageBox.Show(response.MensajeError);
            if (response.EsExitoso)
                ActualizarDataGridView();
        }

        private async void buttonModificar_Click(object sender, EventArgs e)
        {
            if (dataGridViewTurnos.SelectedRows.Count > 0 &&
                comboBoxCliente.SelectedValue is int clienteId)
            {
                var filaSeleccionada = dataGridViewTurnos.SelectedRows[0];
                int turnoId = (int)filaSeleccionada.Cells["Turno_ID"].Value;

                var turno = await _turnosLogic.ObtenerTurnoPorId(turnoId);
                if (turno != null)
                {
                    turno.Cancha_ID = (int)comboBoxCancha.SelectedValue;
                    turno.HoraInicio = dateTimePicker1.Value;
                    turno.HoraFin = dateTimePicker2.Value;
                    turno.Cliente_ID = clienteId;


                    await _turnosLogic.ModificarTurno(turnoId, turno);
                    MessageBox.Show("Turno modificado correctamente.");
                    ActualizarDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un turno para modificar.");
            }
        }

        private async void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewTurnos.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dataGridViewTurnos.SelectedRows[0];
                int turnoId = (int)filaSeleccionada.Cells["Turno_ID"].Value;

                var turno = await _turnosLogic.ObtenerTurnoPorId(turnoId);
                if (turno != null)
                {
                    await _turnosLogic.BorrarTurno(turnoId);
                    MessageBox.Show("Turno eliminado correctamente.");
                    ActualizarDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un turno para eliminar.");
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            comboBoxCancha.SelectedIndex = -1;
            comboBoxConsumicion.SelectedIndex = -1;
            comboBoxCliente.SelectedIndex = -1;
            numericUpDownCantidad.Value = 0;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private async void ActualizarDataGridView()
        {
            // Obtener todos los turnos
            var turnos = await _turnosLogic.ObtenerTodosLosTurnos();

            // Obtener todas las canchas (si ya tienes la lista cargada previamente, puedes evitar esto)
            var canchas = await _httpClient.GetFromJsonAsync<List<Canchas>>("Canchas");

            // Obtener todos los clientes
            var clientes = await _httpClient.GetFromJsonAsync<List<Clientes>>("clientes");

            // Asignar la fuente de datos al DataGridView
            dataGridViewTurnos.DataSource = turnos.Select(t => new
            {
                t.Turno_ID,
                Cancha = $"{canchas.FirstOrDefault(c => c.Cancha_ID == t.Cancha_ID)?.DisplayName} - {canchas.FirstOrDefault(c => c.Cancha_ID == t.Cancha_ID)?.Deporte?.Tipo}",
                Cliente = clientes.FirstOrDefault(c => c.Cliente_ID == t.Cliente_ID)?.Nombre,
                t.HoraInicio,
                t.HoraFin
            }).ToList();
        }


        private async void buttonAgregar_Click(object sender, EventArgs e)
        {
            if (dataGridViewTurnos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un turno para agregar la consumición.");
                return;
            }

            var turnoSeleccionado = (dynamic)dataGridViewTurnos.SelectedRows[0].DataBoundItem;
            int turnoId = turnoSeleccionado.Turno_ID;
            int productoId = (int)comboBoxConsumicion.SelectedValue;
            int cantidad = (int)numericUpDownCantidad.Value;

            if (productoId <= 0 || cantidad <= 0)
            {
                MessageBox.Show("Debe seleccionar un producto y una cantidad válida.");
                return;
            }

            var consumicion = new ConsumicionXturnoDTO
            {
                Turno_ID = turnoId,
                Producto_ID = productoId,
                Cantidad = cantidad
            };

            var (esExitoso, mensajeError) = await _consumicionXturnoLogic.AgregarConsumicion(consumicion);
            MessageBox.Show(mensajeError);

            if (esExitoso)
            {
                MessageBox.Show("Producto agregado correctamente.");
            }

        }

        private async void buttonMostrar_Click(object sender, EventArgs e)
        {
            if (dataGridViewTurnos.SelectedRows.Count > 0)
            {
                var filaSeleccionada = dataGridViewTurnos.SelectedRows[0];
                int turnoId = (int)filaSeleccionada.Cells["Turno_ID"].Value;

                try
                {
                    var textoConsumiciones = await _consumicionXturnoLogic.ObtenerTextoConsumiciones(turnoId);
                    MessageBox.Show(textoConsumiciones, "Consumiciones del Turno");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un turno para mostrar las consumiciones.");
            }
        }

        private async void buttonQuitar_Click(object sender, EventArgs e)
        {
            if (dataGridViewTurnos.SelectedRows.Count == 0 || comboBoxConsumicion.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un turno y un producto para quitar.");
                return;
            }

            var turnoSeleccionado = (dynamic)dataGridViewTurnos.SelectedRows[0].DataBoundItem;
            int turnoId = turnoSeleccionado.Turno_ID;
            int productoId = (int)comboBoxConsumicion.SelectedValue;

            try
            {
                await _consumicionXturnoLogic.QuitarConsumicion(turnoId, productoId);
                MessageBox.Show("Producto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la consumición: {ex.Message}");
            }
        }

        private async void textBoxFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = textBoxBuscar.Text.ToLower();

            // Obtener todos los turnos
            var turnos = await _turnosLogic.ObtenerTodosLosTurnos();

            // Obtener todas las canchas
            var canchas = await _httpClient.GetFromJsonAsync<List<Canchas>>("Canchas");

            // Obtener todos los clientes
            var clientes = await _httpClient.GetFromJsonAsync<List<Clientes>>("clientes");

            // Filtrar los turnos
            var turnosFiltrados = turnos.Where(t =>
                // Filtrar por ID de turno
                t.Turno_ID.ToString().Contains(filtro) ||

                // Filtrar por nombre de la cancha
                canchas.Any(c => c.Cancha_ID == t.Cancha_ID && c.DisplayName.ToLower().Contains(filtro)) ||

                // Filtrar por tipo de deporte
                canchas.Any(c => c.Cancha_ID == t.Cancha_ID && c.Deporte.Tipo.ToLower().Contains(filtro)) ||

                // Filtrar por nombre de cliente
                clientes.Any(c => c.Cliente_ID == t.Cliente_ID && c.Nombre.ToLower().Contains(filtro)) ||

                // Filtrar por hora de inicio
                t.HoraInicio.ToString("HH:mm").Contains(filtro)
            ).ToList();

            // Actualizar el DataGridView con los turnos filtrados
            dataGridViewTurnos.DataSource = turnosFiltrados.Select(t => new
            {
                t.Turno_ID,
                Cancha = $"{canchas.FirstOrDefault(c => c.Cancha_ID == t.Cancha_ID)?.DisplayName} - {canchas.FirstOrDefault(c => c.Cancha_ID == t.Cancha_ID)?.Deporte?.Tipo}",
                Cliente = clientes.FirstOrDefault(c => c.Cliente_ID == t.Cliente_ID)?.Nombre,
                t.HoraInicio,
                t.HoraFin
            }).ToList();
        }

        private async void buttonGenerarEstadisticas_Click(object sender, EventArgs e)
        {
            var turnos = await _turnosLogic.ObtenerTodosLosTurnos();

            int totalTurnos = turnos.Count;

            var turnosPorCancha = turnos
                .GroupBy(t => t.Cancha_ID)
                .Select(group => new
                {
                    Cancha_ID = group.Key,
                    TotalTurnos = group.Count()
                }).ToList();

            var turnosPorCliente = turnos
                .GroupBy(t => t.Cliente_ID)
                .Select(group => new
                {
                    Cliente_ID = group.Key,
                    TotalTurnos = group.Count()
                }).ToList();

            double duracionPromedio = turnos.Average(t => (t.HoraFin - t.HoraInicio).TotalMinutes);

            string reporte = $"Reporte de Estadísticas de Turnos\n\n" +
                             $"Total de Turnos: {totalTurnos}\n" +
                             $"Duración Promedio de los Turnos: {duracionPromedio:F2} minutos\n\n" +
                             $"Turnos por Cancha:\n" +
                             string.Join("\n", turnosPorCancha.Select(tc => $"Cancha {tc.Cancha_ID}: {tc.TotalTurnos} turnos")) +
                             "\n\nTurnos por Cliente:\n" +
                             string.Join("\n", turnosPorCliente.Select(tc => $"Cliente {tc.Cliente_ID}: {tc.TotalTurnos} turnos"));

            // Guardar reporte en un archivo de texto
            string filePath = @"C:\Users\mateo\OneDrive\Escritorio\Practica profecional\Reportes\ReporteEstadisticas.txt";
            File.WriteAllText(filePath, reporte);

            MessageBox.Show("El reporte ha sido generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}



