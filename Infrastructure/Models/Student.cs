using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class Student
    {
        public Student()
        {
            User = new User();
            PaidSessions = new HashSet<PaidSession>();
            RegistredClasses = new HashSet<RegistredClass>();
            SessionSchedules = new HashSet<SessionSchedule>();
        }

        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Sex { get; set; }
        public string ParentGuardianName { get; set; }
        public string Address { get; set; }
        public string HomeNumber { get; set; }
        public string SchoolName { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PaidSession> PaidSessions { get; set; }
        public virtual ICollection<RegistredClass> RegistredClasses { get; set; }
        public virtual ICollection<SessionSchedule> SessionSchedules { get; set; }
    }
}
