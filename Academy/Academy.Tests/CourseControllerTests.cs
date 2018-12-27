using Academy.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.AcademyModels;
using NSubstitute;
using Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Academy.Tests
{
    public class CourseControllerTests
    {
        [Fact]
        public void Courses_ReturnsViewResult_WithListOfCourses()
        {
            // Arrange
            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.GetAllCourses().Returns(new List<Course>() { new Course(), new Course() });
            var controller = new CourseController(courseSericeMock, null, null, null, null);

            //Act
            var result = controller.Courses();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Course>>(viewResult.Model);
        }
    }
}
