using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class HometaskService
    {
        private readonly IAcademyRepository academyRepository;

        public HometaskService()
        {
        }

        public HometaskService(IAcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public virtual List<HomeTask> GetAllHomeTasks()
        {
            if (academyRepository == null)
            {
                return new List<HomeTask>();
            }
            List<HomeTask> homeTaskList = academyRepository.GetAllHomeTasks();
            return homeTaskList;
        }

        public virtual HomeTask GetHomeTask(int id)
        {
            if (academyRepository == null)
            {
                return null;
            }
            HomeTask homeTask = academyRepository.GetHomeTask(id);
            return homeTask;
        }

        public virtual bool CreateHomeTask(HomeTask homeTask)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.CreateHomeTask(homeTask);
            return true;
        }

        public virtual bool UpdateHomeTask(HomeTask homeTask)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.UpdateHomeTask(homeTask);
            return true;
        }

        public virtual bool DeleteHomeTask(HomeTask homeTask)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteHomeTask(homeTask);
            return true;
        }

        public virtual bool DeleteHomeTask(int id)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteHomeTask(id);
            return true;
        }
    }
}
