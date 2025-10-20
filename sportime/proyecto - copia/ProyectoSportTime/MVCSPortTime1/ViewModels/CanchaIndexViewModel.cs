using System.Collections.Generic;
using MVCSPortTime1.Models.Dtos;

namespace MVCSPortTime1.ViewModels
{
    public class CanchaLookupItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }

    public class CanchaIndexViewModel
    {
        public List<CanchaDTO> Canchas { get; set; } = new();
        public CanchaDTO NuevoCancha { get; set; } = new();
        public List<CanchaLookupItem> Deportes { get; set; } = new();
    }
}