namespace SistemaVentas.Web.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AgregarProductoViewModel
{

    public int idPresupuesto {get; set;}
    [Display(Name = "Producto a agregar")]
    public int idProducto {get; set;}
    [Display(Name = "Cantidad")]
    [Required(ErrorMessage = "La cantidad es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
    public int cantidad {get; set;}
    public SelectList ListaProductos {get; set;}
    public AgregarProductoViewModel()
    {
    }
}