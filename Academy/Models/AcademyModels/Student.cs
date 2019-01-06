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

        //public virtual List<StudentCourse> StudentCourses { get; set; }
        public virtual List<Course> Courses { get; set; }
        public virtual List<HomeTaskAssessment> HomeTaskAssessments { get; set; }

        public Student()
        {
            //StudentCourses = new List<StudentCourse>();
            Courses = new List<Course>();
            HomeTaskAssessments = new List<HomeTaskAssessment>();
        }

        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            if (other == null)
                return false;

            return (Id == other.Id
                && Name == other.Name
                && DateTime.Compare(BirthDate, other.BirthDate) == 0
                && PhoneNumber == other.PhoneNumber
                && Email == other.Email
                && GitHubLink == other.GitHubLink
                && Notes == other.Notes);
        }

        public override int GetHashCode()
        {
            return Id
                ^ Name.GetHashCode()
                ^ BirthDate.GetHashCode()
                ^ PhoneNumber.GetHashCode()
                ^ Email.GetHashCode()
                ^ GitHubLink.GetHashCode()
                ^ Notes.GetHashCode();
        }
    }
}