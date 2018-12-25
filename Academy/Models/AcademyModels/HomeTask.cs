using System;
using System.Collections.Generic;

namespace Models.AcademyModels
{
    public class HomeTask
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<HomeTaskAssessment> HomeTaskAssessments { get; set; }
    }
}