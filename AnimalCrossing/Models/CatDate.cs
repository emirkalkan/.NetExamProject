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

        public Cat HostCat { get; set; }

        [ForeignKey("CatId")]
        [Required(ErrorMessage = "Please select a guest cat.")]
        public int GuestId { get; set; } //CatId

        public Cat GuestCat { get; set; }

        [Required(ErrorMessage = "Please select location.")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        //[DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DateTime { get; set; }

       

        public CatDate()
        {
        }
    }
}
