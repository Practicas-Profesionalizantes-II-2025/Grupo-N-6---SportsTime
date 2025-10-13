using System.Collections.Generic;


namespace MVCSPortTime1.ViewModels
{
    public class ProveedorIndexViewModel
    {
        public ProveedorDTO NuevoProveedor { get; set; } = new ProveedorDTO();
        public List<ProveedorDTO> Proveedores { get; set; } = new List<ProveedorDTO>();
    }
}
