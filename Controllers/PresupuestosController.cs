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

    public PresupuestosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        //_presupuestosRepository = presupuestosRepository;
        _presupuestosRepository = new PresupuestosRepository();
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
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Presupuestos presupuesto)
    {
        _presupuestosRepository.InsertPresupuesto(presupuesto);
        return View();
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
        _presupuestosRepository.InsertPresupuesto(p);
        return View();
    }
}