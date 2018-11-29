using System;
using System.Collections.Generic;
using System.Globalization;
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

            AcademyXmlWriter academyXmlWriter = new AcademyXmlWriter();
            academyXmlWriter.GenerateXmlPresentation(academy, "academy02.xml");

            //AcademyXmlReader academyXmlReader = new AcademyXmlReader();
            //Academy academy2 = academyXmlReader.GetAcademyObjectModelFromXml("academy01.xml");

            Console.ReadKey();
        }

        static Academy GenerateSimpleAcademy()
        {
            Course course1 = new Course(1, "course01", new DateTime(2018, 10, 5), new DateTime(2018, 12, 22), 80);
            Lecturer lecturer1 = new Lecturer(1, "Lecturer1", new DateTime(1988, 10, 5));
            Student student1 = new Student(1, "student1", "380xxxxxxxxx", "email@gmail.com", "github.com");
            Hometask hometask1 = new Hometask(1, "task1", "description1", new DateTime(2018, 10, 10), 2);
            HometaskMark mark1 = new HometaskMark(1, new DateTime(2018, 10, 11), true);
            Academy academy = new Academy();

            mark1.Course = course1;
            mark1.Hometask = hometask1;

            hometask1.Course = course1;
            hometask1.HomeworkMarks.Add(mark1);

            student1.Courses.Add(course1);
            student1.Marks.Add(mark1);

            lecturer1.Courses.Add(course1);

            course1.Students.Add(student1);
            course1.Hometasks.Add(hometask1);
            course1.Lecturers.Add(lecturer1);

            academy.Courses.Add(course1);
            academy.Students.Add(student1);
            academy.Lecturers.Add(lecturer1);
            academy.Hometasks.Add(hometask1);

            return academy;
        }
    }
}
