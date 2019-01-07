using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Models.AcademyModels;
using Models.AuthorizationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EF
{
    public class AcademyContext : DbContext
    {
        #region forCreatingDb
        //public AcademyContext()
        //{
        //    Database.EnsureCreated();
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AcademyDb;Trusted_Connection=True;");
        //}
        #endregion

        private readonly IOptions<RepositoryOptions> options;

        public AcademyContext(IOptions<RepositoryOptions> options)
        {
            this.options = options;
        }
        
        #region normalWorkDb
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(options.Value.DefaultConnectionString);
            //optionsBuilder.UseLazyLoadingProxies();
        }
        #endregion

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }

        public DbSet<Course> Course { get; set; }
        public DbSet<Lecturer> Lecturer { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<HomeTask> HomeTask { get; set; }
        public DbSet<HomeTaskAssessment> HomeTaskAssessment { get; set; }

        public DbSet<LecturerCourse> LecturerCourse { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<LecturerCourse>();
            //modelBuilder.Entity<StudentCourse>();
            modelBuilder.ApplyConfiguration(new CoursesConfiguration());
            modelBuilder.ApplyConfiguration(new LecturerConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new HomeTaskConfiguration());
            modelBuilder.ApplyConfiguration(new HomeTaskAssessmentConfiguration());

            modelBuilder.ApplyConfiguration(new LecturerCourseConfiguration());
            modelBuilder.ApplyConfiguration(new StudentCourseConfiguration());


            // create roles
            string adminRoleName = "admin";
            string userRoleName = "user";
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

            // add admin
            string adminEmail = "admin@gmail.com";
            string adminPassword = "qwerty";
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            // add roles into db
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            // add admin user into db
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }

    public class CoursesConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.StartDate).IsRequired().HasColumnType("date");
            builder.Property(c => c.EndDate).IsRequired().HasColumnType("date");

            builder.Ignore(c => c.Lecturers);
            builder.Ignore(c => c.Students);
            builder.Ignore(c => c.HomeTasks);
        }
    }

    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.Property(l => l.Name).IsRequired().HasMaxLength(50);
            builder.Property(l => l.BirthDate).IsRequired().HasColumnType("date");

            builder.Ignore(c => c.Courses);
        }
    }

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(50);
            builder.Property(s => s.BirthDate).IsRequired().HasColumnType("date");
            builder.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(12);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(50);
            builder.Property(s => s.GitHubLink).IsRequired().HasMaxLength(256);
            builder.Property(s => s.Notes).IsRequired().HasColumnType("text");

            builder.Ignore(c => c.Courses);
            builder.Ignore(c => c.HomeTaskAssessments);
        }
    }

    public class HomeTaskConfiguration : IEntityTypeConfiguration<HomeTask>
    {
        public void Configure(EntityTypeBuilder<HomeTask> builder)
        {
            builder.Property(h => h.Title).IsRequired().HasMaxLength(50);
            builder.Property(h => h.Date).IsRequired().HasColumnType("date");

            builder.Ignore(c => c.Course);
            builder.Ignore(c => c.HomeTaskAssessments);

            //builder.HasOne(h => h.Course)
            //       .WithMany(c => c.HomeTasks)
            //       .HasForeignKey(h => h.CourseId);
        }
    }

    public class HomeTaskAssessmentConfiguration : IEntityTypeConfiguration<HomeTaskAssessment>
    {
        public void Configure(EntityTypeBuilder<HomeTaskAssessment> builder)
        {
            builder.Property(a => a.Date).IsRequired().HasColumnType("date");

            //builder.HasOne(a => a.Student)
            //       .WithMany(s => s.HomeTaskAssessments)
            //       .HasForeignKey(a => a.StudentId);

            //builder.HasOne(a => a.HomeTask)
            //       .WithMany(h => h.HomeTaskAssessments)
            //       .HasForeignKey(a => a.HomeTaskId);

            builder.Ignore(c => c.HomeTask);
            builder.Ignore(c => c.Student);
        }
    }

    public class LecturerCourseConfiguration : IEntityTypeConfiguration<LecturerCourse>
    {
        public void Configure(EntityTypeBuilder<LecturerCourse> builder)
        {
            //builder.HasKey(t => new { t.LecturerId, t.CourseId });

            //builder.HasOne(lc => lc.Lecturer)
            //       .WithMany(l => l.LecturerCourses)
            //       .HasForeignKey(lc => lc.LecturerId)
            //       .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(lc => lc.Course)
            //       .WithMany(c => c.LecturerCourses)
            //       .HasForeignKey(lc => lc.CourseId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            //builder.HasKey(t => new { t.StudentId, t.CourseId });

            //builder.HasOne(sc => sc.Student)
            //       .WithMany(s => s.StudentCourses)
            //       .HasForeignKey(sc => sc.StudentId);

            //builder.HasOne(sc => sc.Course)
            //       .WithMany(c => c.StudentCourses)
            //       .HasForeignKey(sc => sc.CourseId);
        }
    }
}
