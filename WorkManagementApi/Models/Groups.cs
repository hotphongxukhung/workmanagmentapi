using System;
using System.Collections.Generic;

namespace WorkManagementApi.Models
{
    public partial class Groups
    {
        public Groups()
        {
            Users = new HashSet<Users>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
