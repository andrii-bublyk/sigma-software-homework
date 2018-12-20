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
    public class HomeTaskAssessmentController : Controller
    {
        private readonly IRepository repository;

        public HomeTaskAssessmentController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult StudentAssessments(int studentId)
        {
            Student student = repository.GetStudentById(studentId);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["studentId"] = student.Id;
            ViewData["studentName"] = student.Name;

            var studentAssessments = repository.GetHomeTaskAssessmentsByStudentId(studentId);

            return View(studentAssessments);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            HomeTaskAssessment assessment = repository.GetHomeTaskAssessmentById(id);
            return View(assessment);
        }

        [HttpPost]
        public IActionResult Edit(HomeTaskAssessment assessment)
        {
            repository.UpdateHomeTaskAssessments(new List<HomeTaskAssessment>() { assessment });
            return RedirectToAction("StudentAssessments", new { studentId = assessment.Student.Id });
        }
    }
}
