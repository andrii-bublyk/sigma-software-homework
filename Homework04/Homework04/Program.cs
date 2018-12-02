using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Homework04.AcademyXmlWorker;

namespace Homework04
{
    public static class ExtentionClass
    {
        public static string GetDate(this DateTime date)
        {
            return date.ToString("dd.MM.yyy", CultureInfo.InvariantCulture);
        }

        public static DateTime GetDate(this string date)
        {
            string[] splitDate = date.Split('.');
            int year = int.Parse(splitDate[2]);
            int month = int.Parse(splitDate[1]);
            int day = int.Parse(splitDate[0]);

            return new DateTime(year, month, day);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Academy academy = GenerateSimpleAcademy();
            AcademyXmlSerializer academyXmlSerializer = new AcademyXmlSerializer();

            using (FileStream fs = new FileStream("academy02.xml", FileMode.OpenOrCreate))
            {
                academyXmlSerializer.Serialize(fs, academy);
            }

            Academy academy2 = new Academy();
            using (FileStream fs = new FileStream("academy02.xml", FileMode.OpenOrCreate))
            {
                academy2 = academyXmlSerializer.Deserialize(fs);
            }

            using (FileStream fs = new FileStream("academy03.xml", FileMode.OpenOrCreate))
            {
                academyXmlSerializer.Serialize(fs, academy2);
            }
            Console.ReadKey();
        }

        static Academy GenerateSimpleAcademy()
        {
            Course course1 = new Course(1, "course01", new DateTime(2018, 10, 5), new DateTime(2018, 12, 22), 80);
            Lecturer lecturer1 = new Lecturer(1, "Lecturer1", new DateTime(1988, 10, 5));
            Student student1 = new Student(1, "student1", "380xxxxxxxxx", "email@gmail.com", "github.com");
            Hometask hometask1 = new Hometask(1, "task1", "description1", new DateTime(2018, 10, 10), 2);
            HometaskMark mark1 = new HometaskMark(1, new DateTime(2018, 10, 11), true);

            Course course2 = new Course(2, "course02", new DateTime(2018, 11, 5), new DateTime(2019, 1, 22), 80);
            Lecturer lecturer2 = new Lecturer(2, "Lecturer2", new DateTime(1982, 12, 2));
            Student student2 = new Student(2, "student2", "380xxxxxxxx2", "email2@gmail.com", "github2.com");
            Hometask hometask2 = new Hometask(2, "task2", "description2", new DateTime(2018, 12, 10), 3);
            HometaskMark mark2 = new HometaskMark(2, new DateTime(2018, 12, 11), false);

            Academy academy = new Academy();

            mark1.Course = course1;
            mark1.Hometask = hometask1;

            hometask1.Course = course1;
            hometask1.HomeworkMarks.Add(mark1);

            student1.Courses.Add(course1);
            student1.Marks.Add(mark1);

            lecturer1.Courses.Add(course1);
            lecturer1.Courses.Add(course2);

            course1.Students.Add(student1);
            course1.Students.Add(student2);
            course1.Hometasks.Add(hometask1);
            course1.Lecturers.Add(lecturer1);

            mark2.Course = course2;
            mark2.Hometask = hometask2;

            hometask2.Course = course2;
            hometask2.HomeworkMarks.Add(mark2);

            student2.Courses.Add(course1);
            student2.Courses.Add(course2);
            student2.Marks.Add(mark2);

            course2.Students.Add(student2);
            course2.Hometasks.Add(hometask2);
            course2.Lecturers.Add(lecturer1);

            academy.Courses.Add(course1);
            academy.Students.Add(student1);
            academy.Lecturers.Add(lecturer1);
            academy.Lecturers.Add(lecturer2);
            academy.Hometasks.Add(hometask1);

            academy.Courses.Add(course2);
            academy.Students.Add(student2);
            academy.Hometasks.Add(hometask2);

            academy.HometasksMarks.Add(mark1);
            academy.HometasksMarks.Add(mark2);

            return academy;
        }
    }
}
