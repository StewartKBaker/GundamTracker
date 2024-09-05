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
    
    public IActionResult Index(string gradeSort)
    {
        IEnumerable<Gundam> products;
        switch (gradeSort)
        {
            case "All":
                products = _repo.GetAllGunpla();
                break;
            case "HG":
                products = _repo.GetGradeGunpla("HG");
                break;
            case "MG":
                products = _repo.GetGradeGunpla("MG");
                break;
            case "RG":
                products = _repo.GetGradeGunpla("RG");
                break;
            case "PG":
                products = _repo.GetGradeGunpla("PG");
                break;
            default:
                products = _repo.GetAllGunpla();
                break;
        }
        var totalBought = products.Count();
        var totalBuilt = products.Count(x => x.Built);
        var totalSpent = products.Sum(x => x.Price);

        var roundedTotal = Math.Round(totalSpent, 2);

        var viewGundam = new GundamIndexView()
        {
            Gunpla = products,
            TotalBought = totalBought,
            TotalBuilt = totalBuilt,
            TotalSpent = roundedTotal
        };
        
        return View(viewGundam);
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

    public IActionResult InsertGunpla()
    {
        return View();
    }

    public IActionResult InsertGunplaToDatabase(Gundam gunplaToInsert)
    {
        _repo.InsertGunpla(gunplaToInsert);
        return RedirectToAction("Index");
    }

    public IActionResult Favorites()
    {
        return View();
    }

    public IActionResult DeleteGundam(Gundam gundam)
    {
        _repo.DeleteGunpla(gundam);
        return RedirectToAction("Index");
    }
}