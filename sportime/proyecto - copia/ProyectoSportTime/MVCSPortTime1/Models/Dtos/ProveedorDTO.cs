namespace MVCSPortTime1.Models.Dtos
{
    public class ProveedorDTO
    {
        public int Proveedor_ID { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
