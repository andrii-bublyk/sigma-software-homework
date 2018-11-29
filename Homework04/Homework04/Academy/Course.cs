using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework04
{
    class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PassingPoints { get; set; }

        public List<Student> Students;
        public List<Hometask> Hometasks;
        public List<Lecturer> Lecturers;

        public Course()
        {
            Students = new List<Student>();
            Hometasks = new List<Hometask>();
            Lecturers = new List<Lecturer>();
        }

        public Course(int id, string name, DateTime start, DateTime end, int points) : this()
        {
            Id = id;
            Name = name;
            StartDate = start;
            EndDate = end;
            PassingPoints = points;
        }
    }
}
