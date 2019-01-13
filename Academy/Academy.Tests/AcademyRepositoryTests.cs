using DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Models.AcademyModels;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Academy.Tests
{
    public class AcademyRepositoryTests
    {
        [Fact]
        public void Student_SavedToDatabase()
        {
            var options = new DbContextOptionsBuilder<AcademyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Run the test against one UniversityContext of the context
            using (var context = new AcademyContext(options))
            {
                IAcademyRepository academyRepository = new AcademyRepository2(context);
                academyRepository.CreateStudent(new Student() { Name = "Test" });
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new AcademyContext(options))
            {
                Assert.Equal(1, context.Student.Count());
                Assert.Equal("Test", context.Student.Single().Name);
                Assert.True(context.Student.Single().Id != default(int));
            }
        }
    }
}
