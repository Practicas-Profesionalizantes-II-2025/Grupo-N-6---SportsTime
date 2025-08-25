using Shared.Dtos;
using CNegocio.Contracts;
using CDatos.Repositorys.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace CNegocio.Implementations
{
    public class ConsumicionXturnoLogic : IConsumicionXTurno
    {
        private readonly IConsumicionXTurnoRepository _repo;

        public ConsumicionXturnoLogic(IConsumicionXTurnoRepository repo)
        {
            _repo = repo;
        }
        /* private readonly HttpClient _httpClient;

        public ConsumicionXturnoLogic()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7094/api/") };
        }
        */

        public async Task AgregarConsumicion(ConsumicionXturnoDTO consumicion)
        {
            ArgumentNullException.ThrowIfNull(consumicion);

            if (consumicion.Turno_ID <= 0)
                throw new ArgumentException("Seleccione un turno válido.");

            if (consumicion.Producto_ID <= 0)
                throw new ArgumentException("Seleccione un producto válido.");

            if (consumicion.Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");

            await _repo.AgregarConsumicion(consumicion);
        }

        public async Task<List<ConsumicionXturnoDTO>> ObtenerConsumicionesPorTurno(int turnoId)
        {
            if (turnoId <= 0)
                throw new ArgumentException("El ID del turno debe ser válido.");

            var consumiciones = await _repo.ObtenerConsumicionesPorTurno(turnoId);
            return consumiciones ?? new List<ConsumicionXturnoDTO>();
        }

        public async Task QuitarConsumicion(int turnoID, int productoID)
        {
            if (turnoID <= 0 || productoID <= 0)
                throw new ArgumentException("Los IDs deben ser mayores a cero.");

            await _repo.QuitarConsumicion(turnoID, productoID);
        }

       /*  public async Task<string> ObtenerTextoConsumiciones(int turnoId)
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
       */
    }



}
