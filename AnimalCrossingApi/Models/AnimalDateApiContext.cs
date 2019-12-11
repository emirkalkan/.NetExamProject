using Microsoft.EntityFrameworkCore;

namespace AnimalCrossingApi.Models
{
    public class AnimalDateApiContext : DbContext
    {
        public AnimalDateApiContext(DbContextOptions<AnimalDateApiContext> options)
            : base(options)
        {
        }

        public DbSet<AnimalCrossing.Models.CatDate> CatDates { get; set; }
    }
}