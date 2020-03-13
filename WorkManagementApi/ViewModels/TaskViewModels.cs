using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagementApi.ViewModels
{
    public class TaskViewModels
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int? CloneFrom { get; set; }

        public string ContentAssigned { get; set; }

        public string ContentHandlingWork { get; set; }

        public string Description { get; set; }

        public int? Mark { get; set; }

        public DateTime? TimeManagerCommented { get; set; }

        public DateTime? TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public DateTime? DueDate { get; set; }

        public int? Status { get; set; }

        public string StatusName { get; set; }

        public DateTime? CreationTime { get; set; }

        public int? Creator { get; set; }

        public string CreatorName { get; set; }

        public int Processor { get; set; }

        public string ProcesssorName { get; set; }

        public bool? Acceptance { get; set; }

        public String AcceptanceName { get; set; }

        public bool? ConfirmationEnded { get; set; }

        public String ConfirmationEndedName { get; set; }

        public string ImageConfirmation { get; set; }

        public Byte[] Image { get; set; }

        public int? Updater { get; set; }

        public DateTime? TimeUpdated { get; set; }
    }
}
