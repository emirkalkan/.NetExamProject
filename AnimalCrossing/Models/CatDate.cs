﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalCrossing.Models
{
    public class CatDate
    {
        public int CatDateId { get; set; }

        [ForeignKey("CatId")]
        public int HostId { get; set; } // CatId
        public Cat HostCat { get; set; }

        [ForeignKey("CatId")]
        public int GuestId { get; set; } //CatId
        public Cat GuestCat { get; set; }

        public string Location { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy hh:mm}")]
        public DateTime DateTime { get; set; }

       

        public CatDate()
        {
        }
    }
}