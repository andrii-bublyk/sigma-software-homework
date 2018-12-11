using DataAccess.ADO;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework07.Controllers
{
    public class StudentsController : ControllerBase
    {
        private readonly Repository repository;

        public StudentsController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("[controller]")]
        public List<Student> All()
        {
            return repository.GetAllStudents(false);
        }

        [HttpPost]
        [Route("[controller]")]
        public void Create([FromBody] Student student)
        {
            repository.CreateStudent(student);
        }

        [HttpPut]
        [Route("[controller]")]
        public void Update([FromBody] Student student)
        {
            repository.UpdateStudent(student);
        }

        [HttpDelete]
        [Route("[controller]/{id}")]
        public void Delete([FromRoute] int id)
        {
            repository.DeleteStudent(id);
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        public Student Get([FromRoute] int id)
        {
            return repository.GetStudentById(id);
        }
    }
}
