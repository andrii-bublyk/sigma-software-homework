using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.ViewModels
{
    public class CourseLecturersAssignmentViewModel
    {
        public Course Course { get; set; }
        public List<LecturerAssignment> LecturersAssignmentsList { get; set; }
    }

    public class LecturerAssignment
    {
        public Lecturer Lecturer { get; set; }
        public bool IsAssigned { get; set; }
    }
}
