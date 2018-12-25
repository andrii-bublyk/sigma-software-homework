using System;
using System.Collections.Generic;

namespace Models.AcademyModels
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GitHubLink { get; set; }
        public string Notes { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
        public List<HomeTaskAssessment> HomeTaskAssessments { get; set; }

        public Student()
        {
            StudentCourses = new List<StudentCourse>();
            HomeTaskAssessments = new List<HomeTaskAssessment>();
        }
    }
}