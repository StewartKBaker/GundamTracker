using Gundam_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gundam_Tracker.Controllers;

public class GundamController : Controller 
{
    private readonly IGundamRepo _repo; //Creates Connection

    public GundamController(IGundamRepo repo)
    {
        _repo = repo;
    }
    
    public IActionResult Index(string gradeSort) //Method for buttons in Index, used to filter what is shown
    {
        IEnumerable<Gundam> products;
        
        switch (gradeSort)
        {
            case "All":
                products = _repo.GetAllGunpla(); //shows all
                break;
            case "HG":
                products = _repo.GetGradeGunpla("HG"); //shows HG kits
                break;
            case "MG":
                products = _repo.GetGradeGunpla("MG"); //shows MG kits
                break;
            case "RG":
                products = _repo.GetGradeGunpla("RG"); //shows RG kits
                break;
            case "PG":
                products = _repo.GetGradeGunpla("PG"); //shows PG kits
                break;
            case "BUILT":
                products = _repo.GetBuiltGunpla(1); //shows built kits
                break;
            case "NON-BUILT":
                products = _repo.GetBuiltGunpla(0); //shows Backlog kits
                break;
            default:
                products = _repo.GetAllGunpla(); //shows all when page is first open
                break;
        }
        
        var totalBought = products.Count(); //shows the total amount of figures owned
        var totalBuilt = products.Count(x => x.Built); //shows total amount of figures that have been built
        var totalSpent = products.Sum(x => x.Price); //shows the total amount of money that has been spent

        var roundedTotal = Math.Round(totalSpent, 2); //rounds the total to be .00

        var viewGundam = new GundamIndexView() //returns a view for all variables
        {
            Gunpla = products,
            TotalBought = totalBought,
            TotalBuilt = totalBuilt,
            TotalSpent = roundedTotal
        };
        
        return View(viewGundam); //sends view for page
    }

    public IActionResult ViewGunpla(int id) //used to show a specific gundam figure when "view" button is clicked
    {
        var product = _repo.GetGunpla(id);
        return View(product);
    }

    public IActionResult UpdateGunpla(int id) //used to show the gundam update page
    {
        Gundam gunpla = _repo.GetGunpla(id);
        if(gunpla == null)
        {
            return View("ProductNotFound");
        }
        return View(gunpla);
    }

    public IActionResult UpdateGunplaToDatabase(Gundam gundam) //used to update the gundam IN the database
    {
        _repo.UpdateGunpla(gundam);

        return RedirectToAction("ViewGunpla", new { id = gundam.GundamID });
    }

    public IActionResult InsertGunpla() //used to show the creation page
    {
        return View();
    }

    public IActionResult InsertGunplaToDatabase(Gundam gunplaToInsert) //used to add a new gundam to the database
    {
        _repo.InsertGunpla(gunplaToInsert);
        return RedirectToAction("Index");
    }

    public IActionResult Favorites() //used to show the favorites page
    {
        return View();
    }

    public IActionResult DeleteGundam(Gundam gundam) //used to delete a gundam figure from the database
    {
        _repo.DeleteGunpla(gundam);
        return RedirectToAction("Index");
    }
}