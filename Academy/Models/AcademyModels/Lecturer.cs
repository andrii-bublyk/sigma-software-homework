using System;
using System.Collections.Generic;

namespace Models.AcademyModels
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public List<LecturerCourse> LecturerCourses { get; set; }
        public List<Course> Courses { get; set; }

        public Lecturer()
        {
            LecturerCourses = new List<LecturerCourse>();
            Courses = new List<Course>();
        }
    }
}
