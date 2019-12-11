using System;
using System.Collections.Generic;
using AnimalCrossing.Models;

namespace AnimalCrossingTests
{
    public class DataTestService
    {
        public static List<Species> GetTestSpecies()
        {
            var sessions = new List<Species>();
            sessions.Add(new Species()
            {
                SpeciesId = 1,
                Name = "Maine coon"
            });
            sessions.Add(new Species()
            {
                SpeciesId = 2,
                Name = "Lynx"
            });
            return sessions;
        }

        public static List<Cat> GetTestCats()
        {
            var sessions = new List<Cat>();
            DateTime value = new DateTime(2017, 1, 18);
            sessions.Add(new Cat()
            {
                 CatId = 1,
                 Name = "Köpük",
                 Gender = Gender.Male,
                 Description = "Test",
                 ProfilePicture = "asdfghj",
                 BirthDate = value,
                 SpeciesId = 1,
            });
            sessions.Add(new Cat()
            {
                CatId = 2,
                Name = "Kitty",
                Gender = Gender.Male,
                Description = "Test",
                ProfilePicture = "asdfghj",
                BirthDate = value,
                SpeciesId = 1,
            });
            return sessions;
        }

        public static List<CatDate> GetTestCatDates()
        {
            var sessions = new List<CatDate>();
            DateTime value = new DateTime(2017, 1, 18);
            sessions.Add(new CatDate()
            {
                CatDateId =1,
                GuestId = 1,
                HostId =2,
                Location = "Istanbul",
                DateTime = value
            });
            sessions.Add(new CatDate()
            {
                CatDateId = 2,
                GuestId = 3,
                HostId = 2,
                Location = "Copenhagen",
                DateTime = value,
            });
            return sessions;
        }

        public DataTestService()
        {
        }
    }
}
