using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class PaidSession
    {
        public string PaidSessionId { get; set; }
        public string StudentId { get; set; }
        public string ClassId { get; set; }
        public DateTime? DatePaid { get; set; }
        public string PictureLink { get; set; }
        public bool? PaymentAccepted { get; set; }
        public DateTime? PaymentsMonth { get; set; }

        public virtual AvailableClass Class { get; set; }
        public virtual Student Student { get; set; }
    }
}
