using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimalCrossing.Data;
using AnimalCrossing.Models;
using Microsoft.AspNetCore.Authorization;
using AnimalCrossing.Models.ViewModels;

namespace AnimalCrossing.Controllers
{
    [Authorize]
    public class CatDatesController : Controller
    {
        //private readonly AnimalCrossingContext _context;

        //public CatDatesController(AnimalCrossingContext context)
        //{
        //    _context = context;
        //}
        private ICatDateRepository catDateRepository;
        private IAnimalRepository animalRepository;

        public CatDatesController(ICatDateRepository catDateRepo, IAnimalRepository animalRepo)
        {
            this.catDateRepository = catDateRepo;
            this.animalRepository = animalRepo;

        }

        // GET: CatDates
        public IActionResult Index(string searchString)
        {
            List<CatDate> cats = this.catDateRepository.Find(searchString);
            ViewBag.SearchString = searchString;
            return View("Index", cats.ToList());
            //return View(catDateRepository.Get());
        }

        //// GET: CatDates/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var catDate = await _context.CatDates
        //        .FirstOrDefaultAsync(m => m.CatDateId == id);
        //    if (catDate == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(catDate);
        //}

        // GET: CatDates/Create
        public IActionResult Create()
        {
            List<Cat> cat = animalRepository.Get();
            //AnimalCatVM animalCatVM = new AnimalCatVM();
            //animalCatVM.CatSelectList = new SelectList(cat, "CatId", "Name");
            ViewBag.CatList = new SelectList(cat, "CatId", "Name");
            return View();
        }

        // POST: CatDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CatDate catDate)
        {
            // catDate.HostCat.CatId
            ModelState.Remove("HostCat.Gender");
            ModelState.Remove("HostCat.Description");
            ModelState.Remove("HostCat.ProfilePicture");
            ModelState.Remove("HostCat.Name");
            ModelState.Remove("GuestCat.Gender");
            ModelState.Remove("GuestCat.Description");
            ModelState.Remove("GuestCat.ProfilePicture");
            ModelState.Remove("GuestCat.Name");
            if (ModelState.IsValid)
            {
                catDate.HostId = catDate.HostCat.CatId;
                catDate.GuestId = catDate.GuestCat.CatId;
                var guestCat = animalRepository.Get(catDate.GuestCat.CatId);
                var hostCat = animalRepository.Get(catDate.HostCat.CatId);
                catDate.HostCat.Name = hostCat.Name;
                catDate.GuestCat.Name = guestCat.Name;

                catDateRepository.Save(catDate);
            }
            return RedirectToAction("Index");
        }

        // GET: CatDates/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catDate = catDateRepository.Get((int)id);
            if (catDate == null)
            {
                return NotFound();
            }
            return View(catDate);
        }

        // POST: CatDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CatDateId,HostId,GuestId,Location,DateTime")] CatDate catDate)
        {
            if (id != catDate.CatDateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    catDateRepository.Save(catDate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatDateExists(catDate.CatDateId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(catDate);
        }

        // GET: CatDates/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catDate = catDateRepository.Get((int)id);
            if (catDate == null)
            {
                return NotFound();
            }

            return View(catDate);
        }

        // POST: CatDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            catDateRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private bool CatDateExists(int id)
        {
            //return _context.CatDates.Any(e => e.CatDateId == id);
            return false;
        }
    }
}
