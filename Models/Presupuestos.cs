namespace Presupuestos;

using PresupuestoDetalle;
public class Presupuestos
{
    public int IdPresupuesto { get; set; }
    public string nombreDestinatario { get; set; }
    public string FechaCreacion { get; set; }
    public List<PresupuestoDetalle> detalle { get; set; }
    public void MontoPresupuesto()
    {

    }
    public void MontoPresupuestoConIva()
    {

    }
    public void CantidadProductos()
    {
        
    }
}