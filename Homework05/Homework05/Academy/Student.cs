using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework05
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GithubLink { get; set; }
        public string Notes { get; set; }

        public Student()
        {

        }

        public Student(int id, string name, string phone, string email, string github)
        {
            Id = id;
            Name = name;
            PhoneNumber = phone;
            Email = email;
            GithubLink = github;
        }
    }
}
