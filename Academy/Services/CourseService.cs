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

        public virtual Course GetCourse(int id)
        {
            if (academyRepository == null)
            {
                return null;
            }
            Course course = academyRepository.GetCourse(id);
            return course;
        }

        public virtual bool CreateCourse(Course course)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.CreateCourse(course);
            return true;
        }

        public virtual bool UpdateCourse(Course course)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.UpdateCourse(course);
            return true;
        }

        public virtual bool DeleteCourse(Course course)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteCourse(course);
            return true;
        }

        public virtual bool DeleteCourse(int id)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteCourse(id);
            return true;
        }

        public virtual bool AssignStudentsToCourse(int courseId, IEnumerable<int> studentsIds)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.AssignStudentsToCourse(courseId, studentsIds);
            return true;
        }

        public virtual bool AssignLecturersToCourse(int courseId, List<int> lecturersIds)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.AssignLecturersToCourse(courseId, lecturersIds);
            return true;
        }

        public virtual bool IsCourseExisted(Course course)
        {
            Course dbCourse = GetCourse(course.Id);
            if (dbCourse == null)
                return false;

            return dbCourse.Equals(course);
        }
    }
}
