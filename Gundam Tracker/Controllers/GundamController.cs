using Gundam_Tracker.Models;
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
        var products = _repo.GetAllGunpla();
        return View(products);
    }

    public IActionResult ViewGunpla(int id)
    {
        var product = _repo.GetGunpla(id);
        return View(product);
    }

    public IActionResult UpdateGunpla(int id)
    {
        Gundam gunpla = _repo.GetGunpla(id);
        if(gunpla == null)
        {
            return View("ProductNotFound");
        }
        return View(gunpla);
    }

    public IActionResult UpdateGunplaToDatabase(Gundam gundam)
    {
        _repo.UpdateGunpla(gundam);

        return RedirectToAction("ViewGunpla", new { id = gundam.GundamID });
    }
}