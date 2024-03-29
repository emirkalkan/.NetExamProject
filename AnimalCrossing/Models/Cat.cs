﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;

namespace AnimalCrossing.Models
{
    public class Cat
    {
        // C# properties = java attribute and public get and set.
        //private int JavaCatId;

        //public int getJavaCatId()
        //{
        //    return this.JavaCatId;
        //}
        //public void setJavaCatId(int catId)
        //{
        //    this.JavaCatId = catId;
        //}


        // If the name is *class_name*Id
        public int CatId { get; set; }

        
        [Required(ErrorMessage = "All cats must have a name to be a pet.")]
        public string Name { get; set; }

        // Later, create 1-to-many relationship to Species table
        //public string Species { get; set; }

        [Required(ErrorMessage = "All cats must have a gender option.")]
        public Gender? Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "All cats must have a picture for identification.")]
        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "All cats must have a description.")]
        [StringLength(100, MinimumLength = 10)]
        public string Description { get; set; }

        // Ratings..Comments, Reviews
        // 
        public List<Review> Reviews { get; set; }

        public int SpeciesId { get; set; }

        //[Required(ErrorMessage = "All cats must have a species.")]
        public Species Species { get; set; }

        public Cat()
        {
        }

    }
    
    public enum Gender { Male = 0, Female = 1, Other = 2 };
}
