using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Homework4
{
    class Academy
    {
        public List<Course> Courses;
        public List<Student> Students;
        public List<Lecturer> Lecturers;

        public Academy()
        {
            Courses = new List<Course>();
            Students = new List<Student>();
            Lecturers = new List<Lecturer>();
        }

        public XDocument ExportToXml()
        {
            XDocument document = new XDocument();
            XElement root = new XElement("Academy");
            document.Add(root);

            XElement coursesList = new XElement("courses-list");
            foreach (var course in Courses)
            {
                coursesList.Add(course.GetXmlPresentation());
            }

            root.Add(coursesList);

            return document;
        }

        public void ImportFromXml(string name)
        {
            Courses = new List<Course>();

            XDocument document = XDocument.Load(name);
            var academy = document.Elements().First();
            var courses = academy.Elements().First();

            foreach (var course in courses.Elements())
            {
                Course importCourse = new Course
                {
                    Id = Convert.ToInt32(course.Attribute("id").Value),
                    Name = course.Attribute("name").Value,
                    StartDate = course.Attribute("start-date").Value.GetDate(),
                    EndDate = course.Attribute("end-date").Value.GetDate(),
                    PassingPoints = Convert.ToInt32(course.Attribute("passing-points").Value)
                };

                Courses.Add(importCourse);
            }
        }
    }
}
