using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Homework04;

namespace Homework04.AcademyXmlWorker
{
    class AcademyXmlWriter
    {
        public void GenerateXmlPresentation(Academy academy, string fileName)
        {
            XDocument document = new XDocument();
            XElement root = new XElement("Academy");
            document.Add(root);

            XElement coursesList = new XElement("courses-list");
            foreach (var course in academy.Courses)
            {
                coursesList.Add(GetCourseXmlPresentation(course));
            }

            root.Add(coursesList);

            document.Save(fileName);
        }

        private XElement GetCourseXmlPresentation(Course cource)
        {
            XElement courseElement = new XElement("course", new XAttribute("id", cource.Id),
                                                            new XAttribute("name", cource.Name),
                                                            new XAttribute("start-date", cource.StartDate.GetDate()),
                                                            new XAttribute("end-date", cource.EndDate.GetDate()),
                                                            new XAttribute("passing-points", cource.PassingPoints)
                                                 );

            XElement studentsList = new XElement("students-list");
            foreach (var student in cource.Students)
            {
                studentsList.Add(new XElement("student", new XAttribute("id", student.Id),
                                                         new XAttribute("name", student.Name))
                                             );
            }

            XElement hometasksList = new XElement("hometasks-list");
            foreach (var hometask in cource.Hometasks)
            {
                studentsList.Add(new XElement("student", new XAttribute("id", hometask.Id),
                                                         new XAttribute("name", hometask.Name))
                                             );
            }

            XElement lecturersList = new XElement("lecturers-list");
            foreach (var lecturer in cource.Lecturers)
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
