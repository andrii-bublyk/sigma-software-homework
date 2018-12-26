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

                var studentsIds = academyDb.StudentCourse.Where(sc => sc.CourseId == course.Id).Select(sc => sc.StudentId);
                List<Student> courseStudents = new List<Student>();
                foreach (var studentId in studentsIds)
                {
                    Student student = academyDb.Student.FirstOrDefault(s => s.Id == studentId);
                    if (student != null)
                    {
                        courseStudents.Add(student);
                    }
                }
                course.Students = courseStudents;

                var lecturersIds = academyDb.LecturerCourse.Where(lc => lc.CourseId == course.Id).Select(lc => lc.LecturerId);
                List<Lecturer> courseLectors = new List<Lecturer>();
                foreach (var lecturerId in lecturersIds)
                {
                    Lecturer lecturer = academyDb.Lecturer.FirstOrDefault(l => l.Id == lecturerId);
                    if (lecturer != null)
                    {
                        courseLectors.Add(lecturer);
                    }
                }
                course.Lecturers = courseLectors;
                
                course.HomeTasks = academyDb.HomeTask.Where(h => h.CourseId == course.Id).ToList();

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
                using (var transaction = academyDb.Database.BeginTransaction())
                {
                    // delete course from lecturer-courcse table
                    var lectureCoursesForDelete = academyDb.LecturerCourse.Where(lc => lc.CourseId == course.Id);
                    foreach (var lcd in lectureCoursesForDelete)
                    {
                        academyDb.LecturerCourse.Remove(lcd);
                    }

                    // delete course from student-courcse table
                    var studentCoursesForDelete = academyDb.StudentCourse.Where(sc => sc.CourseId == course.Id);
                    foreach (var sc in studentCoursesForDelete)
                    {
                        academyDb.StudentCourse.Remove(sc);
                    }

                    // delete assessment from HomeTaskAssessment table
                    // delete hometasks from Hometask table
                    var hometasksForDelete = academyDb.HomeTask.Where(h => h.CourseId == course.Id);
                    foreach (var hd in hometasksForDelete)
                    {
                        var assessmentsForDelete = academyDb.HomeTaskAssessment.Where(ha => ha.HomeTaskId == hd.Id);
                        foreach (var ad in assessmentsForDelete)
                        {
                            academyDb.HomeTaskAssessment.Remove(ad);
                        }
                        academyDb.HomeTask.Remove(hd);
                    }
                    // delete course
                    academyDb.Course.Remove(course);
                    academyDb.SaveChanges();

                    transaction.Commit();
                }
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

        public void AssignStudentsToCourse(int courseId, List<int> assignedStudentsIds)
        {
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                using (var transaction = academyDb.Database.BeginTransaction())
                {
                    List<int> previouslyAssignedstudentsIds = academyDb.StudentCourse.Where(sc => sc.CourseId == courseId).Select(sc => sc.StudentId).ToList();
                    List<int> disassignedStudentsIds = previouslyAssignedstudentsIds.Except(assignedStudentsIds).ToList();
                    List<int> newAssignedStudentsIds = assignedStudentsIds.Except(previouslyAssignedstudentsIds).ToList();

                    // delete disassigned students assessments
                    foreach (var disStId in disassignedStudentsIds)
                    {
                        // todo remove range not necessary
                        academyDb.HomeTaskAssessment.RemoveRange(
                            academyDb.HomeTaskAssessment.Where(ha => ha.StudentId == disStId));
                    }

                    // disassigne disassigned students
                    foreach (var disStId in disassignedStudentsIds)
                    {
                        // todo remove range not necessary
                        academyDb.StudentCourse.RemoveRange(
                            academyDb.StudentCourse.Where(sc => sc.StudentId == disStId));
                    }

                    // assign new assigned students
                    foreach (var asi in newAssignedStudentsIds)
                    {
                        academyDb.StudentCourse.Add(new StudentCourse() { StudentId = asi, CourseId = courseId });
                    }

                    // create assessments for new assigned students
                    List<int> courseHometasksIds = academyDb.HomeTask.Where(h => h.CourseId == courseId).Select(h => h.Id).ToList();
                    foreach (var studentId in newAssignedStudentsIds)
                    {
                        foreach (var hometaskId in courseHometasksIds)
                        {
                            HomeTaskAssessment assessment = new HomeTaskAssessment()
                            {
                                IsComplete = false,
                                Date = DateTime.Now,
                                HomeTaskId = hometaskId,
                                StudentId = studentId
                            };
                            academyDb.HomeTaskAssessment.Add(assessment);
                        }
                    }
                    academyDb.SaveChanges();

                    transaction.Commit();
                }
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
                using (var transaction = academyDb.Database.BeginTransaction())
                {
                    // delete course from lecturer-courcse table
                    var lectureCoursesForDelete = academyDb.LecturerCourse.Where(lc => lc.LecturerId == lecturer.Id);
                    foreach (var lc in lectureCoursesForDelete)
                    {
                        academyDb.LecturerCourse.Remove(lc);
                    }
                    // delete lecturer
                    academyDb.Lecturer.Remove(lecturer);
                    academyDb.SaveChanges();

                    transaction.Commit();
                }
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
                foreach (var assessment in studentAssessments)
                {
                    assessment.HomeTask = academyDb.HomeTask.FirstOrDefault(h => h.Id == assessment.HomeTaskId);
                }

                student.HomeTaskAssessments = studentAssessments;

                List<int> studentCoursesIds = academyDb.StudentCourse.Where(sc => sc.StudentId == student.Id).Select(sc => sc.CourseId).ToList();
                List<Course> studentCourses = new List<Course>();
                foreach(var ci in studentCoursesIds)
                {
                    Course course = academyDb.Course.FirstOrDefault(c => c.Id == ci);
                    if (course != null)
                    {
                        studentCourses.Add(course);
                    }
                }
                student.Courses = studentCourses;

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
                using (var transaction = academyDb.Database.BeginTransaction())
                {
                    // delete student assesments
                    var studentAssessmentsForDelete = academyDb.HomeTaskAssessment.Where(a => a.StudentId == student.Id);
                    foreach (var sa in studentAssessmentsForDelete)
                    {
                        academyDb.HomeTaskAssessment.Remove(sa);
                    }
                    // delete student from student-courcse table
                    var studentCoursesForDelete = academyDb.StudentCourse.Where(sc => sc.StudentId == student.Id);
                    foreach (var sc in studentCoursesForDelete)
                    {
                        academyDb.StudentCourse.Remove(sc);
                    }

                    // delete student
                    academyDb.Student.Remove(student);
                    academyDb.SaveChanges();

                    transaction.Commit();
                }
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
            // create hometask
            using (AcademyContext academyDb = new AcademyContext(options))
            {
                academyDb.HomeTask.Add(homeTask);
                academyDb.SaveChanges();
            }

            using (AcademyContext academyDb = new AcademyContext(options))
            {
                if (academyDb.StudentCourse.Where(sc => sc.CourseId == homeTask.CourseId).Count() > 0)
                {
                    // create assessments for all students in course without assessments
                    List<int> courseHometasksIds = academyDb.HomeTask.Where(h => h.CourseId == homeTask.CourseId).Select(h => h.Id).ToList();
                    List<int> courseStudentsIds = academyDb.StudentCourse.Where(st => st.CourseId == homeTask.CourseId).Select(st => st.StudentId).ToList();
                    foreach (var hi in courseHometasksIds)
                    {
                        if (academyDb.HomeTaskAssessment.Where(ha => ha.HomeTaskId == hi).Count() == 0)
                        {
                            foreach (var csi in courseStudentsIds)
                            {
                                academyDb.HomeTaskAssessment.Add(
                                    new HomeTaskAssessment()
                                    {
                                        IsComplete = false,
                                        Date = DateTime.Now,
                                        HomeTaskId = homeTask.Id,
                                        StudentId = csi
                                    });
                            }
                        }
                    }
                    academyDb.SaveChanges();
                }
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
                using (var transaction = academyDb.Database.BeginTransaction())
                {
                    // delete hometask assessments
                    var hometaskAssessmentsForDelete = academyDb.HomeTaskAssessment.Where(a => a.HomeTaskId == homeTask.Id);
                    foreach (var ha in hometaskAssessmentsForDelete)
                    {
                        academyDb.HomeTaskAssessment.Remove(ha);
                    }

                    // delete hometask
                    academyDb.HomeTask.Remove(homeTask);
                    academyDb.SaveChanges();

                    transaction.Commit();
                }
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
                    assessment.HomeTask.Course = academyDb.Course.FirstOrDefault(c => c.Id == assessment.HomeTask.CourseId);
                }

                return studentAssessments;
            }
        }
    }
}
