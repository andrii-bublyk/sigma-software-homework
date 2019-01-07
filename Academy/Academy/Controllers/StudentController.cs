using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.AcademyModels;
using Services;

namespace Academy.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService studentService;

        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Students()
        {
            var allStudents = studentService.GetAllStudents();
            return View(allStudents);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create(Student newStudent)
        {
            studentService.CreateStudent(newStudent);
            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Info(int id)
        {
            Student student = studentService.GetStudent(id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            Student student = studentService.GetStudent(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Student student)
        {
            studentService.UpdateStudent(student);
            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            studentService.DeleteStudent(id);
            return RedirectToAction("Students");
        }
    }
}