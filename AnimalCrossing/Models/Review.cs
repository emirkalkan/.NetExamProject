using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalCrossing.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Range(1, 5, ErrorMessage = "Please enter a number between 1 to 5.")]
        public int Rating { get; set; } //1-5 stars

        [DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }

        public int ReviewingCatId { get; set; }
        public Cat ReviewingCat { get; set; }

        public string Comment { get; set; }

        public Review()
        {
        }
    }
}
