using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class LecturerService
    {
        private readonly AcademyRepository academyRepository;

        public LecturerService(AcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public List<Lecturer> GetAllLecturers()
        {
            List<Lecturer> lecturersList = academyRepository.GetAllLecturers();
            return lecturersList;
        }

        public Lecturer GetLecturer(int id)
        {
            Lecturer lecturer = academyRepository.GetLecturer(id);
            return lecturer;
        }

        public void CreateLecturer(Lecturer lecturer)
        {
            academyRepository.CreateLecturer(lecturer);
        }

        public void UpdateLecturer(Lecturer lecturer)
        {
            academyRepository.UpdateLecturer(lecturer);
        }

        public void DeleteLecturer(Lecturer lecturer)
        {
            academyRepository.DeleteLecturer(lecturer);
        }

        public void DeleteLecturer(int id)
        {
            academyRepository.DeleteLecturer(id);
        }
    }
}
