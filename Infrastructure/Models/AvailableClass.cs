using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class AvailableClass
    {
        public AvailableClass()
        {
            PaidSessions = new HashSet<PaidSession>();
            RegistredClasses = new HashSet<RegistredClass>();
            SessionSchedules = new HashSet<SessionSchedule>();
        }

        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<PaidSession> PaidSessions { get; set; }
        public virtual ICollection<RegistredClass> RegistredClasses { get; set; }
        public virtual ICollection<SessionSchedule> SessionSchedules { get; set; }
    }
}
