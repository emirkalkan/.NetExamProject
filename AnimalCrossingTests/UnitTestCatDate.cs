﻿using System;
using System.Collections.Generic;
using System.Linq;
using AnimalCrossing;
using AnimalCrossing.Controllers;
using AnimalCrossing.Models;
using AnimalCrossing.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AnimalCrossingTests
{
    public class UnitTestCatDate
    {
        DateTime value = new DateTime(2017, 1, 18);

        [Fact]
        public void TestIndexMethodReturnsObjects()
        {
            // Arrange
            string search = "";

            var mockRepoCatDate = new Mock<ICatDateRepository>();
            mockRepoCatDate.Setup(repo => repo.Find(search))
                .Returns(DataTestService.GetTestCatDates());
            var mockRepoAnimal = new Mock<IAnimalRepository>();
            mockRepoAnimal.Setup(repo => repo.Get())
                .Returns(DataTestService.GetTestCats());

            var controller = new CatDatesController(mockRepoCatDate.Object, mockRepoAnimal.Object);

            // Act
            var result = controller.Index(search);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CatDate>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        //[Fact]
        //public void CreatePost_ReturnsViewWithCatDates_WhenModelStateIsInvalid()
        //{
        //    // Arrange
        //    var mockRepoCatDate = new Mock<ICatDateRepository>();
        //    var mockRepoAnimal = new Mock<IAnimalRepository>();
            
        //    var controller = new CatDatesController(mockRepoCatDate.Object, mockRepoAnimal.Object);

        //    //ViewModelCreator viewModel = new ViewModelCreator();

        //    controller.ModelState.AddModelError("Location", "Required");

        //    CatDate catDate = new CatDate() { Location = "", CatDateId = 1, GuestId = 2, HostId = 1, DateTime = value };

        //    // Act
        //    var result = controller.Create(catDate);

        //    // Assert
        //    //var viewResult = Assert.IsType<ViewResult>(result);
        //    //Assert.IsType<ViewModelCreator>(ViewModelCreator.CreateAnimalCatDateVm(mockRepoAnimal.Object));
        //    //var viewResult = Assert.IsType<ViewResult>(result);
        //    //var model = Assert.IsAssignableFrom<CatDate>(viewResult.ViewData.Model);
        //    //Assert.IsType<CatDate>(model);
        //}

        [Fact]
        public void CreatePost_SaveThroughRepository_WhenModelStateIsValid()
        {
            //Arrange
            var mockRepoCatDate = new Mock<ICatDateRepository>();
            var mockRepoAnimal = new Mock<IAnimalRepository>();

            mockRepoCatDate.Setup(repo => repo.Save(It.IsAny<CatDate>()))
                .Verifiable();
            var controller = new CatDatesController(mockRepoCatDate.Object, mockRepoAnimal.Object);
            CatDate catDate = new CatDate()
            {
                Location = "Istanbul",
                CatDateId = 1,
                GuestId = 2,
                HostId = 1,
                DateTime = value
            };

            //Act
            var result = controller.Create(catDate);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Thanks", redirectToActionResult.ActionName);
            mockRepoCatDate.Verify();

        }

        [Fact]
        public void ThanksMethod()
        {
            // Arrange
            var mockRepoCatDate = new Mock<ICatDateRepository>();
            var mockRepoAnimal = new Mock<IAnimalRepository>();
            var controller = new CatDatesController(mockRepoCatDate.Object, mockRepoAnimal.Object);

            CatDate catDate = new CatDate() { Location = "Istanbul", CatDateId = 1, GuestId = 2, HostId = 1, DateTime = value };
            // Act
            var result = controller.Thanks(catDate);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatDate>(viewResult.ViewData.Model);
            Assert.IsType<CatDate>(model);
        }

        [Fact]
        public void TestDeleteMethodTest()
        {
            var mockRepoCatDate = new Mock<ICatDateRepository>();
            int id = 1;
            mockRepoCatDate.Setup(repo => repo.Get())
               .Returns(DataTestService.GetTestCatDates());
            var mockRepoAnimal = new Mock<IAnimalRepository>();

            var controller = new CatDatesController(mockRepoCatDate.Object, mockRepoAnimal.Object);

            // Act
            var result = controller.DeleteConfirmed(id);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepoCatDate.Verify();
        }



    }
}

