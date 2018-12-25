using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult StudentAssessments(int studentId)
        {
            Student student = studentService.GetStudent(studentId);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["studentId"] = student.Id;
            ViewData["studentName"] = student.Name;

            var studentAssessments = homeTaskAssessmentService.GetHomeTaskAssessmentsByStudentId(studentId);

            return View(studentAssessments);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HomeTaskAssessment assessment = homeTaskAssessmentService.GetHomeTaskAssessment(id);
            return View(assessment);
        }

        [HttpPost]
        public IActionResult Edit(HomeTaskAssessment assessment)
        {
            homeTaskAssessmentService.UpdateHomeTaskAssessment(assessment);
            return RedirectToAction("StudentAssessments", new { studentId = assessment.StudentId });
        }
    }
}