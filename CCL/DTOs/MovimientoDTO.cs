namespace CCL.DTOs
{
    public class MovimientoDTO
    {
        public int IdProducto { get; set; }
        public string Tipo { get; set; } = null!; // "entrada" o "salida"
        public int Cantidad { get; set; }
    }
}
