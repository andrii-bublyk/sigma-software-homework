using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Homework05
{
    class Program
    {
        static void Main(string[] args)
        {
            //Academy academy = new Academy();

            //XmlSerializer serializer = new XmlSerializer(typeof(Academy));
            //using (FileStream fs = new FileStream("academy.xml", FileMode.OpenOrCreate))
            //{
            //    serializer.Serialize(fs, academy);
            //}

            List<Course> courses = AcademyDbAdapter.AcademyDbAdapter.GetAllCourses();
            List<Student> students = AcademyDbAdapter.AcademyDbAdapter.GetAllStudents();

            Console.ReadKey();
        }
    }
}
