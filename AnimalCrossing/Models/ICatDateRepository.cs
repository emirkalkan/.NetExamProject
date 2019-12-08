using System;
using System.Collections.Generic;

namespace AnimalCrossing.Models
{
    public interface ICatDateRepository
    {
        
        public List<CatDate> Get();
        public CatDate Get(int catDateId);
        public void Delete(int catDateId);
        public void Save(CatDate c);
        public List<CatDate> Find(string search);
    }
}
