using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public interface IRepository
    {
        // Course table
        List<Course> GetAllCourses();
        Course GetCourse(int id);
        Course CreateCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);

        // Student table
        List<Student> GetAllStudents(bool loadAllDependencies = true);
        Student GetStudentById(int id, bool loadAllDependencies = true);
        Student CreateStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int studentId);

        // StudentCourse table
        void SetStudentsToCourse(int courseId, IEnumerable<int> studentsId);

        // Lecturer table
        List<Lecturer> GetAllLecturers();
        Lecturer GetLecturerById(int id);
        Lecturer CreateLecturer(Lecturer lecturer);
        void UpdateLecturer(Lecturer lecturer);
        void DeleteLecturer(int id);

        // LecturerCourse table
        void SetLecturersToCourse(int courseId, IEnumerable<int> lecturerIds);

        // Hometask table
        HomeTask GetHomeTaskById(int id, bool loadAllDependencies = true);
        HomeTask CreateHomeTask(HomeTask homeTask, int courseId);
        void UpdateHomeTask(HomeTask homeTask);
        void DeleteHomeTask(int homeTaskId);

        // HometaskAssessment table
        HomeTaskAssessment GetHomeTaskAssessmentById(int assessmentId);
        void CreateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments);
        void UpdateHomeTaskAssessments(List<HomeTaskAssessment> homeTaskHomeTaskAssessments);
        List<HomeTaskAssessment> GetHomeTaskAssessmentsByHomeTaskId(int homeTaskId);
        List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId);
    }
}
