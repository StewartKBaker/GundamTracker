using Gundam_Tracker.Models;

namespace Gundam_Tracker;

    public interface IGundamRepo
    {
        IEnumerable<Gundam> GetAllGunpla();
        Gundam GetGunpla(int id);
        void UpdateGunpla(Gundam gundam);
    }


