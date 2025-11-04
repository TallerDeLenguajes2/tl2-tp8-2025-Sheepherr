namespace ProductosController;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using PresupuestosRepository;

public class PresupuestosController: Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly PresupuestosRepository _presupuestosRepository;

    public PresupuestosController(ILogger<ProductosController> logger, PresupuestosRepository presupuestosRepository)
    {
        _logger = logger;
        _presupuestosRepository = presupuestosRepository;
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

        if (presupuesto == null)
            return NotFound();

        return View(presupuesto);

    }
    
}