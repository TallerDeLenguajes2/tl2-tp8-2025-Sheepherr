namespace ProductosController;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using MVC.Repositorios;

public class PresupuestosController: Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly PresupuestosRepository _presupuestosRepository;
    private readonly ProductosRepository _productosRepository;

    public PresupuestosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        //_presupuestosRepository = presupuestosRepository;
        _presupuestosRepository = new PresupuestosRepository();
        _productosRepository = new ProductosRepository();
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var presupuestos = _presupuestosRepository.GetAllPresupuestos();
        return View(presupuestos);
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