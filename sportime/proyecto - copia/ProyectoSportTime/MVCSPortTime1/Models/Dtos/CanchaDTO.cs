namespace MVCSPortTime1.Models.Dtos
{
    public class CanchaDTO
    {
        public int Cancha_ID { get; set;  }
        public int Deporte_ID { get; set;  }
        public bool Activa { get; set;  } = true;
        public string DeporteNombre { get; set; } = string.Empty;
    }
}
