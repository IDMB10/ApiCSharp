namespace APIWeb.Models {
    public class Producto {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public Decimal Precio { get; set; }
        public bool Activo { get; set; }
    }
}