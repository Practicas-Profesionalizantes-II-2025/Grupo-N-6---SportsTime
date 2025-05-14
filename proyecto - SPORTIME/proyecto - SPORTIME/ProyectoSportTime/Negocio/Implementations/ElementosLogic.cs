using Microsoft.EntityFrameworkCore;
using Negocio.Contracts;
using API.Data;
using Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Implementations
{
    public class ElementosLogic : InterfaceElementos
    {
        private readonly ProyectoDbContext _context;

        // Constructor donde se inyecta el DbContext
        public ElementosLogic(ProyectoDbContext context)
        {
            _context = context;
        }

        public void AltaElemento(string nombre, int cantidad, int canchaID)
        {
            var nuevoElemento = new Elementos
            {
                Nombre = nombre,
                Cantidad = cantidad,
                Cancha_ID = canchaID
            };
            _context.Elementos.Add(nuevoElemento);
            _context.SaveChanges();
        }

        public void ModificarElemento(int elementoID, string nuevoNombre, int nuevaCantidad)
        {
            var elemento = _context.Elementos.Find(elementoID);
            if (elemento != null)
            {
                elemento.Nombre = nuevoNombre;
                elemento.Cantidad = nuevaCantidad;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"No se encontró un elemento con ID {elementoID}.");
            }
        }

        public void BajaElemento(int elementoID)
        {
            var elemento = _context.Elementos.Find(elementoID);
            if (elemento != null)
            {
                _context.Elementos.Remove(elemento);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"No se encontró un elemento con ID {elementoID}.");
            }
        }
    }

}
