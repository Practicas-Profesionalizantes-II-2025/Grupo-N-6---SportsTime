using Shared.Dtos;
using Negocio.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Negocio.Implementations
{
    public class ConsumicionXturnoLogic
    {
        private readonly HttpClient _httpClient;

        public ConsumicionXturnoLogic()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7094/api/") };
        }

        public async Task<(bool EsExitoso, string MensajeError)> AgregarConsumicion(ConsumicionXturnoDTO consumicion)
        {
            if (consumicion == null)
                return (false, "El producto no puede ser nulo.");

            if (consumicion.Turno_ID <= 0)
                return (false, "Seleccione un turno válido.");

            if (consumicion.Producto_ID <= 0)
                return (false, "Seleccione un producto válido.");

            if (consumicion.Cantidad <= 0)
                return (false, "La cantidad debe ser mayor a 0.");

            await ConsumicionXTurnoRepo.AgregarConsumicion(consumicion);
            return (true, "Producto agregado correctamente.");
        }

        public async Task<List<ConsumicionXturnoDTO>> ObtenerConsumicionesPorTurno(int turnoId)
        {
            if (turnoId <= 0)
                throw new ArgumentException("El ID del turno debe ser válido.");

            var consumiciones = await ConsumicionXTurnoRepo.ObtenerConsumicionesPorTurno(turnoId);
            return consumiciones ?? new List<ConsumicionXturnoDTO>();
        }

        public async Task QuitarConsumicion(int turnoID, int productoID)
        {
            if (turnoID <= 0 || productoID <= 0)
                throw new ArgumentException("Los IDs deben ser mayores a cero.");

            await ConsumicionXTurnoRepo.QuitarConsumicion(turnoID, productoID);
        }

        public async Task<string> ObtenerTextoConsumiciones(int turnoId)
        {
            var consumiciones = await ObtenerConsumicionesPorTurno(turnoId);
            var productos = await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("productos");

            return string.Join(Environment.NewLine, consumiciones.Select(c =>
            {
                var producto = productos.FirstOrDefault(p => p.Producto_ID == c.Producto_ID);
                return producto != null
                    ? $"Producto: {producto.Descripcion}, Cantidad: {c.Cantidad}"
                    : $"Producto no encontrado, ID: {c.Producto_ID}, Cantidad: {c.Cantidad}";
            }));
        }
    }



}
