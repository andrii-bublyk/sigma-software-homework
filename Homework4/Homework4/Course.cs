using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework4
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

        public Course(int id, string name, DateTime start, DateTime end, int points)
        {
            Students = new List<Student>();
            Hometasks = new List<Hometask>();
            Lecturers = new List<Lecturer>();

            Id = id;
            Name = name;
            StartDate = start;
            EndDate = end;
            PassingPoints = points;
        }

        public XElement GetXmlPresentation()
        {
            XElement courseElement = new XElement("course", new XAttribute("id", Id),
                                                            new XAttribute("name", Name),
                                                            new XAttribute("start-date", StartDate.GetDate()),
                                                            new XAttribute("end-date", EndDate.GetDate()),
                                                            new XAttribute("passing-points", PassingPoints)
                                                 );

            XElement studentsList = new XElement("students-list");
            foreach (var student in Students)
            {
                studentsList.Add(new XElement("student", new XAttribute("id", student.Id),
                                                         new XAttribute("name", student.Name))
                                             );
            }

            XElement hometasksList = new XElement("hometasks-list");
            foreach (var hometask in Hometasks)
            {
                studentsList.Add(new XElement("student", new XAttribute("id", hometask.Id),
                                                         new XAttribute("name", hometask.Name))
                                             );
            }

            XElement lecturersList = new XElement("lecturers-list");
            foreach (var lecturer in Lecturers)
            {
                studentsList.Add(new XElement("lecturer", new XAttribute("id", lecturer.Id),
                                                         new XAttribute("name", lecturer.Name))
                                             );
            }

            courseElement.Add(studentsList);
            courseElement.Add(hometasksList);
            courseElement.Add(lecturersList);

            return courseElement;
        }
    }
}
