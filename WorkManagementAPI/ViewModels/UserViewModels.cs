using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagementAPI.ViewModels
{
    public class UserViewModels
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }

        public string Fullname { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Qrcode { get; set; }

        public DateTime? DoB { get; set; }

        public int? GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
