using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalCrossing.Models.ViewModels
{
    public class ViewModelCreator
    {
        
        public static AnimalCatVM CreateAnimalCatVm(ISpeciesRepository speciesRepository)
        {
            return new AnimalCatVM()
            {
                Cat = new Cat(),
                SpeciesSelectList = new SelectList(speciesRepository.Get(), "SpeciesId", "Name")
            };
            
        }

        public static AnimalCatVM CreateAnimalCatDateVm(IAnimalRepository animalRepository)
        {
            return new AnimalCatVM()
            {
                CatDate = new CatDate(),
                CatSelectList = new SelectList(animalRepository.Get(), "CatId", "Name")
            };
        }
            public ViewModelCreator()
        {
        }
    }
}
