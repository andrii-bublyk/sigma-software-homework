using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Homework04;

namespace Homework04.AcademyXmlWorker
{
    class AcademyXmlSerializer
    {
        public void Serialize(FileStream fs, Academy academy)
        {
            XDocument xDoc = GenerateXmlPresentation(academy);
            xDoc.Save(fs);
        }

        public Academy Deserialize(FileStream fs)
        {
            XDocument document = XDocument.Load(fs);
            Academy academy = GetAcademyObjectModelFromXml(document);
            return academy;
        }

        #region serializer
        private XDocument GenerateXmlPresentation(Academy academy)
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

            XElement marksList = new XElement("hometasks-marks-list");
            foreach (var mark in academy.HometasksMarks)
            {
                marksList.Add(GetMarkXmlPresentation(mark));
            }

            root.Add(coursesList);
            root.Add(lecturersList);
            root.Add(studentsList);
            root.Add(hometasksList);
            root.Add(marksList);

            return document;
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

            XElement marksList = new XElement("hometask-marks");
            foreach (var mark in student.Marks)
            {
                marksList.Add(new XElement("mark", new XAttribute("id", mark.Id),
                                                           new XAttribute("done", mark.Done))
                                     );
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
        #endregion

        #region deserializer
        private Academy GetAcademyObjectModelFromXml(XDocument document)
        {
            Academy academy = new Academy();
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

        private List<Course> GetCoursesFromNode(XElement coursesNode)
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

        private List<Lecturer> GetLecturersFromNode(XElement lecturersNode)
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

        private List<Student> GetStudentsFromNode(XElement studentsNode)
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

        private List<Hometask> GetHometasksFromNode(XElement hometasksNode)
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

        private List<HometaskMark> GetHometasksMarksFromNode(XElement hometasksMarksNode)
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
        #endregion
    }
}
