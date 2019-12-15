using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalCrossing.Data;
using AnimalCrossing.Models;
using AnimalCrossing.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimalCrossing.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private IAnimalRepository animalRepository;
        private ISpeciesRepository speciesRepository;
        private IReviewRepository reviewRepository;

        public AnimalController(IAnimalRepository animalRepo, ISpeciesRepository s, IReviewRepository r)
        {
            this.speciesRepository = s;
            this.animalRepository = animalRepo;
            this.reviewRepository = r;
        }


        // GET: /<controller>/
        [AllowAnonymous]
        public IActionResult Index(string searchString)
        {
            List<Cat> cats = this.animalRepository.Find(searchString);
            ViewBag.SearchString = searchString;

            List<Review> reviews = this.reviewRepository.Get().ToList();
            ViewBag.Review = reviews;

                return View("ShowCats", cats.ToList());
        }


        public string Hello()
        {
            return "Well, hello there! We are learning .NET Core now...";
        }

        public IActionResult MyFirstView()
        {
            ViewBag.MyWifeSays = "Go buy groceries, clean up, make dinner";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(ViewModelCreator.CreateAnimalCatVm(speciesRepository));
        }

        [HttpPost]
        public IActionResult Create(AnimalCatVM vm)
        {
            if (ModelState.IsValid) {
                ViewBag.Thanks = vm.Cat.Name;
                ViewBag.Cat = vm.Cat;

                animalRepository.Save(vm.Cat);

                if (vm.Cat.Gender == Gender.Male)
                {
                    IEnumerable<Cat> cats = this.animalRepository.Get().Where(c => c.SpeciesId == vm.Cat.SpeciesId &&
                    (c.Gender == Gender.Female || c.Gender == Gender.Other));
                    List<Cat> catList = cats.ToList();
                    return View("Thanks", catList);
                }
                else if (vm.Cat.Gender == Gender.Female)
                {
                    IEnumerable<Cat> cats = this.animalRepository.Get().Where(c => c.SpeciesId == vm.Cat.SpeciesId &&
                    (c.Gender == Gender.Other || c.Gender == Gender.Male));
                    List<Cat> catList = cats.ToList();
                    return View("Thanks", catList);
                }
                else
                {
                    IEnumerable<Cat> cats = this.animalRepository.Get().Where(c => c.SpeciesId == vm.Cat.SpeciesId &&
                    (c.Gender == Gender.Male || c.Gender == Gender.Female));
                    List<Cat> catList = cats.ToList();
                    return View("Thanks", catList);
                }
            }

            return View(ViewModelCreator.CreateAnimalCatVm(speciesRepository));

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Create an edit view
            // Look up cat object from catId in the database
            // Show an edit view to the user, displaying the cat object
            Cat cat = animalRepository.Get(id);
            AnimalCatVM animalCatVM = new AnimalCatVM();
            animalCatVM.Cat = cat;
            List<Species> species = this.speciesRepository.Get();
            animalCatVM.SpeciesSelectList = new SelectList(species, "SpeciesId", "Name");

            return View(animalCatVM);
        }

        [HttpPost]
        public IActionResult Edit(AnimalCatVM c)
        {
            if (ModelState.IsValid)
            {
                animalRepository.Save(c.Cat);
                // Save it to the database
                return RedirectToAction("Index");
            }
            AnimalCatVM animalCatVM = new AnimalCatVM();
            animalCatVM = c;
            List<Species> species = this.speciesRepository.Get();
            animalCatVM.SpeciesSelectList = new SelectList(species, "SpeciesId", "Name");

            return View(animalCatVM);
        }

        // GET: CatDates/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cat = animalRepository.Get((int)id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        // POST: CatDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            animalRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
