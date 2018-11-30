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

            XElement lecturersList = new XElement("lecturers-list");
            foreach (var lecturer in academy.Lecturers)
            {
                lecturersList.Add(GetLecturerXmlPresentation(lecturer));
            }

            XElement studentsList = new XElement("students-list");
            foreach (var student in academy.Students)
            {
                studentsList.Add(GetStudentXmlPresentation(student));
            }

            XElement hometasksList = new XElement("hometasks-list");
            foreach (var hometask in academy.Hometasks)
            {
                hometasksList.Add(GetHometaskXmlPresentation(hometask));
            }

            root.Add(coursesList);
            root.Add(lecturersList);
            root.Add(studentsList);
            root.Add(hometasksList);

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

            XElement studentsList = new XElement("students");
            foreach (var student in cource.Students)
            {
                studentsList.Add(new XElement("student", new XAttribute("id", student.Id),
                                                         new XAttribute("name", student.Name))
                                             );
            }

            XElement hometasksList = new XElement("hometasks");
            foreach (var hometask in cource.Hometasks)
            {
                hometasksList.Add(new XElement("student", new XAttribute("id", hometask.Id),
                                                         new XAttribute("name", hometask.Name))
                                             );
            }

            XElement lecturersList = new XElement("lecturers");
            foreach (var lecturer in cource.Lecturers)
            {
                lecturersList.Add(new XElement("lecturer", new XAttribute("id", lecturer.Id),
                                                         new XAttribute("name", lecturer.Name))
                                             );
            }

            courseElement.Add(studentsList);
            courseElement.Add(hometasksList);
            courseElement.Add(lecturersList);

            return courseElement;
        }

        private XElement GetLecturerXmlPresentation(Lecturer lecturer)
        {
            XElement lecturerElement = new XElement("lecturer", new XAttribute("id", lecturer.Id),
                                                              new XAttribute("name", lecturer.Name),
                                                              new XAttribute("birthday", lecturer.Birthday.GetDate())
                                                 );

            XElement coursesList = new XElement("courses");
            foreach (var course in lecturer.Courses)
            {
                coursesList.Add(new XElement("course", new XAttribute("id", course.Id),
                                                       new XAttribute("name", course.Name))
                                             );
            }

            lecturerElement.Add(coursesList);
            return lecturerElement;
        }

        private XElement GetStudentXmlPresentation(Student student)
        {
            XElement studentElement = new XElement("student", new XAttribute("id", student.Id),
                                                             new XAttribute("name", student.Name),
                                                             new XAttribute("phone", student.Phone),
                                                             new XAttribute("email", student.Email),
                                                             new XAttribute("github-link", student.GithubLink)
                                                 );

            XElement coursesList = new XElement("courses");
            foreach (var course in student.Courses)
            {
                coursesList.Add(new XElement("course", new XAttribute("id", course.Id),
                                                       new XAttribute("name", course.Name))
                                             );
            }

            XElement marksList = new XElement("marks-list");
            foreach (var mark in student.Marks)
            {
                marksList.Add(GetMarkXmlPresentation(mark));
            }

            studentElement.Add(coursesList);
            studentElement.Add(marksList);

            return studentElement;
        }

        private XElement GetMarkXmlPresentation(HometaskMark mark)
        {
            XElement markElement = new XElement("mark", new XAttribute("id", mark.Id),
                                                        new XAttribute("complition-date", mark.ComplitionDate.GetDate()),
                                                        new XAttribute("done", mark.Done)
                                                 );

            XElement course = new XElement("course");
            course.Add(new XElement("course", new XAttribute("id", mark.Course.Id),
                                              new XAttribute("name", mark.Course.Name))
                                   );

            XElement hometask = new XElement("hometask");
            hometask.Add(new XElement("hometask", new XAttribute("id", mark.Hometask.Id),
                                              new XAttribute("name", mark.Hometask.Name))
                                   );

            markElement.Add(course);
            markElement.Add(hometask);

            return markElement;
        }

        private XElement GetHometaskXmlPresentation(Hometask hometask)
        {
            XElement hometaskElement = new XElement("hometask", new XAttribute("id", hometask.Id),
                                                            new XAttribute("name", hometask.Name),
                                                            new XAttribute("description", hometask.Description),
                                                            new XAttribute("date", hometask.Date.GetDate()),
                                                            new XAttribute("serial-number", hometask.SerialNumber)
                                                 );

            XElement course = new XElement("course");
            course.Add(new XElement("course", new XAttribute("id", hometask.Course.Id),
                                              new XAttribute("name", hometask.Course.Name))
                                   );

            XElement hometaskMarksList = new XElement("hometask-marks");
            foreach (var mark in hometask.HomeworkMarks)
            {
                hometaskMarksList.Add(new XElement("mark", new XAttribute("id", mark.Id),
                                                         new XAttribute("done", mark.Done))
                                             );
            }

            hometaskElement.Add(course);
            hometaskElement.Add(hometaskMarksList);

            return hometaskElement;
        }
    }
}
