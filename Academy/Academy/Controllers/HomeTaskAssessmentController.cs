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
    public class HomeTaskAssessmentController : Controller
    {
        private readonly HomeTaskAssessmentService homeTaskAssessmentService;
        private readonly StudentService studentService;

        public HomeTaskAssessmentController(HomeTaskAssessmentService homeTaskAssessmentService, StudentService studentService)
        {
            this.homeTaskAssessmentService = homeTaskAssessmentService;
            this.studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult StudentAssessments(int studentId)
        {
            Student student = studentService.GetStudent(studentId);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["studentId"] = student.Id;
            ViewData["studentName"] = student.Name;

            List<HomeTaskAssessment> studentAssessments = homeTaskAssessmentService.GetHomeTaskAssessmentsByStudentId(studentId);

            return View(studentAssessments);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            HomeTaskAssessment assessment = homeTaskAssessmentService.GetHomeTaskAssessment(id);
            if (assessment == null)
                return NotFound();
            return View(assessment);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(HomeTaskAssessment assessment)
        {
            homeTaskAssessmentService.UpdateHomeTaskAssessment(assessment);
            return RedirectToAction("StudentAssessments", new { studentId = assessment.StudentId });
        }
    }
}