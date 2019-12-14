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
        private ICatDateRepository catDateRepository;
        private IAnimalRepository animalRepository;

        public CatDatesController(ICatDateRepository catDateRepo, IAnimalRepository animalRepo)
        {
            this.catDateRepository = catDateRepo;
            this.animalRepository = animalRepo;
        }

        // GET: CatDates
        //[AllowAnonymous] optional
        public IActionResult Index(String searchString)
        {
            List<CatDate> cats = this.catDateRepository.Find(searchString);
            ViewBag.SearchString = searchString;

            return View("Index", cats.ToList()); 
        }

        // GET: CatDates/Create
        public IActionResult Create()
        {
            //List<Cat> cat = animalRepository.Get();
            //ViewBag.CatList = new SelectList(cat, "CatId", "Name");
            return View(ViewModelCreator.CreateAnimalCatDateVm(animalRepository));
        }

        // POST: CatDates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(CatDate catDate)
        {
            ModelState.Remove("catDate.HostCat");
            ModelState.Remove("catDate.GuestCat");
            if (ModelState.IsValid)
            {
                var hostCat = animalRepository.Get(catDate.HostId);
                var guestCat = animalRepository.Get(catDate.GuestId);
                catDate.HostCat = hostCat;
                catDate.GuestCat = guestCat;

                catDateRepository.Save(catDate);
                return RedirectToAction(nameof(Thanks), catDate);
            }
            
            return View(ViewModelCreator.CreateAnimalCatDateVm(animalRepository));

        }

        public IActionResult Thanks(CatDate catDate)
        {
            catDate.HostCat = animalRepository.Get(catDate.HostId);
            catDate.GuestCat = animalRepository.Get(catDate.GuestId);
            ViewBag.Location = catDate.Location;
            return View("Thanks", catDate);
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
            AnimalCatVM animalCatVM = new AnimalCatVM();
            animalCatVM.CatDate = catDate;

            List<Cat> cats = this.animalRepository.Get();
            animalCatVM.CatSelectList = new SelectList(cats, "CatId", "Name");

            return View(animalCatVM);
        }

        // POST: CatDates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CatDate catDate)
        {
            ModelState.Remove("catDate.HostCat");
            ModelState.Remove("catDate.GuestCat");
            if (ModelState.IsValid)
            {
                try
                {
                    var hostCat = animalRepository.Get(catDate.HostId);
                    var guestCat = animalRepository.Get(catDate.GuestId);
                    catDate.HostCat = hostCat;
                    catDate.GuestCat = guestCat;

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
            AnimalCatVM animalCatVM = new AnimalCatVM();
            animalCatVM.CatDate = catDate;

            List<Cat> cats = this.animalRepository.Get();
            animalCatVM.CatSelectList = new SelectList(cats, "CatId", "Name");
            return View(animalCatVM);
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
