using DataAccess.ADO;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework07.Controllers
{
    public class CoursesController : ControllerBase
    {
        private readonly Repository repository;

        public CoursesController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("[controller]")]
        public List<Course> All()
        {
            return repository.GetAllCourses();
        }
    }
}
