
namespace MVC.Interfaces;
using ClaseMVC.Models;



public interface IProductosRepository
{
    void DeleteProducto(global::System.Int32 id);
    List<Productos> GetAllProductos();
    Productos GetbyIdProducto(global::System.Int32 id);
    void InsertProducto(Productos producto);
    void UpdateProducto(global::System.Int32 id, Productos producto);
}