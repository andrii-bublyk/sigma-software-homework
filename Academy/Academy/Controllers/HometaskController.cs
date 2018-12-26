using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.AcademyModels;
using Services;

namespace Academy.Controllers
{
    public class HometaskController : Controller
    {
        private readonly HometaskService hometaskService;
        private readonly CourseService courseService;

        public HometaskController(HometaskService hometaskService, CourseService courseService)
        {
            this.hometaskService = hometaskService;
            this.courseService = courseService;
        }

        [HttpGet]
        public IActionResult CourseHometasks(int courseId)
        {
            Course course = courseService.GetCourse(courseId);
            if (course == null)
            {
                return NotFound();
            }

            ViewData["courseId"] = course.Id.ToString();
            ViewData["courseName"] = course.Name;

            //var hometasksIds = course.HomeTasks.Select(h => h.Id);
            var hometasksIds = hometaskService.GetAllHomeTasks().Where(h => h.CourseId == course.Id).Select(h => h.Id);

            List<HomeTask> courseHometasks = new List<HomeTask>();
            foreach (var hometaskId in hometasksIds)
            {
                var hometask = hometaskService.GetHomeTask(hometaskId);
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
            ViewData["courseName"] = courseService.GetCourse(courseId).Name;
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeTask homeTask)
        {
            hometaskService.CreateHomeTask(homeTask);

            return RedirectToAction("CourseHometasks", new { courseId = homeTask.CourseId });
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            HomeTask homeTask = hometaskService.GetHomeTask(id);
            if (homeTask == null)
            {
                return NotFound();
            }
            return View(homeTask);
        }

        [HttpPost]
        public IActionResult Edit(HomeTask homeTask)
        {
            hometaskService.UpdateHomeTask(homeTask);
            return RedirectToAction("CourseHometasks", new { courseId = homeTask.CourseId });
        }

        public IActionResult Delete(int id)
        {
            HomeTask homeTask = hometaskService.GetHomeTask(id);
            if (homeTask == null)
            {
                return NotFound();
            }
            hometaskService.DeleteHomeTask(homeTask);

            return RedirectToAction("CourseHometasks", new { courseId = homeTask.CourseId });
        }
    }
}