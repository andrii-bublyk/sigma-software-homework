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
    public class HometaskControllerTests
    {
        [Fact]
        public void CourseHometasks_ReturnsViewResult_WithListOfHometasks()
        {
            // Arrange
            int courseId = 1;
            string courseName = "testCouse";
            int hometaskId = 2;

            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.GetCourse(courseId).Returns(
                new Course()
                {
                    Id = courseId,
                    Name = courseName
                });

            var hometaskSericeMock = Substitute.For<HometaskService>();
            hometaskSericeMock.GetAllHomeTasks().Returns(new List<HomeTask>());
            hometaskSericeMock.GetHomeTask(hometaskId).Returns(new HomeTask());

            var controller = new HometaskController(hometaskSericeMock, courseSericeMock);

            //Act
            var result = controller.CourseHometasks(courseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(courseId, (int)viewResult.ViewData["courseId"]);
            Assert.Equal(courseName, viewResult.ViewData["courseName"]);
            var model = Assert.IsAssignableFrom<IEnumerable<HomeTask>>(viewResult.Model);
        }

        [Fact]
        public void CourseHometasks_ReturnsNotFound_WhenNonExistingCourseId()
        {
            // Arrange
            int nonExistingcourseId = 42;

            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.GetCourse(nonExistingcourseId).Returns(default(Course));

            var controller = new HometaskController(null, courseSericeMock);

            //Act
            var result = controller.CourseHometasks(nonExistingcourseId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Arrange
            int courseId = 1;
            string courseName = "testCourse";
            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.GetCourse(courseId).Returns(
                new Course()
                {
                    Id = courseId,
                    Name = courseName
                });

            var controller = new HometaskController(null, courseSericeMock);

            //Act
            var result = controller.Create(courseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(courseId, (int)viewResult.ViewData["courseId"]);
            Assert.Equal(courseName, viewResult.ViewData["courseName"]);
        }

        [Fact]
        public void Create_Get_ReturnsNotFound_WhenNonExistingCourseId()
        {
            // Arrange
            int nonExistingcourseId = 42;

            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.GetCourse(nonExistingcourseId).Returns(default(Course));

            var controller = new HometaskController(null, courseSericeMock);

            //Act
            var result = controller.CourseHometasks(nonExistingcourseId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Post_RedirectToCourseHometasks()
        {
            // Arrange
            HomeTask homeTaskForCreating = new HomeTask();

            var homeTaskSericeMock = Substitute.For<HometaskService>();
            homeTaskSericeMock.CreateHomeTask(homeTaskForCreating).Returns(true);
            var controller = new HometaskController(homeTaskSericeMock, null);

            // Act
            var result = controller.Create(homeTaskForCreating);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("CourseHometasks", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithHometask()
        {
            // Arrange
            int existingHometaskId = 1;

            var hometaskServiceMock = Substitute.For<HometaskService>();
            hometaskServiceMock.GetHomeTask(existingHometaskId).Returns(new HomeTask());
            var controller = new HometaskController(hometaskServiceMock, null);

            // Act
            var result = controller.Edit(existingHometaskId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<HomeTask>(viewResult.Model);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenNonExistingHometaskId()
        {
            // Arrange
            int nonExistingHometaskId = 0;

            var hometaskServiceMock = Substitute.For<HometaskService>();
            hometaskServiceMock.GetHomeTask(nonExistingHometaskId).Returns(default(HomeTask));
            var controller = new HometaskController(hometaskServiceMock, null);

            // Act
            var result = controller.Edit(nonExistingHometaskId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_RedirectToCourseHometasks()
        {
            // Arrange
            HomeTask homeTaskForEditing = new HomeTask();

            var homeTaskSericeMock = Substitute.For<HometaskService>();
            homeTaskSericeMock.UpdateHomeTask(homeTaskForEditing).Returns(true);
            var controller = new HometaskController(homeTaskSericeMock, null);

            // Act
            var result = controller.Edit(homeTaskForEditing);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("CourseHometasks", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_RedirectToCourseHometasks()
        {
            // Arrange
            int hometaskId = 1;

            var hometaskServiceMock = Substitute.For<HometaskService>();
            hometaskServiceMock.GetHomeTask(hometaskId).Returns(new HomeTask());
            hometaskServiceMock.DeleteHomeTask(hometaskId).Returns(true);
            var controller = new HometaskController(hometaskServiceMock, null);

            // Act
            var result = controller.Delete(hometaskId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("CourseHometasks", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenNonExistingHometaskId()
        {
            // Arrange
            int hometaskId = 1;

            var hometaskServiceMock = Substitute.For<HometaskService>();
            hometaskServiceMock.GetHomeTask(hometaskId).Returns(default(HomeTask));
            var controller = new HometaskController(hometaskServiceMock, null);

            // Act
            var result = controller.Delete(hometaskId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
