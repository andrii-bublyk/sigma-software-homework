using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class HometaskService
    {
        private readonly AcademyRepository academyRepository;

        public HometaskService(AcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public List<HomeTask> GetAllHomeTasks()
        {
            List<HomeTask> homeTaskList = academyRepository.GetAllHomeTasks();
            return homeTaskList;
        }

        public HomeTask GetHomeTask(int id)
        {
            HomeTask homeTask = academyRepository.GetHomeTask(id);
            return homeTask;
        }

        public void CreateHomeTask(HomeTask homeTask)
        {
            academyRepository.CreateHomeTask(homeTask);
        }

        public void UpdateHomeTask(HomeTask homeTask)
        {
            academyRepository.UpdateHomeTask(homeTask);
        }

        public void DeleteHomeTask(HomeTask homeTask)
        {
            academyRepository.DeleteHomeTask(homeTask);
        }

        public void DeleteHomeTask(int id)
        {
            academyRepository.DeleteHomeTask(id);
        }
    }
}
