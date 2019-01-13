using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.AcademyModels;
using Models.AuthorizationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.EF
{
    public class AcademyRepository2 : IAcademyRepository
    {
        private readonly AcademyContext context;

        public AcademyRepository2(AcademyContext context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }

        // Course table
        public List<Course> GetAllCourses()
        {
            return context.Course.ToList();
        }

        public Course GetCourse(int id)
        {
            // todo
            return context.Course.FirstOrDefault(c => c.Id == id);
        }

        public void CreateCourse(Course course)
        {
            context.Course.Add(course);
            context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            context.Course.Update(course);
            context.SaveChanges();
        }

        public void DeleteCourse(Course course)
        {
            context.Course.Remove(course);
            context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            Course course = this.GetCourse(id);
            this.DeleteCourse(course);
        }

        public void AssignStudentsToCourse(int courseId, IEnumerable<int> assignedStudentsIds)
        {
            // todo
        }

        public void AssignLecturersToCourse(int courseId, List<int> lecturersIds)
        {
            // todo
        }

        // Lecturer table
        public List<Lecturer> GetAllLecturers()
        {
            return context.Lecturer.ToList();
        }

        public Lecturer GetLecturer(int id)
        {
            return context.Lecturer.FirstOrDefault(l => l.Id == id);
        }

        public void CreateLecturer(Lecturer lecturer)
        {
            context.Lecturer.Add(lecturer);
            context.SaveChanges();
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            context.Lecturer.Update(lecturer);
            context.SaveChanges();
        }

        public void DeleteLecturer(Lecturer lecturer)
        {
            context.Lecturer.Remove(lecturer);
            context.SaveChanges();
        }

        public void DeleteLecturer(int id)
        {
            Lecturer lecturer = this.GetLecturer(id);
            this.DeleteLecturer(lecturer);
        }

        // Student table
        public List<Student> GetAllStudents()
        {
            return context.Student.ToList();
        }

        public Student GetStudent(int id)
        {
            // todo
            return context.Student.FirstOrDefault(s => s.Id == id);
        }

        public void CreateStudent(Student student)
        {
            context.Student.Add(student);
            context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            context.Student.Update(student);
            context.SaveChanges();
        }

        public void DeleteStudent(Student student)
        {
            context.Student.Remove(student);
            context.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            Student student = this.GetStudent(id);
            this.DeleteStudent(student);
        }

        // Hometask table
        public List<HomeTask> GetAllHomeTasks()
        {
            return context.HomeTask.ToList();
        }

        public HomeTask GetHomeTask(int id)
        {
            return context.HomeTask.FirstOrDefault(t => t.Id == id);
        }

        public void CreateHomeTask(HomeTask homeTask)
        {
            // todo
            context.HomeTask.Add(homeTask);
            context.SaveChanges();
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            context.HomeTask.Update(homeTask);
            context.SaveChanges();
        }

        public void DeleteHomeTask(HomeTask homeTask)
        {
            context.HomeTask.Remove(homeTask);
            context.SaveChanges();
        }

        public void DeleteHomeTask(int id)
        {
            HomeTask homeTask = this.GetHomeTask(id);
            this.DeleteHomeTask(homeTask);
        }

        // HometaskAssessment table
        public List<HomeTaskAssessment> GetAllHomeTaskAssessments()
        {
            return context.HomeTaskAssessment.ToList();
        }

        public HomeTaskAssessment GetHomeTaskAssessment(int id)
        {
            return context.HomeTaskAssessment.FirstOrDefault(a => a.Id == id);
        }

        public void CreateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            context.HomeTaskAssessment.Add(homeTaskAssessment);
            context.SaveChanges();
        }

        public void UpdateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            context.HomeTaskAssessment.Update(homeTaskAssessment);
            context.SaveChanges();
        }

        public void DeleteHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            context.HomeTaskAssessment.Remove(homeTaskAssessment);
            context.SaveChanges();
        }

        public void DeleteHomeTaskAssessment(int id)
        {
            HomeTaskAssessment assessment = this.GetHomeTaskAssessment(id);
            this.DeleteHomeTaskAssessment(assessment);
        }

        public List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId)
        {
            // todo
            return new List<HomeTaskAssessment>();
        }

        // User table
        public User GetUser(User user)
        {
            return context.User
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
        }

        public User GetUserByEmail(string email)
        {
            return context.User.FirstOrDefault(u => u.Email == email);
        }

        public void CreateUser(User user)
        {
                Role userRole = context.Role.FirstOrDefault(r => r.Name == "user");
                if (userRole != null)
                    user.Role = userRole;

                context.User.Add(user);
                context.SaveChanges();
        }
    }
}
