using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.AcademyModels;
using Services;

namespace Academy.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService courseService;

        public CourseController(CourseService courseService)
        {
            this.courseService = courseService;
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
    }
}