using DataAccess.ADO;
using Homework08.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework08.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IRepository repository;

        public CoursesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Courses()
        {
            return View(repository.GetAllCourses());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course newCourse)
        {
            repository.CreateCourse(newCourse);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course = repository.GetCourse(id);
            if (course == null)
                return NotFound();
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            repository.UpdateCourse(course);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            repository.DeleteCourse(id);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public IActionResult AssignStudents(int id)
        {
            Course course = repository.GetCourse(id);
            List<Student> allStudents = repository.GetAllStudents();

            CourseStudentsAssignmentViewModel model = new CourseStudentsAssignmentViewModel();
            model.Course = course;
            model.StudentsList = new List<StudentAssignment>();
            foreach (var student in allStudents)
            {
                bool isAssigned = course.Students.Any(c => c.Id == student.Id);
                model.StudentsList.Add(new StudentAssignment() { Student = student, IsAssigned = isAssigned });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignStudents(CourseStudentsAssignmentViewModel model)
        {
            var assignedStudentsId = model.StudentsList.Where(a => a.IsAssigned)
                .Select(s => s.Student.Id);
            repository.SetStudentsToCourse(model.Course.Id, assignedStudentsId);

            var courseHometasksIds = repository.GetCourse(model.Course.Id).HomeTasks
                .Select(h => h.Id);

            foreach (var studentId in assignedStudentsId)
            {
                var studentHometaskIds = repository.GetStudentById(studentId, true).HomeTaskAssessments
                    .Select(a => a.HomeTask.Id);

                var missingHometasksId = courseHometasksIds.Except(studentHometaskIds);

                List<HomeTaskAssessment> assessments = new List<HomeTaskAssessment>();
                foreach (var hometaskId in missingHometasksId)
                {
                    HomeTaskAssessment assessment = new HomeTaskAssessment()
                    {
                        IsComplete = false,
                        Date = DateTime.Now,
                        HomeTask = new HomeTask() { Id = hometaskId },
                        Student = new Student() { Id = studentId }
                    };

                    assessments.Add(assessment);
                }

                repository.CreateHomeTaskAssessments(assessments);
            }

            return RedirectToAction("Courses");
        }

        [HttpGet]
        public IActionResult AssignLecturers(int id)
        {
            Course course = repository.GetCourse(id);
            List<Lecturer> allLecturers = repository.GetAllLecturers();

            CourseLecturersAssignmentViewModel model = new CourseLecturersAssignmentViewModel();
            model.Course = course;
            model.LecturersList = new List<LecturerAssignment>();
            foreach (var lecturer in allLecturers)
            {
                bool isAssigned = course.Lecturers.Any(c => c.Id == lecturer.Id);
                model.LecturersList.Add(new LecturerAssignment() { Lecturer = lecturer, IsAssigned = isAssigned });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AssignLecturers(CourseLecturersAssignmentViewModel model)
        {
            var assignedLecturersId = model.LecturersList.Where(a => a.IsAssigned).Select(l => l.Lecturer.Id);
            repository.SetLecturersToCourse(model.Course.Id, assignedLecturersId);
            return RedirectToAction("Courses");
        }
    }
}
