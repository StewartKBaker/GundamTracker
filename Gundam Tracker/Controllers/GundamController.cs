using Microsoft.AspNetCore.Mvc;

namespace Gundam_Tracker.Controllers;

public class GundamController : Controller
{
    private readonly IGundamRepo _repo;

    public GundamController(IGundamRepo repo)
    {
        _repo = repo;
    }
    
    public IActionResult Index()
    {
        var products = _repo.GetAllProducts();
        return View(products);
    }
}