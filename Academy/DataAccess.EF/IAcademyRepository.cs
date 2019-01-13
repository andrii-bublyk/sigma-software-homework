using Models.AcademyModels;
using Models.AuthorizationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EF
{
    public interface IAcademyRepository
    {
        // Course table
        List<Course> GetAllCourses();
        Course GetCourse(int id);
        void CreateCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        void DeleteCourse(int id);
        void AssignStudentsToCourse(int courseId, IEnumerable<int> assignedStudentsIds);
        void AssignLecturersToCourse(int courseId, List<int> lecturersIds);

        // Lecturer table
        List<Lecturer> GetAllLecturers();
        Lecturer GetLecturer(int id);
        void CreateLecturer(Lecturer lecturer);
        void UpdateLecturer(Lecturer lecturer);
        void DeleteLecturer(Lecturer lecturer);
        void DeleteLecturer(int id);

        // Student table
        List<Student> GetAllStudents();
        Student GetStudent(int id);
        void CreateStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        void DeleteStudent(int id);

        // Hometask table
        List<HomeTask> GetAllHomeTasks();
        HomeTask GetHomeTask(int id);
        void CreateHomeTask(HomeTask homeTask);
        void UpdateHomeTask(HomeTask homeTask);
        void DeleteHomeTask(HomeTask homeTask);
        void DeleteHomeTask(int id);

        // HometaskAssessment table
        List<HomeTaskAssessment> GetAllHomeTaskAssessments();
        HomeTaskAssessment GetHomeTaskAssessment(int id);
        void CreateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment);
        void UpdateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment);
        void DeleteHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment);
        void DeleteHomeTaskAssessment(int id);
        List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId);

        // User table
        User GetUser(User user);
        User GetUserByEmail(string email);
        void CreateUser(User user);
    }
}
