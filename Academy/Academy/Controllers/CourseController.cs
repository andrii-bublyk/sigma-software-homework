using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academy.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
            this.homeTaskAssessmentService = homeTaskAssessmentService;
            this.lecturerService = lecturerService;
        }

        [HttpGet]
        public IActionResult Courses()
        {
            var coursesList = courseService.GetAllCourses();
            return View(coursesList);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create(Course newCourse)
        {
            courseService.CreateCourse(newCourse);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Info(int id)
        {
            Course course = courseService.GetCourse(id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            Course course = courseService.GetCourse(id);
            if (course == null)
                return NotFound();
            return View(course);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Course course)
        {
            courseService.UpdateCourse(course);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            courseService.DeleteCourse(id);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AssignStudents(int id)
        {
            Course course = courseService.GetCourse(id);
            if (course == null)
                return NotFound();

            List<Student> allStudents = studentService.GetAllStudents();

            CourseStudentsAssignmentViewModel model = new CourseStudentsAssignmentViewModel();
            model.Course = course;
            model.StudentsAssignmentsList = new List<StudentAssignment>();
            foreach (var student in allStudents)
            {
                bool isAssigned = course.Students.Any(c => c.Id == student.Id);
                model.StudentsAssignmentsList.Add(new StudentAssignment() { Student = student, IsAssigned = isAssigned });
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AssignStudents(CourseStudentsAssignmentViewModel model)
        {
            // todo check
            //if (!courseService.IsCourseExisted(model.Course))
            //    return NotFound();

            //foreach(var studentAssignment in model.StudentsAssignmentsList)
            //{
            //    if (!studentService.IsStudentExisted(studentAssignment.Student))
            //        return NotFound();
            //}

            var assignedStudentsId = model.StudentsAssignmentsList.Where(al => al.IsAssigned).Select(s => s.Student.Id).ToList();
            courseService.AssignStudentsToCourse(model.Course.Id, assignedStudentsId);

            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult AssignLecturers(int id)
        {
            Course course = courseService.GetCourse(id);
            if (course == null)
                return NotFound();

            List<Lecturer> allLecturers = lecturerService.GetAllLecturers();

            CourseLecturersAssignmentViewModel model = new CourseLecturersAssignmentViewModel();
            model.Course = course;
            model.LecturersAssignmentsList = new List<LecturerAssignment>();
            foreach (var lecturer in allLecturers)
            {
                bool isAssigned = course.Lecturers.Any(c => c.Id == lecturer.Id);
                model.LecturersAssignmentsList.Add(new LecturerAssignment() { Lecturer = lecturer, IsAssigned = isAssigned });
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult AssignLecturers(CourseLecturersAssignmentViewModel model)
        {
            // todo check
            //if (!courseService.IsCourseExisted(model.Course))
            //    return NotFound();

            //foreach (var lecturerAssignment in model.LecturersAssignmentsList)
            //{
            //    if (!lecturerService.IsLecturerExisted(lecturerAssignment.Lecturer))
            //        return NotFound();
            //}

            var assignedLecturersId = model.LecturersAssignmentsList.Where(a => a.IsAssigned).Select(l => l.Lecturer.Id).ToList();
            courseService.AssignLecturersToCourse(model.Course.Id, assignedLecturersId);
            return RedirectToAction("Courses");
        }
    }
}