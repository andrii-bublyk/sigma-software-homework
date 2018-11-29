using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework04
{
    class HometaskMark
    {
        public int Id { get; set; }
        public DateTime ComplitionDate;
        public bool Done { get; set; }

        public Course Course { get; set; }
        public Hometask Hometask { get; set; }
    }
}
