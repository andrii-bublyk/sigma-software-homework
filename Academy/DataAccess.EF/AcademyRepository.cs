using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EF
{
    public class AcademyRepository
    {
        private readonly IOptions<RepositoryOptions> options;

        public AcademyRepository(IOptions<RepositoryOptions> options)
        {
            this.options = options;
        }

        // Course table
        public List<Course> GetAllCourses()
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                 return academyDb.Course.ToList();
            }
        }

        public Course GetCourse(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                Course course = academyDb.Course.FirstOrDefault(c => c.Id == id);

                //var studentsIds = academyDb.StudentCourse.Where(sc => sc.CourseId == course.Id).Select(sc => sc.StudentId);
                //List<Student> courseStudents = new List<Student>();
                //foreach (var studentId in studentsIds)
                //{
                //    Student student = academyDb.Student.FirstOrDefault(s => s.Id == studentId);
                //    if (student != null)
                //    {
                //        courseStudents.Add(student);
                //    }
                //}
                //course.Students = courseStudents;

                //var lecturersIds = academyDb.LecturerCourse.Where(lc => lc.CourseId == course.Id).Select(lc => lc.LecturerId);
                //List<Lecturer> courseLectors = new List<Lecturer>();
                //foreach (var lecturerId in lecturersIds)
                //{
                //    Lecturer lecturer = academyDb.Lecturer.FirstOrDefault(l => l.Id == lecturerId);
                //    if (lecturer != null)
                //    {
                //        courseLectors.Add(lecturer);
                //    }
                //}
                //course.Lecturers = courseLectors;

                return course;
            }
        }

        public void CreateCourse(Course course)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Course.Add(course);
                academyDb.SaveChanges();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Course.Update(course);

                academyDb.SaveChanges();
            }
        }

        public void DeleteCourse(Course course)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Course.Remove(course);
                academyDb.SaveChanges();
            }
        }

        public void DeleteCourse(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                Course course = GetCourse(id);
                DeleteCourse(course);
                academyDb.SaveChanges();
            }
        }

        public void AssignStudentsToCourse(int courseId, List<int> studentsIds)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                // get all course notations
                var studCour = academyDb.StudentCourse.Where(sc => sc.CourseId == courseId);
                foreach (var sc in studCour)
                {
                    academyDb.StudentCourse.Remove(sc);
                }

                foreach (var stId in studentsIds)
                {
                    academyDb.StudentCourse.Add(new StudentCourse() { StudentId = stId, CourseId = courseId });
                }

                academyDb.SaveChanges();
            }
        }

        public void AssignLecturersToCourse(int courseId, List<int> lecturersIds)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                // get all course notations
                var lectCour = academyDb.LecturerCourse.Where(lc => lc.CourseId == courseId);
                foreach (var lc in lectCour)
                {
                    academyDb.LecturerCourse.Remove(lc);
                }

                foreach (var lcId in lecturersIds)
                {
                    academyDb.LecturerCourse.Add(new LecturerCourse() { LecturerId = lcId, CourseId = courseId });
                }

                academyDb.SaveChanges();
            }
        }

        // Lecturer table
        public List<Lecturer> GetAllLecturers()
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                return academyDb.Lecturer.ToList();
            }
        }

        public Lecturer GetLecturer(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                return academyDb.Lecturer.FirstOrDefault(l => l.Id == id);
            }
        }

        public void CreateLecturer(Lecturer lecturer)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Lecturer.Add(lecturer);
                academyDb.SaveChanges();
            }
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Lecturer.Update(lecturer);
                academyDb.SaveChanges();
            }
        }

        public void DeleteLecturer(Lecturer lecturer)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Lecturer.Remove(lecturer);
                academyDb.SaveChanges();
            }
        }

        public void DeleteLecturer(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                Lecturer lecturer = GetLecturer(id);
                DeleteLecturer(lecturer);
                academyDb.SaveChanges();
            }
        }

        // Student table
        public List<Student> GetAllStudents()
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                return academyDb.Student.ToList();
            }
        }

        public Student GetStudent(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                Student student = academyDb.Student.FirstOrDefault(s => s.Id == id);
                List<HomeTaskAssessment> studentAssessments = academyDb.HomeTaskAssessment.Where(a => a.StudentId == student.Id).ToList();
                student.HomeTaskAssessments = studentAssessments;

                return student;
            }
        }

        public void CreateStudent(Student student)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Student.Add(student);
                academyDb.SaveChanges();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Student.Update(student);
                academyDb.SaveChanges();
            }
        }

        public void DeleteStudent(Student student)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.Student.Remove(student);
                academyDb.SaveChanges();
            }
        }

        public void DeleteStudent(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                Student student = GetStudent(id);
                DeleteStudent(student);
                academyDb.SaveChanges();
            }
        }

        // Hometask table
        public List<HomeTask> GetAllHomeTasks()
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                return academyDb.HomeTask.ToList();
            }
        }

        public HomeTask GetHomeTask(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                return academyDb.HomeTask.FirstOrDefault(h => h.Id == id);
            }
        }

        public void CreateHomeTask(HomeTask homeTask)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                //using (var transaction = academyDb.Database.BeginTransaction())
                //{
                //    academyDb.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Course] ON");
                //    academyDb.HomeTask.Add(homeTask);
                //    academyDb.SaveChanges();
                //    academyDb.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Course] OFF");
                //    transaction.Commit();
                //}
                academyDb.HomeTask.Add(homeTask);
                academyDb.SaveChanges();
            }
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.HomeTask.Update(homeTask);
                academyDb.SaveChanges();
            }
        }

        public void DeleteHomeTask(HomeTask homeTask)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.HomeTask.Remove(homeTask);
                academyDb.SaveChanges();
            }
        }

        public void DeleteHomeTask(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                HomeTask homeTask = GetHomeTask(id);
                DeleteHomeTask(homeTask);
                academyDb.SaveChanges();
            }
        }

        // HometaskAssessment table
        public List<HomeTaskAssessment> GetAllHomeTaskAssessments()
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                return academyDb.HomeTaskAssessment.ToList();
            }
        }

        public HomeTaskAssessment GetHomeTaskAssessment(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                return academyDb.HomeTaskAssessment.FirstOrDefault(a => a.Id == id);
            }
        }

        public void CreateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.HomeTaskAssessment.Add(homeTaskAssessment);
                academyDb.SaveChanges();
            }
        }

        public void UpdateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.HomeTaskAssessment.Update(homeTaskAssessment);
                academyDb.SaveChanges();
            }
        }

        public void DeleteHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.HomeTaskAssessment.Remove(homeTaskAssessment);
                academyDb.SaveChanges();
            }
        }

        public void DeleteHomeTaskAssessment(int id)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                HomeTaskAssessment homeTaskAssessment = GetHomeTaskAssessment(id);
                DeleteHomeTaskAssessment(homeTaskAssessment);
                academyDb.SaveChanges();
            }
        }

        public List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                List<HomeTaskAssessment> studentAssessments = academyDb.HomeTaskAssessment.Where(a => a.StudentId == studentId).ToList();

                foreach (var assessment in studentAssessments)
                {
                    assessment.HomeTask = academyDb.HomeTask.FirstOrDefault(h => h.Id == assessment.HomeTaskId);
                }

                return studentAssessments;
            }
        }
    }
}
