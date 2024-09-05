using Gundam_Tracker.Models;

namespace Gundam_Tracker;

    public interface IGundamRepo //used to create all the functions 
    {
        IEnumerable<Gundam> GetAllGunpla();

        IEnumerable<Gundam> GetGradeGunpla(string grade);

        IEnumerable<Gundam> GetBuiltGunpla(int built);
        
        Gundam GetGunpla(int id);
        
        void UpdateGunpla(Gundam gundam);
        
        public void InsertGunpla(Gundam gunplaToInsert);

        public void DeleteGunpla(Gundam gundam);
    }


