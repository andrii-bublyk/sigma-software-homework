using System;
using System.Collections.Generic;

namespace Models.AcademyModels
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public List<Course> Courses { get; set; }

        public Lecturer()
        {
            Courses = new List<Course>();
        }

        public override bool Equals(object obj)
        {
            Lecturer other = obj as Lecturer;
            if (other == null)
                return false;

            return (Id == other.Id
                && Name == other.Name
                && DateTime.Compare(BirthDate, other.BirthDate) == 0);
        }

        public override int GetHashCode()
        {
            return Id
                ^ Name.GetHashCode()
                ^ BirthDate.GetHashCode();
        }
    }
}
