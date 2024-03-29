﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int? MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int? GroupId { get; set; }
        public virtual List<RoleResponse> Roles { get; set; }
    }
}
