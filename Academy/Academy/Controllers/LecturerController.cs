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
    public class LecturerController : Controller
    {
        private readonly LecturerService lecturerService;

        public LecturerController(LecturerService lecturerService)
        {
            this.lecturerService = lecturerService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Lecturers()
        {
            return View(lecturerService.GetAllLecturers());
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Create(Lecturer newLecturer)
        {
            lecturerService.CreateLecturer(newLecturer);
            return RedirectToAction("Lecturers");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            Lecturer lecturer = lecturerService.GetLecturer(id);
            if (lecturer == null)
                return NotFound();
            return View(lecturer);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Lecturer lecturer)
        {
            lecturerService.UpdateLecturer(lecturer);
            return RedirectToAction("Lecturers");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            lecturerService.DeleteLecturer(id);
            return RedirectToAction("Lecturers");
        }
    }
}