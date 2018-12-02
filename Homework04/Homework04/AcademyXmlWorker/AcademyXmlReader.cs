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

            XElement hometasksMarksNode = academyNode.Descendants("hometasks-marks-list").FirstOrDefault();
            List<HometaskMark> hometasksMarks = GetHometasksMarksFromNode(hometasksMarksNode);

            List<KeyValuePair<int, int>> bindingCoursesLecturersDictionary = GetCoursesLecturersBindingDictionary(coursesNode);
            List<KeyValuePair<int, int>> bindingCoursesStudentsDictionary = GetCoursesStudentsBindingDictionary(coursesNode);
            List<KeyValuePair<int, int>> bindingCoursesHometasksDictionary = GetCoursesHometasksBindingDictionary(coursesNode);

            List<KeyValuePair<int, int>> bindingStudentsMarksDictionary = GetStudentsMarksBindingDictionary(studentsNode);
            List<KeyValuePair<int, int>> bindingMarksHometasksDictionary = GetMarksHometasksBindingDictionary(hometasksMarksNode);
            List<KeyValuePair<int, int>> bindingMarksCoursesDictionary = GetMarksCouresBindingDictionary(hometasksMarksNode);
            
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

        private List<KeyValuePair<int, int>> GetStudentsMarksBindingDictionary(XElement studentsNode)
        {
            List<KeyValuePair<int, int>> bindingStudentsMarksDictionary = new List<KeyValuePair<int, int>>();

            foreach (var student in studentsNode.Elements())
            {
                int studentId = Convert.ToInt32(student.Attribute("id").Value);
                XElement marks = student.Descendants("hometask-marks").FirstOrDefault();
                foreach (var mark in marks.Elements())
                {
                    int markId = Convert.ToInt32(mark.Attribute("id").Value);
                    bindingStudentsMarksDictionary.Add(new KeyValuePair<int, int>(studentId, markId));
                }
            }

            return bindingStudentsMarksDictionary;
        }

        private List<KeyValuePair<int, int>> GetMarksHometasksBindingDictionary(XElement marksNode)
        {
            List<KeyValuePair<int, int>> bindingMarksHometasksDictionary = new List<KeyValuePair<int, int>>();

            foreach (var mark in marksNode.Elements())
            {
                int markId = Convert.ToInt32(mark.Attribute("id").Value);
                XElement hometasks = mark.Descendants("hometask").FirstOrDefault();
                foreach (var hometask in hometasks.Elements())
                {
                    int hometaskId = Convert.ToInt32(hometask.Attribute("id").Value);
                    bindingMarksHometasksDictionary.Add(new KeyValuePair<int, int>(markId, hometaskId));
                }
            }

            return bindingMarksHometasksDictionary;
        }

        private List<KeyValuePair<int, int>> GetMarksCouresBindingDictionary(XElement marksNode)
        {
            List<KeyValuePair<int, int>> bindingMarksCoursesDictionary = new List<KeyValuePair<int, int>>();

            foreach (var mark in marksNode.Elements())
            {
                int markId = Convert.ToInt32(mark.Attribute("id").Value);
                XElement courses = mark.Descendants("course").FirstOrDefault();
                foreach (var course in courses.Elements())
                {
                    int coursekId = Convert.ToInt32(course.Attribute("id").Value);
                    bindingMarksCoursesDictionary.Add(new KeyValuePair<int, int>(markId, coursekId));
                }
            }

            return bindingMarksCoursesDictionary;
        }
    }
}
