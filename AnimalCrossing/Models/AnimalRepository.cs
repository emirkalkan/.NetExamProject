﻿using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrossing.Models
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalCrossingContext _context;
        public AnimalRepository(AnimalCrossingContext _context)
        {
            this._context = _context;
        }

        public void Delete(int catId)
        {
            _context.Cats.Remove(this.Get(catId));
            _context.SaveChanges();
        }

        public List<Cat> Find(string search)
        {
            var cats = from m in _context.Cats.Include(c => c.Species)
                       select m;

            if (!String.IsNullOrEmpty(search))
            {
                cats = cats.Where(cat => cat.Name.Contains(search) ||
                cat.Description.Contains(search) || cat.Species.Name.Contains(search));
            }
            return cats.ToList();
        }

        public List<Cat> Get()
        {
            return _context.Cats.Include(c => c.Species).ToList();
        }

        public Cat Get(int catId)
        {
            return _context.Cats.Find(catId);
        }

        public void Save(Cat c)
        {
            if (c.CatId == 0)
            {
                _context.Cats.Add(c);
            }
            else
            {
                _context.Cats.Update(c);
            }
            _context.SaveChanges();
        }
    }
}
