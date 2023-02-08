public class ProductoCreateDTO {

    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public Decimal Precio { get; set; }
    public DateTime FechaDeAlta { get; set; }
    public bool Activo { get; set; }

    public ProductoCreateDTO() {
        FechaDeAlta = DateTime.Now;
        Activo = true;
    }
}