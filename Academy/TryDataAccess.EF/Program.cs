using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Linq;

namespace TryDataAccess.EF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AcademyContext academyDb = new AcademyContext())
            {
                academyDb.Course.Add(new Course()
                {
                    Name = "name",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    PassCredits = 1
                });
                academyDb.SaveChanges();
            }

            using (AcademyContext academyDb = new AcademyContext())
            {
                var courses = academyDb.Course.ToList();
            }

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
