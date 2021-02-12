using System;
using System.Collections.Generic;

#nullable disable

namespace ClientSideACMS.Infrastructure.Models
{
    public partial class ClassCategory
    {
        public ClassCategory()
        {
            RegistredClasses = new HashSet<RegistredClass>();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal? IncomeInstructor { get; set; }
        public decimal? IncomeAiu { get; set; }
        public decimal? TotalTutionFee { get; set; }
        public decimal? DiscountedFee { get; set; }

        public virtual ICollection<RegistredClass> RegistredClasses { get; set; }
    }
}
