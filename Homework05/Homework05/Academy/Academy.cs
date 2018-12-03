using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework05
{
    public class Academy
    {
        public List<Course> Courses;
        //public List<Student> Students;
        //public List<Lecturer> Lecturers;
        public List<Hometask> Hometasks;
        public List<HomeTaskAssessment> HometasksMarks;
        
        public Academy()
        {
            Courses = new List<Course>();
            Hometasks = new List<Hometask>();
            HometasksMarks = new List<HomeTaskAssessment>();
        }
    }
}
