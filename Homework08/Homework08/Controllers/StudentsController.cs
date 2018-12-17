using DataAccess.ADO;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework08.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Repository repository;

        public StudentsController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Students()
        {
            return View(repository.GetAllStudents());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student newStudent)
        {
            repository.CreateStudent(newStudent);
            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = repository.GetStudentById(id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            repository.UpdateStudent(student);
            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            repository.DeleteStudent(id);
            return RedirectToAction("Students");
        }
    }
}
