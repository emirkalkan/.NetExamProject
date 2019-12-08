using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing.Data;
using AnimalCrossing.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossing.Models
{
    public class CatDateRepository : ICatDateRepository
    {
        private readonly AnimalCrossingContext _context;
        public CatDateRepository(AnimalCrossingContext _context)
        {
            this._context = _context;
        }

        public void Delete(int catDateId)
        {
            _context.CatDates.Remove(this.Get(catDateId));
        }

        public List<CatDate> Find(string search)
        {
            var cats = from m in _context.CatDates
                       select m;

            if (!String.IsNullOrEmpty(search))
            {
                cats = cats.Where(cat => cat.HostCat.Name.Contains(search) ||
                cat.GuestCat.Name.Contains(search) || cat.Location.Contains(search));
            }

            return cats.ToList();

        }

        public List<CatDate> Get()
        {
            return _context.CatDates.ToList();
        }

        public CatDate Get(int catDateId)
        {
            return _context.CatDates.Find(catDateId);
        }

        public void Save(CatDate c)
        {
            if (c.CatDateId == 0)
            {
                _context.CatDates.Add(c);
            }
            else
            {
                _context.CatDates.Update(c);
            }

            _context.SaveChanges();
        }
    }
}
