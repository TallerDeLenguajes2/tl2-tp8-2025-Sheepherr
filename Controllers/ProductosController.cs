using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using ProductosRepository;
using Productos;
namespace ProductosController;
public class ProductosController: Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly ProductosRepository _productoRepository;

    public ProductosController(Ilogger<ProductosController> logger)
    {
        _logger = logger;
    }
    

    [HttpGet]
    public IActionResult Index()
    {
        List<Producto> productos = productoRepository.GetAllProductos();
        return View(productos);
    }
}