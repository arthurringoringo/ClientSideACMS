using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class SessionSchedule
    {
        public string ScheduleId { get; set; }
        public string TeacherId { get; set; }
        public string ClassId { get; set; }
        public string StudentId { get; set; }
        public TimeSpan? Time { get; set; }
        public string Day { get; set; }
        public string Remarks { get; set; }

        public virtual AvailableClass Class { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
