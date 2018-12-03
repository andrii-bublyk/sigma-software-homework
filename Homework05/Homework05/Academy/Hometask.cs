using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework05
{
    public class Hometask
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }

        public Course Course { get; set; }

        public Hometask()
        {

        }

        public Hometask(int id, string name, string description, DateTime date, int serialNumber)
        {
            Id = id;
            Date = date;
            Title = name;
            Description = description;
            Number = serialNumber;
        }
    }
}
