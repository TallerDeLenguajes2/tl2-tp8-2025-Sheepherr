namespace MVC.Interfaces;

using ClaseMVC.Models;

public interface IPresupuestosRepository
{
    void DeletePresupuesto(global::System.Int32 id);
    List<Presupuestos> GetAllPresupuestos();
    Presupuestos GetbyIdPresupuesto(global::System.Int32 id);
    void InsertPresupuesto(Presupuestos presupuesto);
    void InsertPresupuestoDetalle(global::System.Int32 id, PresupuestoDetalle presupuestoDetalle);
}
