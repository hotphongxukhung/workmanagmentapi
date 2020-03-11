using System;
using System.Collections.Generic;

namespace WorkManagementAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            Groups = new HashSet<Groups>();
            TasksCreatorNavigation = new HashSet<Tasks>();
            TasksProcessorNavigation = new HashSet<Tasks>();
        }

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

        public virtual Groups Group { get; set; }
        public virtual Roles Role { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
        public virtual ICollection<Tasks> TasksCreatorNavigation { get; set; }
        public virtual ICollection<Tasks> TasksProcessorNavigation { get; set; }
    }
}
