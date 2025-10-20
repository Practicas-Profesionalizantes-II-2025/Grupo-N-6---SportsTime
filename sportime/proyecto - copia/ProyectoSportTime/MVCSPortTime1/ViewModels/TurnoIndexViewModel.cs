using System;
using System.Collections.Generic;
using MVCSPortTime1.Models;

namespace MVCSPortTime1.ViewModels
{
    public class TurnoLookupItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;

        // Nuevo: id del deporte asociado (nullable por seguridad)
        public int? ParentId { get; set; }

    }

    public class TurnoIndexViewModel
    {
        public List<TurnoDTO> Turnos { get; set; } = new();
        public TurnoDTO NuevoTurno { get; set; } = new();
        public List<TurnoLookupItem> Canchas { get; set; } = new();
        public List<TurnoLookupItem> Usuarios { get; set; } = new();
        public List<TurnoLookupItem> Deportes { get; set; } = new();
        public List<TurnoLookupItem> Estados { get; set; } = new();
        public string Search { get; set; } = string.Empty;
    }
}