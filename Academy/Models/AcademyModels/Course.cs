using System;
using System.Collections.Generic;

namespace Models.AcademyModels
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PassCredits { get; set; }

        //public virtual List<LecturerCourse> LecturerCourses { get; set; }
        //public virtual List<StudentCourse> StudentCourses { get; set; }
        public List<Lecturer> Lecturers { get; set; }
        public List<Student> Students { get; set; }
        public List<HomeTask> HomeTasks { get; set; }

        public Course()
        {
            //LecturerCourses = new List<LecturerCourse>();
            //StudentCourses = new List<StudentCourse>();
            Lecturers = new List<Lecturer>();
            Students = new List<Student>();
            HomeTasks = new List<HomeTask>();
        }

        public override bool Equals(object obj)
        {
            Course other = obj as Course;
            if (other == null)
                return false;

            return (Id == other.Id
                && Name == other.Name
                && DateTime.Compare(StartDate, other.StartDate) == 0
                && DateTime.Compare(EndDate, other.EndDate) == 0
                && PassCredits == other.PassCredits);
        }

        public override int GetHashCode()
        {
            return Id
                ^ Name.GetHashCode()
                ^ StartDate.GetHashCode()
                ^ EndDate.GetHashCode()
                ^ PassCredits;
        }
    }
}