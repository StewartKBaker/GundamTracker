using Gundam_Tracker.Models;

namespace Gundam_Tracker;

    public interface IGundamRepo
    {
        IEnumerable<Gundam> GetAllGunpla();

        IEnumerable<Gundam> GetGradeGunpla(string grade);
        
        Gundam GetGunpla(int id);
        
        void UpdateGunpla(Gundam gundam);
        
        public void InsertGunpla(Gundam gunplaToInsert);

        public void DeleteGunpla(Gundam gundam);
    }


