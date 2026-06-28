namespace ProyAPI_802.DTOs
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }

        public bool? Estado { get; set; }

        public string? CategoriaNombre { get; set; }
    }

    public class ProductoCreateDto
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
    }

    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

}
