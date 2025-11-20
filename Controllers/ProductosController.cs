namespace ProductosController;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using MVC.Repositorios;
using SistemaVentas.Web.ViewModels;
using System.ComponentModel;
using MVC.Interfaces;

public class ProductosController: Controller
{
    private readonly IProductosRepository _productoRepository;
    private readonly IAuthenticationService _authService;

    public ProductosController(IProductosRepository productoRepository, IAuthenticationService authService)
    {
        _productoRepository = productoRepository;
        _authService = authService;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var productosViewModel = _productoRepository.GetAllProductos().Select(p => new ProductosViewModel(p.idProducto,p.descripcion,p.precio)).ToList();
        
        return View(productosViewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ProductosViewModel productoVM)
    {   
        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }
        var nuevoProducto = new Productos
        {
            descripcion = productoVM.descripcion,
            precio = productoVM.precio
        };
        _productoRepository.InsertProducto(nuevoProducto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var p = _productoRepository.GetbyIdProducto(id);
        var productoViewModel = new ProductosViewModel(p.idProducto,p.descripcion,p.precio);
        return View(productoViewModel);
    }

    [HttpPost]
    public IActionResult Edit(int id,ProductosViewModel productoVM)
    {
        if(id != productoVM.idProducto) return NotFound();
        
        if (!ModelState.IsValid)
        {
            return NotFound(productoVM);
        }
        
        var productoAEditar = new Productos
        {
            idProducto = productoVM.idProducto,
            descripcion = productoVM.descripcion,
            precio = productoVM.precio
        };
        _productoRepository.UpdateProducto(id, productoAEditar);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var p = _productoRepository.GetbyIdProducto(id);
        var productoViewModel = new ProductosViewModel(p.idProducto,p.descripcion,p.precio);
        return View(productoViewModel);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _productoRepository.DeleteProducto(id);
        return RedirectToAction("Index");
    }
}