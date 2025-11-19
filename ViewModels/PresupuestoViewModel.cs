namespace SistemaVentas.Web.ViewModels;
using System.ComponentModel.DataAnnotations;

public class PresupuestoViewModel
{
    public int idPresupuesto {get; set;}
    [Required(ErrorMessage ="El nombre es obligatorio")]
    public string NombreDestinatario {get; set;}
    [Required(ErrorMessage = "La fecha es requerida")]
    public string FechaCreacion { get; set; }
    public List<ProductosViewModel> productosVM {get; set;}
    public PresupuestoViewModel(int id, string nombre, string fecha)
    {
        this.idPresupuesto = id;
        this.NombreDestinatario = nombre;
        this.FechaCreacion = fecha;
        this.productosVM = [];
    }
    public PresupuestoViewModel()
    {
    }
}