using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing;
using AnimalCrossing.Controllers;
using AnimalCrossing.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AnimalCrossingTests
{
    public class UnitTestCat
    {
        [Fact]
        public void TestIndexMethodReturnObjCat()
        {
            //Arrange
            var mockRepoCats = new Mock<IAnimalRepository>();
            var mockRepoSpecies = new Mock<ISpeciesRepository>();

            string test = "köpük";

            mockRepoCats.Setup(repo => repo.Get())
                .Returns(DataTestService.GetTestCats());

            mockRepoSpecies.Setup(repo => repo.Get())
                .Returns(DataTestService.GetTestSpecies());

            var controller = new AnimalController(mockRepoCats.Object, mockRepoSpecies.Object);
            //Act
            var result = controller.Index(test);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Cat>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }


    }
}

