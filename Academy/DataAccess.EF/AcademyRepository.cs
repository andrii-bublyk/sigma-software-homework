using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EF
{
    public class AcademyRepository
    {
        private readonly AcademyContext academyDb;

        public AcademyRepository(AcademyContext context)
        {
            academyDb = context;
        }

        public List<Course> GetAllCourses()
        {
            using (academyDb)
            {
                 return academyDb.Course.ToList();
            }
        }

        public void CreateCourse(Course course)
        {
            using (academyDb)
            {
                academyDb.Course.Add(course);
                academyDb.SaveChanges();
            }
        }
    }
}
