using Negocio.Implementations;
using Newtonsoft.Json;
using Shared.Dtos;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm.Form_Home
{
    public partial class FormCanchas : Form
    {
        private readonly CanchasLogic _canchasLogic;
        private List<CanchaDTO> _canchasOriginales = new List<CanchaDTO>();

        public FormCanchas()
        {
            InitializeComponent();

            _canchasLogic = new CanchasLogic();
            this.Load += FormCanchas_Load;
            textBoxBuscar.TextChanged += textBoxBuscar_TextChanged; // Evento para búsqueda en tiempo real
        }

        private async void FormCanchas_Load(object sender, EventArgs e)
        {
            await ActualizarDataGridView();

            try
            {
                DeportesLogic deportesLogic = new DeportesLogic();

                var deportes = await deportesLogic.ObtenerTodosLosDeportes();

                comboBoxDeporte.DataSource = deportes;
                comboBoxDeporte.DisplayMember = "Tipo";  // Mostrar el nombre o tipo del deporte
                comboBoxDeporte.ValueMember = "Deporte_ID";  // Usar el ID del deporte como valor
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error si falla la carga de deportes
                MessageBox.Show($"Error al cargar los deportes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ActualizarDataGridView()
        {
            _canchasOriginales = await _canchasLogic.ObtenerTodasLasCanchas(); // Guardamos copia original
            MostrarCanchas(_canchasOriginales); // Mostramos las canchas en el DataGridView
        }

        private void MostrarCanchas(List<CanchaDTO> canchas)
        {
            dataGridView1.DataSource = canchas.Select(c => new
            {
                c.Cancha_ID,
                c.Deporte_ID,
                c.Tipo
            }).ToList();
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = textBoxBuscar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                MostrarCanchas(_canchasOriginales); // Mostrar todas si el filtro está vacío
            }
            else
            {
                var canchasFiltradas = _canchasOriginales
                    .Where(c => c.Tipo.ToLower().Contains(filtro) ||
                                c.Cancha_ID.ToString().Contains(filtro))
                    .ToList();

                MostrarCanchas(canchasFiltradas);
            }
        }

        private async void buttonGuardar_Click(object sender, EventArgs e)
        {
            var nuevaCancha = new CanchaDTO
            {
                Deporte_ID = (int)comboBoxDeporte.SelectedValue
            };

            try
            {

                await _canchasLogic.AltaCancha(nuevaCancha);
                MessageBox.Show("Cancha guardada correctamente.");


                await ActualizarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la cancha: {ex.Message}");
            }
        }

        private async void buttonModificar_Click(object sender, EventArgs e)
        {
            // Obtener el ID de la cancha seleccionada desde el DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una cancha para modificar.");
                return;
            }

            int canchaID = (int)dataGridView1.SelectedRows[0].Cells["Cancha_ID"].Value;

            // Crear un objeto CanchaDTO con los valores actualizados
            var canchaModificada = new CanchaDTO
            {
                Deporte_ID = (int)comboBoxDeporte.SelectedValue // Puedes agregar otros campos si los tienes
            };

            try
            {
                // Llamar al método de lógica para modificar la cancha
                await _canchasLogic.ModificarCancha(canchaID, canchaModificada);

                // Mostrar mensaje de éxito
                MessageBox.Show("Cancha modificada correctamente.");

                // Actualizar el DataGridView si es necesario
                await ActualizarDataGridView();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MessageBox.Show($"Error al modificar la cancha: {ex.Message}");
            }
        }

        private async void buttonEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el ID de la cancha seleccionada desde el DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione una cancha para eliminar.");
                return;
            }

            int canchaID = (int)dataGridView1.SelectedRows[0].Cells["Cancha_ID"].Value;

            // Confirmación de eliminación
            var confirmacion = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta cancha?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.No) return;

            try
            {
                // Llamar al método de lógica para eliminar la cancha
                await _canchasLogic.BajaCancha(canchaID);

                // Mostrar mensaje de éxito
                MessageBox.Show("Cancha eliminada correctamente.");

                // Actualizar el DataGridView si es necesario
                await ActualizarDataGridView();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MessageBox.Show($"Error al eliminar la cancha: {ex.Message}");
            }
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


