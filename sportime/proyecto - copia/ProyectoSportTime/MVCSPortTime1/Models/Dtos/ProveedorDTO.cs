using System.ComponentModel.DataAnnotations;

public class ProveedorDTO
{
    public int Proveedor_ID { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no es válido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [RegularExpression(@"^\d{7,15}$", ErrorMessage = "El teléfono debe ser numérico y tener entre 7 y 15 dígitos")]
    public string Telefono { get; set; }
}