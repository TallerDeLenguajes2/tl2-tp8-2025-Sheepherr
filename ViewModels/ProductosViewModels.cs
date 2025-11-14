namespace SistemaVentas.Web.ViewModels;
using System.ComponentModel.DataAnnotations;
public class ProductosViewModels
{
    public int idProducto { get; set; }
    [Display(Name = "Descripción del Producto")]
    [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
    public string descripcion { get; set; }
    [Display(Name = "Precio Unitario")]
    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public decimal precio { get; set; }
}