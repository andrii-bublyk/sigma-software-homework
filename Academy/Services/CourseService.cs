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

        public List<Course> GetAllCourses()
        {
            return academyRepository.GetAllCourses();
        }

        public void CreateCourse(Course course)
        {
            academyRepository.CreateCourse(course);
        }
    }
}
