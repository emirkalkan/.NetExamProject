using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnimalCrossing.Models.ViewModels
{
    public class AnimalCatVM
    {
        public Cat Cat { get; set; }
        public SelectList SpeciesSelectList { get; set; }

        public CatDate CatDate { get; set; }
        public SelectList CatSelectList { get; set; }

        public Review Review { get; set; }

        public AnimalCatVM()
        {
        }
    }
}
