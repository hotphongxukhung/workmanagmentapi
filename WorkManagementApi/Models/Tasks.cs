using System;
using System.Collections.Generic;

namespace WorkManagementApi.Models
{
    public partial class Tasks
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
        public DateTime? CreationTime { get; set; }
        public int? Creator { get; set; }
        public int Processor { get; set; }
        public bool? Acceptance { get; set; }
        public bool? ConfirmationEnded { get; set; }
        public string ImageConfirmation { get; set; }
        public int? Updater { get; set; }
        public DateTime? TimeUpdated { get; set; }

        public virtual Users CreatorNavigation { get; set; }
        public virtual Users ProcessorNavigation { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual Users UpdaterNavigation { get; set; }
    }
}
