// Using statements
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicioso.Models;
namespace YourProjectName.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        // Now any time we want to access our database we use _context   
        List<Dish> DishesList = _context.Dishes.ToList();



        return View("Index", DishesList);
    }

    [HttpGet]

    [Route("form/dishes")]
    public IActionResult FormDishes(Dish plato)
    {

        return View("FormDishes");
    }

      [HttpGet]
      [Route("dashboard/{Id}")]
        public IActionResult Dashboard(int Id)
        {
            Dish model = _context.Dishes.FirstOrDefault(d => d.Id == Id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }


    [HttpGet]
    [Route("formulario/editar/dishe/{id}")]

    public IActionResult FormularioEditarDish(int id)
    {
        Dish? plato = _context.Dishes.FirstOrDefault(d => d.Id == id);
        return View("FormularioEditarDish", plato);
    }

    //POST
    [HttpPost]

    [Route("new/dishe")]
    public IActionResult NewDishe(Dish plato)
    {



        // si todos los datos fueron validados
        if (ModelState.IsValid)
        {

            _context.Dishes.Add(plato);
            _context.SaveChanges();
            return RedirectToAction("Index");
        };
        return View("FormDishes");
    }

    
    [HttpPost]
    [Route("eliminar/dishe{id}")]
    public IActionResult EliminarDishe(int id)
    {
        Dish platoNuevo = _context.Dishes.FirstOrDefault(plato => plato.Id == id);

        _context.Dishes.Remove(platoNuevo);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

   [HttpPost]
    [Route("actualiza/dishe/{id}")]
    public IActionResult ActualizaDishe(Dish plato, int id)
    {
        Dish? platoPrevio = _context.Dishes.FirstOrDefault(p => p.Id == id);

        if (ModelState.IsValid)
        {
            platoPrevio.Name = plato.Name;
            platoPrevio.Chef = plato.Chef;
            platoPrevio.Tastines = plato.Tastines;
            platoPrevio.Description = plato.Description;
            //peliculaPrevia.Fecha_Actualizacion = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("FormularioEditarDish", platoPrevio);

    }


}
