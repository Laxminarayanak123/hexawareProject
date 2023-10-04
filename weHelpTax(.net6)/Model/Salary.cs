using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace weHelpTax_.net6_.Model
{
    public partial class Salary
    {
        [Key]
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public decimal Salary1 { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
