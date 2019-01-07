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
    public class HomeTaskAssessmentControllerTests
    {
        [Fact]
        public void StudentAssessments_ReturnsViewResult_WithStudentAssessments()
        {
            // Arrange
            int studentId = 1;
            string studentName = "testStudent";
            int homeTaskAssessment1Id = 2;
            int homeTaskAssessment2Id = 3;

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.GetStudent(studentId).Returns(
                new Student()
                {
                    Id = studentId,
                    Name = studentName
                });

            var homeTaskAssessmentServiceMock = Substitute.For<HomeTaskAssessmentService>();
            homeTaskAssessmentServiceMock.GetHomeTaskAssessmentsByStudentId(studentId).Returns(
                new List<HomeTaskAssessment>()
                {
                    new HomeTaskAssessment() { Id = homeTaskAssessment1Id },
                    new HomeTaskAssessment() { Id = homeTaskAssessment2Id },
                });

            var controller = new HomeTaskAssessmentController(homeTaskAssessmentServiceMock, studentServiceMock);

            //Act
            var result = controller.StudentAssessments(studentId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(studentId, (int)viewResult.ViewData["studentId"]);
            Assert.Equal(studentId, (int)viewResult.ViewData["studentId"]);
            var model = Assert.IsAssignableFrom<IEnumerable<HomeTaskAssessment>>(viewResult.Model);
        }

        [Fact]
        public void StudentAssessments_ReturnsNotFound_WhenNonExistingStudentId()
        {
            // Arrange
            int nonExistingStudentId = 1;

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.GetStudent(nonExistingStudentId).Returns(default(Student));

            var controller = new HomeTaskAssessmentController(null, studentServiceMock);

            //Act
            var result = controller.StudentAssessments(nonExistingStudentId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithHomeTaskAssessment()
        {
            // Arrange
            int existingHometaskAssessmentId = 42;

            var hometaskAssessmentServiceMock = Substitute.For<HomeTaskAssessmentService>();
            hometaskAssessmentServiceMock.GetHomeTaskAssessment(existingHometaskAssessmentId).Returns(new HomeTaskAssessment());
            var controller = new HomeTaskAssessmentController(hometaskAssessmentServiceMock, null);

            // Act
            var result = controller.Edit(existingHometaskAssessmentId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<HomeTaskAssessment>(viewResult.Model);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenNonExistingAssessmentId()
        {
            // Arrange
            int nonExistingAssessmentId = 0;

            var assessmentServiceMock = Substitute.For<HomeTaskAssessmentService>();
            assessmentServiceMock.GetHomeTaskAssessment(nonExistingAssessmentId).Returns(default(HomeTaskAssessment));
            var controller = new HomeTaskAssessmentController(assessmentServiceMock, null);

            // Act
            var result = controller.Edit(nonExistingAssessmentId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_RedirectToStudentAssessments()
        {
            // Arrange
            HomeTaskAssessment assessment = new HomeTaskAssessment();

            var homeTaskAssessmentSericeMock = Substitute.For<HomeTaskAssessmentService>();
            homeTaskAssessmentSericeMock.UpdateHomeTaskAssessment(assessment).Returns(true);
            var controller = new HomeTaskAssessmentController(homeTaskAssessmentSericeMock, null);

            // Act
            var result = controller.Edit(assessment);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("StudentAssessments", redirectToActionResult.ActionName);
        }
    }
}
