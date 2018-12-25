using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class StudentService
    {
        private readonly AcademyRepository academyRepository;

        public StudentService(AcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> studentsList = academyRepository.GetAllStudents();
            return studentsList;
        }

        public Student GetStudent(int id)
        {
            Student student = academyRepository.GetStudent(id);
            return student;
        }

        public void CreateStudent(Student student)
        {
            academyRepository.CreateStudent(student);
        }

        public void UpdateStudent(Student student)
        {
            academyRepository.UpdateStudent(student);
        }

        public void DeleteStudent(Student student)
        {
            academyRepository.DeleteStudent(student);
        }

        public void DeleteStudent(int id)
        {
            academyRepository.DeleteStudent(id);
        }
    }
}
