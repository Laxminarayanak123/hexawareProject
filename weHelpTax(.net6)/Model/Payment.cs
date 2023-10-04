using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace weHelpTax_.net6_.Model
{
    public partial class Payment
    {
        [Key]
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public DateTime PaymentTime { get; set; }
        public decimal Amount { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
