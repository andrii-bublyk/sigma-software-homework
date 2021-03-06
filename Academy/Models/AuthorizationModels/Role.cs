﻿using System.Collections.Generic;

namespace Models.AuthorizationModels
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
