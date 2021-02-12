using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            User = new User();
            AvailableClasses = new HashSet<AvailableClass>();
            SessionSchedules = new HashSet<SessionSchedule>();
        }

        public string TeacherId { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string HomeNumber { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<AvailableClass> AvailableClasses { get; set; }
        public virtual ICollection<SessionSchedule> SessionSchedules { get; set; }
    }
}
