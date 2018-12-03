using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework05
{
    public class HomeTaskAssessment
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public DateTime Date { get; set; }

        public Student Student { get; set; }
        public Hometask Hometask { get; set; }

        public HomeTaskAssessment()
        {

        }

        public HomeTaskAssessment(int id, DateTime complitionDate, bool done)
        {
            Id = id;
            IsComplete = done;
            Date = complitionDate;
        }
    }
}
