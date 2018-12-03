using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework05;

namespace Homework05.AcademyDbAdapter
{
    static class AcademyDbAdapter
    {
        public static List<Course> GetAllCourses()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Course",
                                                       connection);
                List<Course> courses = new List<Course>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course course = new Course
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            StartDate = reader.GetDateTime(2),
                            EndDate = reader.GetDateTime(3),
                            PassCredits = reader.GetInt32(4)
                        };

                        course.Students = GetCourseStudents(course.Id);
                        course.Lecturers = GetCourseLecturers(course.Id);

                        courses.Add(course);
                    }
                }

                return courses;
            }
        }

        public static List<Student> GetAllStudents()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Student",
                                                       connection);
                List<Student> students = new List<Student>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Birthday = reader.GetDateTime(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4),
                            GithubLink = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Notes = reader.IsDBNull(6) ? "" : reader.GetString(6)
                        };

                        students.Add(student);
                    }
                }

                return students;
            }
        }

        public static List<Lecturer> GetAllLecturers()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Lecturer",
                                                       connection);
                List<Lecturer> lecturers = new List<Lecturer>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lecturer lecturer = new Lecturer
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Birthday = reader.GetDateTime(2),
                        };

                        lecturers.Add(lecturer);
                    }
                }

                return lecturers;
            }
        }

        public static List<Hometask> GetAllHometasks()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM HomeTask",
                                                       connection);
                List<Hometask> hometasks = new List<Hometask>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Hometask hometask = new Hometask();
                        hometask.Id = reader.GetInt32(0);
                        hometask.Date = reader.GetDateTime(1);
                        hometask.Title = reader.GetString(2);
                        hometask.Description = reader.GetString(3);
                        hometask.Number = reader.GetInt16(4);

                        int courseId = reader.GetInt32(5);
                        hometask.Course = GetCourseById(courseId);

                        hometasks.Add(hometask);
                    }
                }

                return hometasks;
            }
        }

        public static List<HomeTaskAssessment> GetAllHomeTasksAssessments()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT * FROM HomeTaskAssessment",
                                                       connection);
                List<HomeTaskAssessment> homeTaskAssessments = new List<HomeTaskAssessment>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HomeTaskAssessment homeTaskAssessment = new HomeTaskAssessment
                        {
                            Id = reader.GetInt32(0),
                            IsComplete = reader.GetBoolean(1),
                            Date = reader.GetDateTime(2),
                        };

                        int studentId = reader.GetInt32(3);
                        int homeTaskId = reader.GetInt32(4);
                        homeTaskAssessment.Student = GetStudentById(studentId);
                        homeTaskAssessment.Hometask = GetHometaskById(homeTaskId);

                        homeTaskAssessments.Add(homeTaskAssessment);
                    }
                }

                return homeTaskAssessments;
            }
        }




        private static List<Student> GetCourseStudents(int courseId)
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand($@"SELECT Id, Name, BirthDate, PhoneNumber, Email, GitHubLink, Notes
                                                          FROM Student AS st
                                                          JOIN StudentCourse AS sc ON sc.CourseId=st.Id
                                                          WHERE sc.CourseId =  {courseId}",
                                                        connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Birthday = reader.GetDateTime(2),
                            PhoneNumber = reader.GetString(3),
                            Email = reader.GetString(4),
                            GithubLink = reader.IsDBNull(5) ? "" : reader.GetString(5),
                            Notes = reader.IsDBNull(6) ? "" : reader.GetString(6)
                        };
                        students.Add(student);
                    }
                }
            }

            return students;
        }

        private static List<Lecturer> GetCourseLecturers(int courseId)
        {
            List<Lecturer> lecturers = new List<Lecturer>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand($@"SELECT Id, Name, BirthDate
                                                          FROM Lecturer AS lec
                                                          JOIN LecturerCourse AS lc ON lc.CourseId=lec.Id
                                                          WHERE lc.CourseId =  {courseId}",
                                                        connection);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lecturer lecturer = new Lecturer
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Birthday = reader.GetDateTime(2),
                        };
                        lecturers.Add(lecturer);
                    }
                }
            }

            return lecturers;
        }

        private static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BaseCourseDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        private static Course GetCourseById(int courseId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand($@"SELECT * 
                                                         FROM Course
                                                         WHERE Id = {courseId}",
                                                       connection);
                Course course = new Course();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        course.Id = reader.GetInt32(0);
                        course.Name = reader.GetString(1);
                        course.StartDate = reader.GetDateTime(2);
                        course.EndDate = reader.GetDateTime(3);
                        course.PassCredits = reader.GetInt32(4);

                        course.Students = GetCourseStudents(course.Id);
                        course.Lecturers = GetCourseLecturers(course.Id);
                    }
                }

                return course;
            }
        }

        private static Student GetStudentById(int studentId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand($@"SELECT * 
                                                         FROM Student
                                                         WHERE Id = {studentId}",
                                                       connection);
                Student student = new Student();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        student.Id = reader.GetInt32(0);
                        student.Name = reader.GetString(1);
                        student.Birthday = reader.GetDateTime(2);
                        student.PhoneNumber = reader.GetString(3);
                        student.Email = reader.GetString(4);
                        student.GithubLink = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        student.Notes = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    }
                }

                return student;
            }
        }

        private static Hometask GetHometaskById(int hometaskId)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand($@"SELECT * 
                                                         FROM Hometask
                                                         WHERE Id = {hometaskId}",
                                                       connection);
                Hometask hometask = new Hometask();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        hometask.Id = reader.GetInt32(0);
                        hometask.Date = reader.GetDateTime(1);
                        hometask.Title = reader.GetString(2);
                        hometask.Description = reader.GetString(3);
                        hometask.Number = reader.GetInt16(4);

                        int courseId = reader.GetInt32(5);
                        hometask.Course = GetCourseById(courseId);
                    }
                }

                return hometask;
            }
        }
    }
}
