using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PruebaAKS.Models;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System.Numerics;
namespace PruebaAKS.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context ;
    public HomeController(ILogger<HomeController> logger,AppDbContext context)
    {

    _context = context;
    _context.Database.EnsureCreated ();
    _logger = logger;
    }
    public IActionResult Index()
    {
        Models.ViewModelItem model = new Models.ViewModelItem();

        
        model.Lista = _context.TodoItems.ToList ();
        return View(model);
    }
[HttpPost]
 public IActionResult Index(Models.ViewModelItem model)
 {
     if (model.TodoItem.Length > 0)
     {
            TodoItem  todoItem = new TodoItem ();
            todoItem.Nombre = model.TodoItem;
            _context.TodoItems.Add (todoItem);
            _context.SaveChanges();

     }
     
     model.Lista = _context.TodoItems.ToList ();
    return View (model);
 }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
