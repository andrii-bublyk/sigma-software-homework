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
    class AcademyXmlReader
    {
        public Academy GetAcademyObjectModelFromXml(string fileName)
        {
            Academy academy = new Academy();

            XDocument document = XDocument.Load(fileName);
            XElement academyNode = document.Elements().First();

            //XElement coursesNode = academyNode.Elements().First();
            XElement coursesNode = academyNode.Descendants("courses-list").FirstOrDefault();
            List<Course> courses = GetCoursesFromNode(coursesNode);

            XElement lecturersNode = academyNode.Descendants("lecturers-list").FirstOrDefault();
            List<Lecturer> lecturers = GetLecturersFromNode(lecturersNode);

            XElement studentsNode = academyNode.Descendants("students-list").FirstOrDefault();
            List<Student> students = GetStudentsFromNode(studentsNode);

            XElement hometasksNode = academyNode.Descendants("hometasks-list").FirstOrDefault();
            List<Hometask> hometasks = GetHometasksFromNode(hometasksNode);

            List<KeyValuePair<int, int>> bindingCoursesLecturersDictionary = GetCoursesLecturersBindingDictionary(coursesNode);
            List<KeyValuePair<int, int>> bindingCoursesStudentsDictionary = GetCoursesStudentsBindingDictionary(coursesNode);
            List<KeyValuePair<int, int>> bindingCoursesHometasksDictionary = GetCoursesHometasksBindingDictionary(coursesNode);
            
            // courses and lecturers binding
            foreach (var courseLecturer in bindingCoursesLecturersDictionary)
            {
                Course course = courses.Where(n => n.Id == courseLecturer.Key).First();
                Lecturer lecturer = lecturers.Where(n => n.Id == courseLecturer.Value).First();

                course.Lecturers.Add(lecturer);
                lecturer.Courses.Add(course);
            }

            // courses and students binding
            foreach (var courseStudent in bindingCoursesStudentsDictionary)
            {
                Course course = courses.Where(n => n.Id == courseStudent.Key).First();
                Student student = students.Where(n => n.Id == courseStudent.Value).First();

                course.Students.Add(student);
                student.Courses.Add(course);
            }

            // courses and hometasks binding
            foreach (var courseHometask in bindingCoursesHometasksDictionary)
            {
                Course course = courses.Where(n => n.Id == courseHometask.Key).First();
                Hometask hometask = hometasks.Where(n => n.Id == courseHometask.Value).First();

                course.Hometasks.Add(hometask);
                hometask.Course = course; ;
            }

            academy.Courses = courses;
            academy.Lecturers = lecturers;
            academy.Students = students;
            academy.Hometasks = hometasks;

            return academy;
        }

        private static List<Course> GetCoursesFromNode(XElement coursesNode)
        {
            List<Course> courses = new List<Course>();
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

            return courses;
        }

        private static List<Lecturer> GetLecturersFromNode(XElement lecturersNode)
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            foreach (var lecturer in lecturersNode.Elements())
            {
                Lecturer importLecturer = new Lecturer
                {
                    Id = Convert.ToInt32(lecturer.Attribute("id").Value),
                    Name = lecturer.Attribute("name").Value,
                    Birthday = lecturer.Attribute("birthday").Value.GetDate()
                };
                lecturers.Add(importLecturer);
            }

            return lecturers;
        }

        private static List<Student> GetStudentsFromNode(XElement studentsNode)
        {
            List<Student> students = new List<Student>();
            foreach (var student in studentsNode.Elements())
            {
                Student importStudent = new Student
                {
                    Id = Convert.ToInt32(student.Attribute("id").Value),
                    Name = student.Attribute("name").Value,
                    Phone = student.Attribute("phone").Value,
                    Email = student.Attribute("email").Value,
                    GithubLink = student.Attribute("github-link").Value
                };
                students.Add(importStudent);
            }

            return students;
        }

        private static List<Hometask> GetHometasksFromNode(XElement hometasksNode)
        {
            List<Hometask> hometasks = new List<Hometask>();
            foreach (var hometask in hometasksNode.Elements())
            {
                Hometask importHometask = new Hometask
                {
                    Id = Convert.ToInt32(hometask.Attribute("id").Value),
                    Name = hometask.Attribute("name").Value,
                    Description = hometask.Attribute("description").Value,
                    Date = hometask.Attribute("date").Value.GetDate(),
                    SerialNumber = Convert.ToInt32(hometask.Attribute("serial-number").Value)
                };
                hometasks.Add(importHometask);
            }

            return hometasks;
        }

        private List<KeyValuePair<int, int>> GetCoursesLecturersBindingDictionary(XElement coursesNode)
        {
            List<KeyValuePair<int, int>> bindingCoursesLecturersDictionary = new List<KeyValuePair<int, int>>();

            foreach (var course in coursesNode.Elements())
            {
                int courseId = Convert.ToInt32(course.Attribute("id").Value);
                XElement lecturers = course.Descendants("lecturers").FirstOrDefault();
                foreach (var lecturer in lecturers.Elements())
                {
                    int lecturerId = Convert.ToInt32(lecturer.Attribute("id").Value);
                    bindingCoursesLecturersDictionary.Add(new KeyValuePair<int, int>(courseId, lecturerId));
                }
            }

            return bindingCoursesLecturersDictionary;
        }

        private List<KeyValuePair<int, int>> GetCoursesStudentsBindingDictionary(XElement coursesNode)
        {
            List<KeyValuePair<int, int>> bindingCoursesStudentsDictionary = new List<KeyValuePair<int, int>>();

            foreach (var course in coursesNode.Elements())
            {
                int courseId = Convert.ToInt32(course.Attribute("id").Value);
                XElement students = course.Descendants("students").FirstOrDefault();
                foreach (var student in students.Elements())
                {
                    int studentId = Convert.ToInt32(student.Attribute("id").Value);
                    bindingCoursesStudentsDictionary.Add(new KeyValuePair<int, int>(courseId, studentId));
                }
            }

            return bindingCoursesStudentsDictionary;
        }

        private List<KeyValuePair<int, int>> GetCoursesHometasksBindingDictionary(XElement coursesNode)
        {
            List<KeyValuePair<int, int>> bindingCoursesHometasksDictionary = new List<KeyValuePair<int, int>>();

            foreach (var course in coursesNode.Elements())
            {
                int courseId = Convert.ToInt32(course.Attribute("id").Value);
                XElement hometasks = course.Descendants("hometasks").FirstOrDefault();
                foreach (var hometask in hometasks.Elements())
                {
                    int hometaskId = Convert.ToInt32(hometask.Attribute("id").Value);
                    bindingCoursesHometasksDictionary.Add(new KeyValuePair<int, int>(courseId, hometaskId));
                }
            }

            return bindingCoursesHometasksDictionary;
        }
    }
}
