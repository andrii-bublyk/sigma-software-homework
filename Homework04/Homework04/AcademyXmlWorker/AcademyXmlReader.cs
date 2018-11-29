using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Homework04;

namespace Homework04.AcademyXmlWorker
{
    class AcademyXmlReader
    {
        public Academy GetAcademyObjectModelFromXml(string fileName)
        {
            Academy academy = new Academy();

            List<Course> courses = new List<Course>();
            XDocument document = XDocument.Load(fileName);
            var academyNode = document.Elements().First();
            var coursesNode = academyNode.Elements().First();

            foreach (var course in coursesNode.Elements())
            {
                Course importCourse = new Course
                {
                    Id = Convert.ToInt32(course.Attribute("id").Value),
                    Name = course.Attribute("name").Value,
                    StartDate = course.Attribute("start-date").Value.GetDate(),
                    EndDate = course.Attribute("end-date").Value.GetDate(),
                    PassingPoints = Convert.ToInt32(course.Attribute("passing-points").Value)
                };

                courses.Add(importCourse);
            }

            academy.Courses = courses;

            return academy;
        }
    }
}
