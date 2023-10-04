using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weHelpTax_.net6_.Model
{
    public class TermSalary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? EmpId { get; set; }
        public decimal TaxTerm1 { get; set; }
        public decimal TaxTerm2 { get; set; }
        public decimal TaxTerm3 { get; set; }
    }
}
