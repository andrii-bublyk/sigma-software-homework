using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ADO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;

namespace Homework08.Controllers
{
    public class LecturersController : Controller
    {
        private readonly IRepository repository;

        public LecturersController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Lecturers()
        {
            return View(repository.GetAllLecturers());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Lecturer newLecturer)
        {
            repository.CreateLecturer(newLecturer);
            return RedirectToAction("Lecturers");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Lecturer lecturer = repository.GetLecturerById(id);
            if (lecturer == null)
                return NotFound();
            return View(lecturer);
        }

        [HttpPost]
        public IActionResult Edit(Lecturer lecturer)
        {
            repository.UpdateLecturer(lecturer);
            return RedirectToAction("Lecturers");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            repository.DeleteLecturer(id);
            return RedirectToAction("Lecturers");
        }
    }
}