namespace Gundam_Tracker.Models;

public class GundamIndexView
{
    public IEnumerable<Gundam> Gunpla { get; set; }
    
    public double TotalSpent { get; set; }
    
    public int TotalBuilt { get; set; }
    
    public int TotalBought { get; set; }
}