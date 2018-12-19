using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ADO;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Homework08.Controllers
{
    public class HometaskController : Controller
    {
        private readonly Repository repository;

        public HometaskController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult CourseHometasks([FromRoute]int id)
        {
            // TODO think about optimization
            Course course = repository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            ViewData["courseId"] = course.Id.ToString();
            ViewData["courseName"] = course.Name;

            var hometasksIds = course.HomeTasks.Select(h => h.Id);

            List<HomeTask> courseHometasks = new List<HomeTask>();
            foreach (var hometaskId in hometasksIds)
            {
                var hometask = repository.GetHomeTaskById(hometaskId);
                if (hometask != null)
                {
                    courseHometasks.Add(hometask);
                }
            }
            
            return View(courseHometasks);
        }

        [HttpGet]
        public IActionResult Create(int courseId)
        {
            ViewData["courseId"] = courseId;
            ViewData["courseName"] = repository.GetCourse(courseId).Name;
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeTask homeTask)
        {
            repository.CreateHomeTask(homeTask, homeTask.Course.Id);

            Course course = repository.GetCourse(homeTask.Course.Id);

            List<HomeTaskAssessment> assessments = new List<HomeTaskAssessment>();
            foreach (var student in course.Students)
            {
                assessments.Add(new HomeTaskAssessment()
                {
                    IsComplete = false,
                    Date = DateTime.Now,
                    HomeTask = homeTask,
                    Student = student
                });
            }
            repository.CreateHomeTaskAssessments(assessments);

            return RedirectToAction("CourseHometasks", new { id = homeTask.Course.Id });
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            HomeTask homeTask = repository.GetHomeTaskById(id);
            if (homeTask == null)
            {
                return NotFound();
            }
            return View(homeTask);
        }

        [HttpPost]
        public IActionResult Edit(HomeTask homeTask)
        {
            repository.UpdateHomeTask(homeTask);
            return RedirectToAction("CourseHometasks", new { id = homeTask.Course.Id });
        }

        public IActionResult Delete(int id)
        {
            HomeTask homeTask= repository.GetHomeTaskById(id);
            if (homeTask == null)
            {
                return NotFound();
            }
            int courseId = homeTask.Course.Id;

            repository.DeleteHomeTask(id);
            return RedirectToAction("CourseHometasks", new { id = courseId });
        }
    }
}