namespace Gundam_Tracker.Models;

public class Gundam
{
    public Gundam()
    {
    }
    
    public int GundamID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Grade { get; set; }
    public bool Built { get; set; }
    public int Rating { get; set; }
    
}