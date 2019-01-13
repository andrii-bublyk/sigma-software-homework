using Academy.Controllers;
using Academy.ViewModels;
using DeepEqual.Syntax;
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
            var model = Assert.IsAssignableFrom<IEnumerable<Course>>(viewResult.Model);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Arrange
            var controller = new CourseController(null, null, null, null, null);

            //Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_RedirectToCourses()
        {
            // Arrange
            Course courseForCreating = new Course();

            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.CreateCourse(courseForCreating).Returns(true);
            var controller = new CourseController(courseSericeMock, null, null, null, null);
            
            // Act
            var result = controller.Create(courseForCreating);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Courses", redirectToActionResult.ActionName);
        }
        
        [Fact]
        public void Info_ReturnsViewResult_WithCource()
        {
            // Arrange
            int existingCourseId = 42;

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(existingCourseId).Returns(new Course());
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.Info(existingCourseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Course>(viewResult.Model);
        }

        [Fact]
        public void Info_ReturnsNotFound_WhenNonExistingCourseId()
        {
            // Arrange
            int nonExistingCourseId = 0;

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(nonExistingCourseId).Returns(default(Course));
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.Info(nonExistingCourseId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsViewResult_WithCource()
        {
            // Arrange
            int existingCourseId = 42;

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(existingCourseId).Returns(new Course());
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.Edit(existingCourseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Course>(viewResult.Model);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenNonExistingCourseId()
        {
            // Arrange
            int nonExistingCourseId = 0;

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(nonExistingCourseId).Returns(default(Course));
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.Edit(nonExistingCourseId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_RedirectToCourses()
        {
            // Arrange
            Course courseForEditing = new Course();

            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.CreateCourse(courseForEditing).Returns(true);
            var controller = new CourseController(courseSericeMock, null, null, null, null);

            // Act
            var result = controller.Edit(courseForEditing);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Courses", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_RedirectToCourses()
        {
            // Arrange
            int courseId = 42;

            var courseSericeMock = Substitute.For<CourseService>();
            courseSericeMock.DeleteCourse(courseId).Returns(true);
            var controller = new CourseController(courseSericeMock, null, null, null, null);

            // Act
            var result = controller.Delete(courseId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Courses", redirectToActionResult.ActionName);
        }

        [Fact]
        public void AssignStudents_Get_ReturnsViewResult_WithCourseStudentsAssignmentViewModel()
        {
            // Arrange
            const int courseId = 1;
            const string courseName = "test";
            const int assignedStudentId = 2;
            const int nonAssignedStudentId = 3;

            var allStudents = new List<Student>()
            {
                new Student() { Id = assignedStudentId },
                new Student() { Id = nonAssignedStudentId }
            };
            CourseStudentsAssignmentViewModel expectedModel = new CourseStudentsAssignmentViewModel()
            {
                Course = new Course()
                {
                    Id = courseId,
                    Name = courseName,
                    Students = new List<Student>()
                    {
                        new Student()
                        {
                            Id = assignedStudentId
                        }
                    }
                },
                StudentsAssignmentsList = new List<StudentAssignment>()
                {
                    new StudentAssignment()
                    {
                        Student = new Student()
                        {
                            Id = assignedStudentId
                        },
                        IsAssigned = true
                    },
                    new StudentAssignment()
                    {
                        Student = new Student()
                        {
                            Id = nonAssignedStudentId
                        },
                        IsAssigned = false
                    }
                }
            };

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(courseId).Returns(new Course()
            {
                Id = courseId,
                Name = courseName,
                Students = new List<Student>()
                {
                    new Student()
                    {
                        Id = assignedStudentId
                    }
                }
            });

            var studentService = Substitute.For<StudentService>();
            studentService.GetAllStudents().Returns(allStudents);

            var controller = new CourseController(courseServiceMock, studentService, null, null, null);

            // Act
            var result = controller.AssignStudents(courseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualModel = Assert.IsType<CourseStudentsAssignmentViewModel>(viewResult.Model);
            actualModel.WithDeepEqual(expectedModel).Assert();
        }

        [Fact]
        public void AssignStudents_Get_ReturnsNotFound_WhenNonExistingCourseId()
        {
            // Arrange
            int nonExistingCourseId = 0;

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(nonExistingCourseId).Returns(default(Course));
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.AssignStudents(nonExistingCourseId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AssignStudents_Post_RedirectToCourses()
        {
            // Arrange
            int courseId = 1;
            const int assignedStudentId = 2;
            const int nonAssignedStudentId = 3;
            Course course = new Course() { Id = courseId };
            CourseStudentsAssignmentViewModel model = new CourseStudentsAssignmentViewModel()
            {
                Course = course,
                StudentsAssignmentsList = new List<StudentAssignment>()
                {
                    new StudentAssignment()
                    {
                        Student = new Student()
                        {
                            Id = assignedStudentId
                        },
                        IsAssigned = true
                    },
                    new StudentAssignment()
                    {
                        Student = new Student()
                        {
                            Id = nonAssignedStudentId
                        },
                        IsAssigned = false
                    }
                }
            };

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.IsCourseExisted(course).Returns(true);
            courseServiceMock.AssignStudentsToCourse(courseId, new List<int>()).Returns(true);

            var studentServiceMock = Substitute.For<StudentService>();
            //studentServiceMock.IsStudentExisted(new Student()).Returns(true);

            var controller = new CourseController(courseServiceMock, studentServiceMock, null, null, null);

            // Act
            var result = controller.AssignStudents(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Courses", redirectToActionResult.ActionName);
        }

        //[Fact]
        public void AssignStudents_Post_ReturnsNotFound_WhenNonExistingCourseInModel()
        {
            // Arrange
            int nonExistingCourseId = 0;
            Course nonExistingCourse = new Course() { Id = nonExistingCourseId };
            CourseStudentsAssignmentViewModel model = new CourseStudentsAssignmentViewModel()
            {
                Course = nonExistingCourse,
                StudentsAssignmentsList = new List<StudentAssignment>()
            };

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.IsCourseExisted(nonExistingCourse).Returns(false);
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.AssignStudents(model);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AssignLecturers_Get_ReturnsViewResult_WithCourseLecturersAssignmentViewModel()
        {
            // Arrange
            const int courseId = 1;
            const int assignedLecturerId = 2;
            const int nonAssignedLecturerId = 3;

            var allLecturers = new List<Lecturer>()
            {
                new Lecturer() { Id = assignedLecturerId },
                new Lecturer() { Id = nonAssignedLecturerId }
            };
            CourseLecturersAssignmentViewModel expectedModel = new CourseLecturersAssignmentViewModel()
            {
                Course = new Course()
                {
                    Id = courseId,
                    Lecturers = new List<Lecturer>()
                    {
                        new Lecturer()
                        {
                            Id = assignedLecturerId
                        }
                    }
                },
                LecturersAssignmentsList = new List<LecturerAssignment>()
                {
                    new LecturerAssignment()
                    {
                        Lecturer = new Lecturer()
                        {
                            Id = assignedLecturerId
                        },
                        IsAssigned = true
                    },
                    new LecturerAssignment()
                    {
                        Lecturer = new Lecturer()
                        {
                            Id = nonAssignedLecturerId
                        },
                        IsAssigned = false
                    }
                }
            };

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(courseId).Returns(new Course()
            {
                Id = courseId,
                Lecturers = new List<Lecturer>()
                {
                    new Lecturer()
                    {
                        Id = assignedLecturerId
                    }
                }
            });

            var lecturerServise = Substitute.For<LecturerService>();
            lecturerServise.GetAllLecturers().Returns(allLecturers);

            var controller = new CourseController(courseServiceMock, null, null, null, lecturerServise);

            // Act
            var result = controller.AssignLecturers(courseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualModel = Assert.IsType<CourseLecturersAssignmentViewModel>(viewResult.Model);
            actualModel.WithDeepEqual(expectedModel).Assert();
        }

        [Fact]
        public void AssignLecturers_Get_ReturnsNotFound_WhenNonExistingCourseId()
        {
            // Arrange
            int nonExistingCourseId = 0;

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(nonExistingCourseId).Returns(default(Course));
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.AssignLecturers(nonExistingCourseId);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AssignLecturers_Post_RedirectToCourses()
        {
            // Arrange
            int courseId = 1;
            const int assignedLecturerId = 2;
            const int nonAssignedLecturertId = 3;
            Course course = new Course() { Id = courseId };
            CourseLecturersAssignmentViewModel model = new CourseLecturersAssignmentViewModel()
            {
                Course = course,
                LecturersAssignmentsList = new List<LecturerAssignment>()
                {
                    new LecturerAssignment()
                    {
                        Lecturer = new Lecturer()
                        {
                            Id = assignedLecturerId
                        },
                        IsAssigned = true
                    },
                    new LecturerAssignment()
                    {
                        Lecturer = new Lecturer()
                        {
                            Id = nonAssignedLecturertId
                        },
                        IsAssigned = false
                    }
                }
            };

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.IsCourseExisted(course).Returns(true);
            courseServiceMock.AssignStudentsToCourse(courseId, new List<int>()).Returns(true);

            var lecturerServiceMock = Substitute.For<LecturerService>();
            //lecturerServiceMock.IsStudentExisted(new Lecturer()).Returns(true);

            var controller = new CourseController(courseServiceMock, null, null, null, lecturerServiceMock);

            // Act
            var result = controller.AssignLecturers(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Courses", redirectToActionResult.ActionName);
        }

        //[Fact]
        public void AssignLecturers_Post_ReturnsNotFound_WhenNonExistingCourseInModel()
        {
            // Arrange
            int nonExistingCourseId = 0;
            Course nonExistingCourse = new Course() { Id = nonExistingCourseId };
            CourseLecturersAssignmentViewModel model = new CourseLecturersAssignmentViewModel()
            {
                Course = nonExistingCourse,
                LecturersAssignmentsList = new List<LecturerAssignment>()
            };

            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.IsCourseExisted(nonExistingCourse).Returns(false);
            var controller = new CourseController(courseServiceMock, null, null, null, null);

            // Act
            var result = controller.AssignLecturers(model);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
