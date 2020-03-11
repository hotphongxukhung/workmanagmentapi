using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagementAPI.ViewModels
{
    public class GroupViewModels
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int? MangerId { get; set; }
    }
}
