namespace ProductosController;
using tl2_tp8_2025_Sheepherr.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using ProductosRepository;

public class ProductosController: Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly ProductosRepository _productoRepository;

    public ProductosController(ILogger<ProductosController> logger, ProductosRepository productoRepository)
    {
        _logger = logger;
        _productoRepository = productoRepository;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var productos = _productoRepository.GetAllProductos();
        return View(productos);
    }
}