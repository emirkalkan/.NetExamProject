using System;
namespace AnimalCrossingApi.Models
{
    public class CatDate
    {
        public int CatDateId { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        public int HostId { get; set; }
        public int GuestId { get; set; }

        public CatDate()
        {
        }
    }
}
