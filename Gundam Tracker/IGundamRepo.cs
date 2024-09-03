using Gundam_Tracker.Models;

namespace Gundam_Tracker;

    public interface IGundamRepo
    {
        IEnumerable<Gundam> GetAllProducts();
    }


