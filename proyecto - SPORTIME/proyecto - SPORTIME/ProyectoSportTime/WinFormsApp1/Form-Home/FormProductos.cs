using Negocio.Implementations;
using Negocio.Repositorys;
using Shared.Dtos;
using Shared.Entidades;
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
    public partial class FormProductos : Form
    {
        private readonly ProductosLogic _productosLogic;
        private List<ProductoDTO> _productosOriginales = new List<ProductoDTO>();
        private List<ProveedorDTO> _proveedores = new List<ProveedorDTO>();

        public FormProductos()
        {
            InitializeComponent();
            _productosLogic = new ProductosLogic();

            // Evento para la búsqueda en tiempo real
            textBoxBuscar.TextChanged += textBoxBuscar_TextChanged;
        }

        private async void FormProductos_Load(object sender, EventArgs e)
        {
            try
            {
                _proveedores = await ObtenerProveedores(); // Carga los proveedores
                comboBoxProveedor.DataSource = _proveedores;
                comboBoxProveedor.DisplayMember = "Nombre"; // Mostrar el nombre del proveedor
                comboBoxProveedor.ValueMember = "Proveedor_ID"; // Valor asociado con el proveedor
                comboBoxProveedor.SelectedIndex = -1;

                // Cargar los productos
                _productosOriginales = await _productosLogic.ObtenerTodosLosProductos();
                ActualizarDataGridView(_productosOriginales); // Actualizar el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los proveedores o productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarDataGridView(List<ProductoDTO> productos)
        {
            var productosConProveedor = productos.Select(p => new
            {
                p.Producto_ID,
                p.Tipo,
                p.Descripcion,
                ProveedorNombre = _proveedores.FirstOrDefault(prov => prov.Proveedor_ID == p.Proveedor_ID)?.Nombre // Obtener el nombre del proveedor
            }).ToList();

            dataGridView1.DataSource = productosConProveedor;
        }

        private void textBoxBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = textBoxBuscar.Text.ToLower();

            var productosFiltrados = _productosOriginales.Where(p =>
                p.Producto_ID.ToString().Contains(filtro) ||
                p.Tipo.ToLower().Contains(filtro) ||
                p.Descripcion.ToLower().Contains(filtro) ||
                p.Proveedor_ID.ToString().Contains(filtro)
            ).ToList();

            ActualizarDataGridView(productosFiltrados);
        }

        private async void buttonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación de los campos
                if (string.IsNullOrWhiteSpace(txtTipo.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                    comboBoxProveedor.SelectedItem == null)
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevoProducto = new ProductoDTO
                {
                    Tipo = txtTipo.Text,
                    Descripcion = txtDescripcion.Text,
                    Proveedor_ID = (int)comboBoxProveedor.SelectedValue // Obtiene el ID del proveedor seleccionado
                };

                await _productosLogic.AltaProducto(nuevoProducto);
                MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar los productos
                _productosOriginales = await _productosLogic.ObtenerTodosLosProductos();
                ActualizarDataGridView(_productosOriginales); // Actualizar el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor, seleccione un producto para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = dataGridView1.SelectedRows[0];
                var productoID = (int)selectedRow.Cells["Producto_ID"].Value; // Obtener ID del producto seleccionado
                var productoExistente = await _productosLogic.ObtenerProductoPorId(productoID);

                if (productoExistente == null)
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Actualiza los campos con los datos del producto existente
                txtTipo.Text = productoExistente.Tipo;
                txtDescripcion.Text = productoExistente.Descripcion;
                comboBoxProveedor.SelectedValue = productoExistente.Proveedor_ID;

                // Modifica el producto al guardar los cambios
                var productoModificado = new ProductoDTO
                {
                    Producto_ID = productoExistente.Producto_ID,
                    Tipo = txtTipo.Text,
                    Descripcion = txtDescripcion.Text,
                    Proveedor_ID = (int)comboBoxProveedor.SelectedValue
                };

                await _productosLogic.ModificarProducto(productoID, productoModificado);
                MessageBox.Show("Producto modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar los productos
                _productosOriginales = await _productosLogic.ObtenerTodosLosProductos();
                ActualizarDataGridView(_productosOriginales); // Actualizar el DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor, seleccione un producto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = dataGridView1.SelectedRows[0];
                var productoID = (int)selectedRow.Cells["Producto_ID"].Value; // Obtener ID del producto seleccionado

                var confirmResult = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    await _productosLogic.BajaProducto(productoID);
                    MessageBox.Show("Producto eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar los productos
                    _productosOriginales = await _productosLogic.ObtenerTodosLosProductos();
                    ActualizarDataGridView(_productosOriginales); // Actualizar el DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private async Task<List<ProveedorDTO>> ObtenerProveedores()
        {
            // Método para obtener los proveedores desde el repositorio o la API
            return await ProveedoresRepository.GetAllProveedores(); // Suponiendo que tienes un repositorio de proveedores
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


}
