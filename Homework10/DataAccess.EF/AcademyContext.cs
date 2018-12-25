using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EF
{
    class AcademyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AcademyDemo;Trusted_Connection=True;");
        }

        //public DbSet<Student> Student { get; set; }
        //public DbSet<Course> Course { get; set; }
        //public DbSet<Lecturer> Lecturer { get; set; }
        //public DbSet<HomeTask> HomeTask { get; set; }
        public DbSet<Bla> Blas { get; set; }
    }

    class Bla
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
