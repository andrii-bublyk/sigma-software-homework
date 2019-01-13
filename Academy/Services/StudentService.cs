using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class StudentService
    {
        private readonly IAcademyRepository academyRepository;

        public StudentService()
        {
        }

        public StudentService(IAcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public virtual List<Student> GetAllStudents()
        {
            if (academyRepository == null)
            {
                return new List<Student>();
            }
            List<Student> studentsList = academyRepository.GetAllStudents();
            return studentsList;
        }

        public virtual Student GetStudent(int id)
        {
            if (academyRepository == null)
            {
                return null;
            }
            Student student = academyRepository.GetStudent(id);
            return student;
        }

        public virtual bool CreateStudent(Student student)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.CreateStudent(student);
            return true;
        }

        public virtual bool UpdateStudent(Student student)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.UpdateStudent(student);
            return true;
        }

        public virtual bool DeleteStudent(Student student)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteStudent(student);
            return true;
        }

        public virtual bool DeleteStudent(int id)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteStudent(id);
            return true;
        }

        public virtual bool IsStudentExisted(Student student)
        {
            Student dbStudent = GetStudent(student.Id);
            if (dbStudent == null)
                return false;

            return dbStudent.Equals(student);
        }
    }
}
