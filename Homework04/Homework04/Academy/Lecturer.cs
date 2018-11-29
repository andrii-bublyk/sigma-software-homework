using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework04
{
    class Lecturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public List<Course> Courses;

        public Lecturer()
        {
            Courses = new List<Course>();
        }
    }
}
