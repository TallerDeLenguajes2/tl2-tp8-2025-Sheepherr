namespace MVC.Interfaces;

using ClaseMVC.Models;

public interface IPresupuestosRepository
{
    void DeletePresupuesto(global::System.Int32 id);
    List<Presupuestos> GetAllPresupuestos();
    Presupuestos GetbyIdPresupuesto(global::System.Int32 id);
    void InsertPresupuesto(Presupuestos presupuesto);
    void AddDetalle (int idPresupuesto, int idProducto, int cantidad);
    void UpdatePresupuesto(Presupuestos presupuesto);
    void UpdateDetalles(int idPresupuesto, int idProducto, int cantidad);
    void DeleteDetails(int idPresupuesto, int idProducto);    
}
