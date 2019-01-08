using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models.AcademyModels;
using Services;

namespace Academy.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService courseService;
        private readonly StudentService studentService;
        private readonly HometaskService hometaskService;
        private readonly HomeTaskAssessmentService homeTaskAssessmentService;
        private readonly LecturerService lecturerService;

        public CourseController(CourseService courseService, StudentService studentService, HometaskService hometaskService,
            HomeTaskAssessmentService homeTaskAssessmentService, LecturerService lecturerService)
        {
            this.courseService = courseService;
            this.studentService = studentService;
            this.hometaskService = hometaskService;
            this.lecturerService = lecturerService;
        }

        [HttpGet]
        public IActionResult Courses()
        {
            var coursesList = courseService.GetAllCourses();
            return View(coursesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course newCourse)
        {
            courseService.CreateCourse(newCourse);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course = courseService.GetCourse(id);
            if (course == null)
                return NotFound();
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            courseService.UpdateCourse(course);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            courseService.DeleteCourse(id);
            return RedirectToAction("Courses");
        }

        //[HttpGet]
        //public IActionResult AssignStudents(int id)
        //{
        //    Course course = courseService.GetCourse(id);
        //    List<Student> allStudents = studentService.GetAllStudents();

        //    CourseStudentsAssignmentViewModel model = new CourseStudentsAssignmentViewModel();
        //    model.Course = course;
        //    model.StudentsAssignmentsList = new List<StudentAssignment>();
        //    foreach (var student in allStudents)
        //    {
        //        bool isAssigned = course.Students.Any(c => c.Id == student.Id);
        //        model.StudentsAssignmentsList.Add(new StudentAssignment() { Student = student, IsAssigned = isAssigned });
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public IActionResult AssignStudents(CourseStudentsAssignmentViewModel model)
        {
            var assignedStudentsId = model.StudentsAssignmentsList.Where(al => al.IsAssigned).Select(s => s.Student.Id).ToList();
            courseService.AssignStudentsToCourse(model.Course.Id, assignedStudentsId);

            // auto creating marks
            var courseHometasksIds = hometaskService.GetAllHomeTasks().Where(h => h.CourseId == model.Course.Id).Select(h => h.Id);
            foreach (var studentId in assignedStudentsId)
            {
                var studentHometaskIds = studentService.GetStudent(studentId).HomeTaskAssessments.Select(a => a.HomeTaskId);
                var missingHometasksId = courseHometasksIds.Except(studentHometaskIds);
                foreach (var hometaskId in missingHometasksId)
                {
                    HomeTaskAssessment assessment = new HomeTaskAssessment()
                    {
                        IsComplete = false,
                        Date = DateTime.Now,
                        HomeTaskId = hometaskId,
                        StudentId = studentId
                    };
                    homeTaskAssessmentService.CreateHomeTaskAssessment(assessment);
                }
            }

            return RedirectToAction("Courses");
        }

        //[HttpGet]
        //public IActionResult AssignLecturers(int id)
        //{
        //    Course course = courseService.GetCourse(id);
        //    List<Lecturer> allLecturers = lecturerService.GetAllLecturers();

        //    CourseLecturersAssignmentViewModel model = new CourseLecturersAssignmentViewModel();
        //    model.Course = course;
        //    model.LecturersAssignmentsList = new List<LecturerAssignment>();
        //    foreach (var lecturer in allLecturers)
        //    {
        //        bool isAssigned = course.Lecturers.Any(c => c.Id == lecturer.Id);
        //        model.LecturersAssignmentsList.Add(new LecturerAssignment() { Lecturer = lecturer, IsAssigned = isAssigned });
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public IActionResult AssignLecturers(CourseLecturersAssignmentViewModel model)
        {
            var assignedLecturersId = model.LecturersAssignmentsList.Where(a => a.IsAssigned).Select(l => l.Lecturer.Id).ToList();
            courseService.AssignLecturersToCourse(model.Course.Id, assignedLecturersId);
            return RedirectToAction("Courses");
        }
    }
}