using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework08.ViewModels
{
    public class CourseLecturersAssignmentViewModel
    {
        public Course Course { get; set; }
        public List<LecturerAssignment> LecturersList { get; set; }
    }

    public class LecturerAssignment
    {
        public Lecturer Lecturer { get; set; }
        public bool IsAssigned { get; set; }
    }
}
