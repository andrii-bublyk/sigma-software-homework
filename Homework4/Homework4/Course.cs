using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4
{
    class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PassingPoint { get; set; }

        public List<Student> Students;
        public List<Hometask> Hometasks;
        public List<Lecturer> Lecturers;

        public Course()
        {
            Students = new List<Student>();
            Hometasks = new List<Hometask>();
            Lecturers = new List<Lecturer>();
        }
    }
}
