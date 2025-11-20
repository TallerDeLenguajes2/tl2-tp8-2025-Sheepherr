namespace PresupuestosController;
using Microsoft.AspNetCore.Mvc;
using ClaseMVC.Models;
using MVC.Repositorios;
using SistemaVentas.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Interfaces;

public class PresupuestosController: Controller
{
    private readonly IPresupuestosRepository _presupuestosRepository;
    private readonly IProductosRepository _productosRepository;
    private readonly IAuthenticationService _authService;

    public PresupuestosController(
        IPresupuestosRepository presupuestosRepository,
        IProductosRepository productosRepository,
        IAuthenticationService authService)
    {
        _presupuestosRepository = presupuestosRepository;
        _productosRepository = productosRepository;
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var presupuestosVM = _presupuestosRepository.GetAllPresupuestos().Select(p => new PresupuestoViewModel(p.IdPresupuesto,p.nombreDestinatario, p.FechaCreacion)).ToList();
        return View(presupuestosVM);
    }
    [HttpGet]
    
    public IActionResult Details(int id)
    {
        var presu = _presupuestosRepository.GetbyIdPresupuesto(id);
        var detalle = presu.detalle;

        var presupuestoVM = new PresupuestoViewModel(id,presu.nombreDestinatario,presu.FechaCreacion);

        foreach (var d in detalle)
        {
            var produVM = new ProductosViewModel
            {
                idProducto = d.producto.idProducto,
                descripcion = d.producto.descripcion,
                precio = d.producto.precio,
                cantidad = d.cantidad
            };
            presupuestoVM.productosVM.Add(produVM);
        }

        
        return View(presupuestoVM);
    }
    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        var productos = _productosRepository.GetAllProductos();

        AgregarProductoViewModel model = new AgregarProductoViewModel
        {
            idPresupuesto = id,
            ListaProductos = new SelectList(productos,"idProducto", "descripcion")
        };
        return View(model);
    }
    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var productos = _productosRepository.GetAllProductos();
            model.ListaProductos = new SelectList(productos,"idProducto", "descripcion");

            return View(model);
        }
        var presupuesto = _presupuestosRepository.GetbyIdPresupuesto(model.idPresupuesto);

        if (presupuesto.detalle.Any(d => d.producto.idProducto == model.idProducto))
        {
            ModelState.AddModelError("idProducto", "Este producto ya está incluido en el presupuesto.");
        
            var productos = _productosRepository.GetAllProductos();
            model.ListaProductos = new SelectList(productos, "idProducto", "descripcion");

            return View(model);
        }

        _presupuestosRepository.AddDetalle(model.idPresupuesto, model.idProducto, model.cantidad);
        TempData["Success"] = "Producto agregado correctamente.";
        return RedirectToAction("Details", new { id = model.idPresupuesto });
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
    public IActionResult Create(PresupuestoViewModel presupuestoVM)
    {
        var presupuesto = new Presupuestos
        {
            nombreDestinatario = presupuestoVM.NombreDestinatario,
            FechaCreacion = presupuestoVM.FechaCreacion,
        };

        _presupuestosRepository.InsertPresupuesto(presupuesto);
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var presupuesto = _presupuestosRepository.GetbyIdPresupuesto(id);
        if (presupuesto == null) 
            return NotFound();
        var presupuestoVM = new PresupuestoViewModel
        {
            NombreDestinatario = presupuesto.nombreDestinatario,
            FechaCreacion = presupuesto.FechaCreacion,
            idPresupuesto = presupuesto.IdPresupuesto            
        };
        return View(presupuestoVM);
    }
    [HttpPost]
    public IActionResult Edit(PresupuestoViewModel presu)
    {
        var p = new Presupuestos
        {
            IdPresupuesto = presu.idPresupuesto,
            nombreDestinatario = presu.NombreDestinatario,
            FechaCreacion = presu.FechaCreacion
        };
        _presupuestosRepository.UpdatePresupuesto(p);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var presupuesto = _presupuestosRepository.GetbyIdPresupuesto(id);
        List<ProductosViewModel> productosVM = [];
        foreach (var d in presupuesto.detalle)
        {
            var p = new ProductosViewModel
            {
                idProducto = d.producto.idProducto,
                precio = d.producto.precio,
                descripcion = d.producto.descripcion,
                cantidad = d.cantidad
            };
            productosVM.Add(p);
        }
        var presupuestoVM = new PresupuestoViewModel
        {
            idPresupuesto = presupuesto.IdPresupuesto,
            NombreDestinatario = presupuesto.nombreDestinatario,
            FechaCreacion = presupuesto.FechaCreacion,
            productosVM = productosVM
        };
        return View(presupuestoVM);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _presupuestosRepository.DeletePresupuesto(id);
        return RedirectToAction("Index");
    }
    
}