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
        public static List<Student> GetAllStudents()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * from Student",
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

        public static List<Course> GetAllCourses()
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * from Course",
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
    }
}
