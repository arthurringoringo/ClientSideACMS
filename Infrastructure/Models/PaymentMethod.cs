using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            RegistredClasses = new HashSet<RegistredClass>();
        }

        public string PaymentMethodId { get; set; }
        public string MethodName { get; set; }
        public int? Terms { get; set; }

        public virtual ICollection<RegistredClass> RegistredClasses { get; set; }
    }
}
