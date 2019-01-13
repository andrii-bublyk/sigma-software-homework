using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class HomeTaskAssessmentService
    {
        private readonly IAcademyRepository academyRepository;

        public HomeTaskAssessmentService()
        {
        }

        public HomeTaskAssessmentService(IAcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public virtual List<HomeTaskAssessment> GetAllHomeTasks()
        {
            if (academyRepository == null)
            {
                return new List<HomeTaskAssessment>();
            }
            List<HomeTaskAssessment> homeTaskAssessmentList = academyRepository.GetAllHomeTaskAssessments();
            return homeTaskAssessmentList;
        }

        public virtual HomeTaskAssessment GetHomeTaskAssessment(int id)
        {
            if (academyRepository == null)
            {
                return null;
            }
            HomeTaskAssessment homeTaskAssessment = academyRepository.GetHomeTaskAssessment(id);
            return homeTaskAssessment;
        }

        public virtual bool CreateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.CreateHomeTaskAssessment(homeTaskAssessment);
            return true;
        }

        public virtual bool UpdateHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.UpdateHomeTaskAssessment(homeTaskAssessment);
            return true;
        }

        public virtual bool DeleteHomeTaskAssessment(HomeTaskAssessment homeTaskAssessment)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteHomeTaskAssessment(homeTaskAssessment);
            return true;
        }

        public virtual bool DeleteHomeTaskAssessment(int id)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteHomeTaskAssessment(id);
            return true;
        }

        public virtual List<HomeTaskAssessment> GetHomeTaskAssessmentsByStudentId(int studentId)
        {
            if (academyRepository == null)
            {
                return new List<HomeTaskAssessment>(); ;
            }
            return academyRepository.GetHomeTaskAssessmentsByStudentId(studentId);
        }
    }
}
