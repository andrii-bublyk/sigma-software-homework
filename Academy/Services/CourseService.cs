using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;

namespace Services
{
    public class CourseService
    {
        private readonly AcademyRepository academyRepository;

        public CourseService()
        {
        }

        public CourseService(AcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public virtual List<Course> GetAllCourses()
        {
            if (academyRepository == null)
            {
                return new List<Course>();
            }
            List<Course> coursesList = academyRepository.GetAllCourses();
            return coursesList;
        }

        public Course GetCourse(int id)
        {
            Course course = academyRepository.GetCourse(id);
            return course;
        }

        public void CreateCourse(Course course)
        {
            academyRepository.CreateCourse(course);
        }

        public void UpdateCourse(Course course)
        {
            academyRepository.UpdateCourse(course);
        }

        public void DeleteCourse(Course course)
        {
            academyRepository.DeleteCourse(course);
        }

        public void DeleteCourse(int id)
        {
            academyRepository.DeleteCourse(id);
        }

        public void AssignStudentsToCourse(int courseId, List<int> studentsIds)
        {
            academyRepository.AssignStudentsToCourse(courseId, studentsIds);
        }

        public void AssignLecturersToCourse(int courseId, List<int> lecturersIds)
        {
            academyRepository.AssignLecturersToCourse(courseId, lecturersIds);
        }
    }
}
