using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalCrossing.Models
{
    public class CatDate
    {
        public int CatDateId { get; set; }

        
        [ForeignKey("CatId")]
        [Required(ErrorMessage = "Please select a host cat.")]
        public int HostId { get; set; } // CatId

        //[Required(ErrorMessage = "Please select a host cat.")]
        public Cat HostCat { get; set; }

        [ForeignKey("CatId")]
        [Required(ErrorMessage = "Please select a guest cat.")]
        public int GuestId { get; set; } //CatId

        //[Required(ErrorMessage = "Please select a guest cat.")]
        public Cat GuestCat { get; set; }

        [Required(ErrorMessage = "Please select location.")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        //DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DateTime { get; set; }

       

        public CatDate()
        {
        }
    }
}
