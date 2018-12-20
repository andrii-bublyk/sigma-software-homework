using Models;
using Models.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.EF
{
    public class Repository : IRepository
    {
        public Course CreateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public HomeTask CreateHomeTask(HomeTask homeTask, int courseId)
        {
            throw new NotImplementedException();
        }

        public void CreateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments)
        {
            throw new NotImplementedException();
        }

        public Lecturer CreateLecturer(Lecturer lecturer)
        {
            throw new NotImplementedException();
        }

        public Student CreateStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteHomeTask(int homeTaskId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLecturer(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public List<Lecturer> GetAllLecturers()
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAllStudents(bool loadAllDependencies = true)
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }

        public HomeTaskAssessment GetHomeTaskAssessmentById(int assessmentId)
        {
            throw new NotImplementedException();
        }

        public List<HomeTaskAssessment> GetHomeTaskAssessmentsByHomeTaskId(int homeTaskId)
        {
            throw new NotImplementedException();
        }

        public List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId)
        {
            throw new NotImplementedException();
        }

        public HomeTask GetHomeTaskById(int id, bool loadAllDependencies = true)
        {
            throw new NotImplementedException();
        }

        public Lecturer GetLecturerById(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetStudentById(int id, bool loadAllDependencies = true)
        {
            throw new NotImplementedException();
        }

        public void SetLecturersToCourse(int courseId, IEnumerable<int> lecturerIds)
        {
            throw new NotImplementedException();
        }

        public void SetStudentsToCourse(int courseId, IEnumerable<int> studentsId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            throw new NotImplementedException();
        }

        public void UpdateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments)
        {
            throw new NotImplementedException();
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
