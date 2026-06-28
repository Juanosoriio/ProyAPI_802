namespace ProyAPI_802.Models;

public partial class Producto
{
    public int IdProducto { get; set; }
    public int IdCategoria { get; set; }
    public string? Nombre { get; set; }
    public decimal? Precio { get; set; }
    public int? Stock { get; set; }
    public bool? Estado { get; set; }

    public virtual Categoria? objCategoria { get; set; }
}