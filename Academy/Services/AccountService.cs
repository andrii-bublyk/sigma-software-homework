using DataAccess.EF;
using Models.AuthorizationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AccountService
    {
        private readonly AcademyRepository academyRepository;

        public AccountService()
        {
        }

        public AccountService(AcademyRepository academyRepository)
        {
            this.academyRepository = academyRepository;
        }

        public virtual User GetUser(User user)
        {
            return academyRepository.GetUser(user);
        }


        public User GetUserByEmail(string email)
        {
            return academyRepository.GetUserByEmail(email);
        }

        public virtual void CreateUser(User user)
        {
            academyRepository.CreateUser(user);
        }
    }
}
