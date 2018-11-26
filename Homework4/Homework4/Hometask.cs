using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4
{
    class Hometask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int SerialNumber { get; set; }

        public Course Course { get; set; }
        public List<HomeworkMark> HomeworkMarks;

        public Hometask()
        {
            HomeworkMarks = new List<HomeworkMark>();
        }
    }
}
