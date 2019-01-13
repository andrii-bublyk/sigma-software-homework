using Academy.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.AcademyModels;
using NSubstitute;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Academy.Tests
{
    public class LecturerControllerTests
    {
        [Fact]
        public void Lecturers_ReturnsViewResult_WithListOfLecturers()
        {
            // Arrange
            var lecturerServiceMock = Substitute.For<LecturerService>();
            lecturerServiceMock.GetAllLecturers().Returns(new List<Models.AcademyModels.Lecturer>());

            var controller = new LecturerController(lecturerServiceMock);

            //Act
            var result = controller.Lecturers();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Lecturer>>(viewResult.Model);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Arrange
            var controller = new LecturerController(null);

            //Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_RedirectToLecturers()
        {
            // Arrange
            Lecturer lecturerForCreating = new Lecturer();

            var lecturerServiceMock = Substitute.For<LecturerService>();
            lecturerServiceMock.CreateLecturer(lecturerForCreating).Returns(true);

            var controller = new LecturerController(lecturerServiceMock);

            // Act
            var result = controller.Create(lecturerForCreating);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Lecturers", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithLecturer()
        {
            // Arrange
            int existingLecturerId = 1;

            var lecturerServiceMock = Substitute.For<LecturerService>();
            lecturerServiceMock.GetLecturer(existingLecturerId).Returns(new Lecturer());
            var controller = new LecturerController(lecturerServiceMock);

            // Act
            var result = controller.Edit(existingLecturerId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Lecturer>(viewResult.Model);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenNonExistingLecturerId()
        {
            // Arrange
            int nonExistingLecturerId = 0;

            var lecturerServiceMock = Substitute.For<LecturerService>();
            lecturerServiceMock.GetLecturer(nonExistingLecturerId).Returns(default(Lecturer));
            var controller = new LecturerController(lecturerServiceMock);

            // Act
            var result = controller.Edit(nonExistingLecturerId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_RedirectToLecturers()
        {
            // Arrange
            Lecturer lecturerForEditing = new Lecturer();

            var lecturerSericeMock = Substitute.For<LecturerService>();
            lecturerSericeMock.UpdateLecturer(lecturerForEditing).Returns(true);
            var controller = new LecturerController(lecturerSericeMock);

            // Act
            var result = controller.Edit(lecturerForEditing);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Lecturers", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_RedirectToLecturers()
        {
            // Arrange
            int lecturerId = 1;

            var lecturerServiceMock = Substitute.For<LecturerService>();
            lecturerServiceMock.DeleteLecturer(lecturerId).Returns(true);
            var controller = new LecturerController(lecturerServiceMock);

            // Act
            var result = controller.Delete(lecturerId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Lecturers", redirectToActionResult.ActionName);
        }
    }
}
