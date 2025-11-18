namespace PresupuestosController;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using MVC.Repositorios;
using SistemaVentas.Web.ViewModels;

public class PresupuestosController: Controller
{
    private readonly PresupuestosRepository _presupuestosRepository = new PresupuestosRepository();
    private readonly ProductosRepository _productosRepository = new ProductosRepository();
    
    [HttpGet]
    public IActionResult Index()
    {
        var presupuestosVM = _presupuestosRepository.GetAllPresupuestos().Select(p => new PresupuestoViewModel(p.IdPresupuesto,p.nombreDestinatario, p.FechaCreacion)).ToList();
        return View(presupuestosVM);
    }
    [HttpGet]
    
    public IActionResult Details(int id)
    {
        var presupuesto = _presupuestosRepository.GetbyIdPresupuesto(id);
        ViewBag.Productos = _productosRepository.GetAllProductos();
        return View(presupuesto);

    }
    [HttpPost]
    public IActionResult AgregarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        _presupuestosRepository.AddDetalle(idPresupuesto, idProducto, cantidad);
        TempData["Success"] = "Producto agregado correctamente.";
        return RedirectToAction("Details", new { id = idPresupuesto });
    }

    [HttpPost]
    public IActionResult EliminarDetalle(int idPresupuesto, int idProducto)
    {
        _presupuestosRepository.DeleteDetails(idPresupuesto,idProducto);
        TempData["Error"] = "Producto eliminado del presupuesto.";
        return RedirectToAction("Details", new {id = idPresupuesto});
    }
    [HttpPost]
    public IActionResult ModificarDetalle(int idPresupuesto, int idProducto, int cantidad)
    {
        _presupuestosRepository.UpdateDetalles(idPresupuesto, idProducto, cantidad);
        TempData["Success"] = "Cantidad actualizada correctamente.";
        return RedirectToAction("Details", new {id = idPresupuesto});
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Presupuestos presupuesto)
    {
        _presupuestosRepository.InsertPresupuesto(presupuesto);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var presupuesto = _presupuestosRepository.GetbyIdPresupuesto(id);
        if (presupuesto == null) 
            return NotFound();

        return View(presupuesto);
    }
    [HttpPost]
    public IActionResult Edit(Presupuestos p)
    {
        _presupuestosRepository.UpdatePresupuesto(p);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var presupuesto = _presupuestosRepository.GetbyIdPresupuesto(id);
        return View(presupuesto);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _presupuestosRepository.DeletePresupuesto(id);
        return RedirectToAction("Index");
    }
    
}