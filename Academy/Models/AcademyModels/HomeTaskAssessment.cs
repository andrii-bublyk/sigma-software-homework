using System;

namespace Models.AcademyModels
{
    public class HomeTaskAssessment
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public DateTime Date { get; set; }

        public int HomeTaskId { get; set; }
        public HomeTask HomeTask { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}