using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class RegistredClass
    {
        public string RegistredClassId { get; set; }
        public string StudentId { get; set; }
        public string CategoryId { get; set; }
        public string ClassId { get; set; }
        public bool ConfirmedByTeacher { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Day { get; set; }
        public TimeSpan? Time { get; set; }
        public string PaymentMethodId { get; set; }
        public bool? FullyPaid { get; set; }
        public string ClassReportId { get; set; }

        public virtual ClassCategory Category { get; set; }
        public virtual AvailableClass Class { get; set; }
        public virtual ClassReport ClassReport { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Student Student { get; set; }
    }
}
