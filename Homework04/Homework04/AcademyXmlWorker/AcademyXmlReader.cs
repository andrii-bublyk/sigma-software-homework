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

            XElement coursesNode = academyNode.Descendants("courses-list").FirstOrDefault();
            List<Course> courses = GetCoursesFromNode(coursesNode);

            XElement lecturersNode = academyNode.Descendants("lecturers-list").FirstOrDefault();
            List<Lecturer> lecturers = GetLecturersFromNode(lecturersNode);

            XElement studentsNode = academyNode.Descendants("students-list").FirstOrDefault();
            List<Student> students = GetStudentsFromNode(studentsNode);

            XElement hometasksNode = academyNode.Descendants("hometasks-list").FirstOrDefault();
            List<Hometask> hometasks = GetHometasksFromNode(hometasksNode);

            XElement hometasksMarksNode = academyNode.Descendants("hometasks-marks-list").FirstOrDefault();
            List<HometaskMark> hometasksMarks = GetHometasksMarksFromNode(hometasksMarksNode);

            List<KeyValuePair<int, int>> bindingCoursesLecturersDictionary = GetBindingDictionaryById(coursesNode, "lecturers");
            List<KeyValuePair<int, int>> bindingCoursesStudentsDictionary = GetBindingDictionaryById(coursesNode, "students");
            List<KeyValuePair<int, int>> bindingCoursesHometasksDictionary = GetBindingDictionaryById(coursesNode, "hometasks");

            List<KeyValuePair<int, int>> bindingStudentsMarksDictionary = GetBindingDictionaryById(studentsNode, "hometask-marks");
            List<KeyValuePair<int, int>> bindingMarksHometasksDictionary = GetBindingDictionaryById(hometasksMarksNode, "hometask");
            List<KeyValuePair<int, int>> bindingMarksCoursesDictionary = GetBindingDictionaryById(hometasksMarksNode, "course");
            
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

            // staudents and marks binding
            foreach (var studentMark in bindingStudentsMarksDictionary)
            {
                Student student = students.Where(n => n.Id == studentMark.Key).First();
                HometaskMark mark = hometasksMarks.Where(n => n.Id == studentMark.Value).First();

                student.Marks.Add(mark);
            }

            // marks and hometasks binding
            foreach (var markHometask in bindingMarksHometasksDictionary)
            {
                HometaskMark mark = hometasksMarks.Where(n => n.Id == markHometask.Key).First();
                Hometask hometask = hometasks.Where(n => n.Id == markHometask.Value).First();

                mark.Hometask = hometask;
                hometask.HomeworkMarks.Add(mark);
            }

            // marks and courses binding
            foreach (var markHometask in bindingMarksCoursesDictionary)
            {
                HometaskMark mark = hometasksMarks.Where(n => n.Id == markHometask.Key).First();
                Course course = courses.Where(n => n.Id == markHometask.Value).First();

                mark.Course = course;
            }

            academy.Courses = courses;
            academy.Lecturers = lecturers;
            academy.Students = students;
            academy.Hometasks = hometasks;
            academy.HometasksMarks = hometasksMarks;

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
        
        private static List<HometaskMark> GetHometasksMarksFromNode(XElement hometasksMarksNode)
        {
            List<HometaskMark> hometasksMarks = new List<HometaskMark>();
            foreach (var mark in hometasksMarksNode.Elements())
            {
                HometaskMark importMark = new HometaskMark
                {
                    Id = Convert.ToInt32(mark.Attribute("id").Value),
                    ComplitionDate = mark.Attribute("complition-date").Value.GetDate(),
                    Done = Convert.ToBoolean(mark.Attribute("done").Value)
                };
                hometasksMarks.Add(importMark);
            }

            return hometasksMarks;
        }

        private List<KeyValuePair<int, int>> GetBindingDictionaryById(XElement node, string secondElementName)
        {
            List<KeyValuePair<int, int>> bindingDictionary = new List<KeyValuePair<int, int>>();

            foreach (var element in node.Elements())
            {
                int firstId = Convert.ToInt32(element.Attribute("id").Value);
                XElement secondElements = element.Descendants(secondElementName).FirstOrDefault();
                foreach (var course in secondElements.Elements())
                {
                    int secondId = Convert.ToInt32(course.Attribute("id").Value);
                    bindingDictionary.Add(new KeyValuePair<int, int>(firstId, secondId));
                }
            }

            return bindingDictionary;
        }
    }
}
