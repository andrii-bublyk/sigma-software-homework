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
            //Academy academy = new Academy();
            //Course course1 = new Course(1,
            //                            "course01",
            //                            new DateTime(2018, 10, 5),
            //                            new DateTime(2018, 12, 22),
            //                            80);
            //academy.Courses.Add(course1);

            //AcademyXmlWriter academyXmlWriter = new AcademyXmlWriter();
            //academyXmlWriter.GenerateXmlPresentation(academy, "academy01.xml");


            AcademyXmlReader academyXmlReader = new AcademyXmlReader();
            Academy academy2 = academyXmlReader.GetAcademyObjectModelFromXml("academy01.xml");
            
            //academy.ImportFromXml("academy.xml");

            Console.ReadKey();
        }
    }
}
