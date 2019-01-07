using DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Academy.Tests
{
    public class AcademyRepositoryTests
    {
        //[Fact]
        //public void Student_SavedToDatabase()
        //{
        //    var options = new DbContextOptionsBuilder<AcademyContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDatabase")
        //        .Options;

        //    // Run the test against one UniversityContext of the context
        //    using (var context = new AcademyContext(options))
        //    {
        //        var universityRepository = new CourseServise(context);
        //        universityRepository.Create(new Student() { Name = "Test" });
        //    }

        //    // Use a separate instance of the context to verify correct data was saved to database
        //    using (var context = new UniversityContext(options))
        //    {
        //        Assert.Equal(1, context.Students.Count());
        //        Assert.Equal("Test", context.Students.Single().Name);
        //        Assert.True(context.Students.Single().Id != default(int));
        //    }
        //}
    }
}
