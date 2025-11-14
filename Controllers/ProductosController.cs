namespace ProductosController;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using MVC.Repositorios;

public class ProductosController: Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly ProductosRepository _productoRepository;

    public ProductosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        //_productoRepository = productoRepository;
        _productoRepository = new ProductosRepository();
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var productos = _productoRepository.GetAllProductos();
        return View(productos);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Productos producto)
    {
        _productoRepository.InsertProducto(producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producto = _productoRepository.GetbyIdProducto(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Edit(int id, Productos producto)
    {
        _productoRepository.UpdateProducto(id, producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var producto = _productoRepository.GetbyIdProducto(id);
        return View(producto);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _productoRepository.DeleteProducto(id);
        return RedirectToAction("Index");
    }
}