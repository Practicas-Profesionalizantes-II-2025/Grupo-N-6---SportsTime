using Microsoft.EntityFrameworkCore;
using Negocio.Contracts;
using API.Data;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio.Repositorys;
using Shared.Dtos;

namespace Negocio.Implementations
{
    public class DeportesLogic 
    {
        public async Task AltaDeporte(DeporteDTO nuevoDeporte)
        {
            ArgumentNullException.ThrowIfNull(nuevoDeporte);

            await DeportesRepository.CreateDeporte(nuevoDeporte);  // Llamamos al repositorio para crear el deporte
        }

        public async Task ModificarDeporte(int deporteID, DeporteDTO deporteModificado)
        {
            ArgumentNullException.ThrowIfNull(deporteModificado);

            if (deporteID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await DeportesRepository.UpdateDeporte(deporteID, deporteModificado);  // Llamamos al repositorio para modificar el deporte
        }

        public async Task BajaDeporte(int deporteID)
        {
            if (deporteID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            await DeportesRepository.DeleteDeporte(deporteID);  // Llamamos al repositorio para eliminar el deporte
        }

        public async Task<List<DeporteDTO>> ObtenerTodosLosDeportes()
        {
            return await DeportesRepository.GetAllDeportes();  // Llamamos al repositorio para obtener todos los deportes
        }

        public async Task<DeporteDTO?> ObtenerDeportePorId(int deporteID)
        {
            if (deporteID <= 0)
            {
                throw new ArgumentException("Id debe ser mayor a cero");
            }

            return await DeportesRepository.GetDeporteById(deporteID);  // Llamamos al repositorio para obtener un deporte por ID
        }
    }

}
