using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.ViewModels
{
    public class CourseStudentsAssignmentViewModel
    {
        public Course Course { get; set; }
        public List<StudentAssignment> StudentsAssignmentsList { get; set; }
    }

    public class StudentAssignment
    {
        public Student Student { get; set; }
        public bool IsAssigned { get; set; }
    }
}
