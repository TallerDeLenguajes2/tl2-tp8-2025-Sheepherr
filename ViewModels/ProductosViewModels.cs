namespace SistemaVentas.Web.ViewModels;
using System.ComponentModel.DataAnnotations;
public class ProductosViewModel
{
    public int idProducto { get; set; }
    [Display(Name = "Descripción del Producto")]
    [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
    [Required(ErrorMessage = "La descripcion es obligatoria.")]
    public string descripcion { get; set; }
    [Display(Name = "Precio Unitario")]
    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
    public decimal precio { get; set; }
    public List<ProductosViewModel> productos {get; set;}
    public ProductosViewModel(int idprodu, string desc, decimal prec)
    {
        this.idProducto = idprodu;
        this.descripcion = desc;
        this.precio = prec;
    }

    public ProductosViewModel()
    {
    }
}