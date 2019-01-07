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
    public class StudentControllerTests
    {
        [Fact]
        public void Students_ReturnsViewResult_WithListOfStudents()
        {
            // Arrange
            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.GetAllStudents().Returns(new List<Student>());

            var controller = new StudentController(studentServiceMock);

            //Act
            var result = controller.Students();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Student>>(viewResult.Model);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Arrange
            var controller = new StudentController(null);

            //Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_RedirectToStudents()
        {
            // Arrange
            Student studentForCreating = new Student();

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.CreateStudent(studentForCreating).Returns(true);

            var controller = new StudentController(studentServiceMock);

            // Act
            var result = controller.Create(studentForCreating);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Students", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Info_ReturnsViewResult_WithStudent()
        {
            // Arrange
            int existingStudentId = 1;

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.GetStudent(existingStudentId).Returns(new Student());
            var controller = new StudentController(studentServiceMock);

            // Act
            var result = controller.Info(existingStudentId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Student>(viewResult.Model);
        }

        [Fact]
        public void Info_ReturnsNotFound_WhenNonExistingStudentId()
        {
            // Arrange
            int nonExistingStudentId = 0;

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.GetStudent(nonExistingStudentId).Returns(default(Student));
            var controller = new StudentController(studentServiceMock);

            // Act
            var result = controller.Edit(nonExistingStudentId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithStudent()
        {
            // Arrange
            int existingStudentId = 1;

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.GetStudent(existingStudentId).Returns(new Student());
            var controller = new StudentController(studentServiceMock);

            // Act
            var result = controller.Edit(existingStudentId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Student>(viewResult.Model);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenNonExistingStudentId()
        {
            // Arrange
            int nonExistingStudentId = 0;

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.GetStudent(nonExistingStudentId).Returns(default(Student));
            var controller = new StudentController(studentServiceMock);

            // Act
            var result = controller.Edit(nonExistingStudentId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_RedirectToStudents()
        {
            // Arrange
            Student studentForEditing = new Student();

            var studentSericeMock = Substitute.For<StudentService>();
            studentSericeMock.UpdateStudent(studentForEditing).Returns(true);
            var controller = new StudentController(studentSericeMock);

            // Act
            var result = controller.Edit(studentForEditing);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Students", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_RedirectToStudents()
        {
            // Arrange
            int studentId = 1;

            var studentServiceMock = Substitute.For<StudentService>();
            studentServiceMock.DeleteStudent(studentId).Returns(true);
            var controller = new StudentController(studentServiceMock);

            // Act
            var result = controller.Delete(studentId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Students", redirectToActionResult.ActionName);
        }
    }
}
