using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string GithubLink { get; set; }

        public List<Course> Courses;
        public List<HomeworkMark> Marks;

        public Student()
        {
            Courses = new List<Course>();
            Marks = new List<HomeworkMark>();
        }

    }
}
