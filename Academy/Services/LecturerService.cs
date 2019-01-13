using DataAccess.EF;
using Models.AcademyModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class LecturerService
    {
        private readonly IAcademyRepository academyRepository;

        public LecturerService()
        {
        }

        public LecturerService(IAcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public virtual List<Lecturer> GetAllLecturers()
        {
            if (academyRepository == null)
            {
                return new List<Lecturer>();
            }
            List<Lecturer> lecturersList = academyRepository.GetAllLecturers();
            return lecturersList;
        }

        public virtual Lecturer GetLecturer(int id)
        {
            if (academyRepository == null)
            {
                return null;
            }
            Lecturer lecturer = academyRepository.GetLecturer(id);
            return lecturer;
        }

        public virtual bool CreateLecturer(Lecturer lecturer)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.CreateLecturer(lecturer);
            return true;
        }

        public virtual bool UpdateLecturer(Lecturer lecturer)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.UpdateLecturer(lecturer);
            return true;
        }

        public virtual bool DeleteLecturer(Lecturer lecturer)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteLecturer(lecturer);
            return true;
        }

        public virtual bool DeleteLecturer(int id)
        {
            if (academyRepository == null)
            {
                return false;
            }
            academyRepository.DeleteLecturer(id);
            return true;
        }

        public virtual bool IsLecturerExisted(Lecturer lecturer)
        {
            Lecturer dBlecturer = GetLecturer(lecturer.Id);
            if (dBlecturer == null)
                return false;

            return dBlecturer.Equals(lecturer);
        }
    }
}
