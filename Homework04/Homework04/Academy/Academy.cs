﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework04
{
    class Academy
    {
        public List<Course> Courses;
        public List<Student> Students;
        public List<Lecturer> Lecturers;
        public List<Hometask> Hometasks;
        public List<HometaskMark> HometasksMarks;

        public Academy()
        {
            Courses = new List<Course>();
            Students = new List<Student>();
            Lecturers = new List<Lecturer>();
            Hometasks = new List<Hometask>();
            HometasksMarks = new List<HometaskMark>();
        }
    }
}
