using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class HomeTaskAssessmentService
    {
        private readonly AcademyRepository academyRepository;

        public HomeTaskAssessmentService(AcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public List<HomeTaskAssessment> GetAllHomeTasks()
        {
            List<HomeTaskAssessment> homeTaskAssessmentList = academyRepository.GetAllHomeTaskAssessments();
            return homeTaskAssessmentList;
        }

        public HomeTaskAssessment GetHomeTaskAssessment(int id)
        {
            HomeTaskAssessment homeTaskAssessment = academyRepository.GetHomeTaskAssessment(id);
            return homeTaskAssessment;
        }

        public void CreateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            academyRepository.CreateHomeTaskAssessment(homeTaskAssessment);
        }

        public void UpdateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            academyRepository.UpdateHomeTaskAssessment(homeTaskAssessment);
        }

        public void DeleteHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            academyRepository.DeleteHomeTaskAssessment(homeTaskAssessment);
        }

        public void DeleteHomeTaskAssessment(int id)
        {
            academyRepository.DeleteHomeTaskAssessment(id);
        }

        public List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId)
        {
            return academyRepository.GetHomeTaskAssessmentsByStudentId(studentId);
        }

    }
}
