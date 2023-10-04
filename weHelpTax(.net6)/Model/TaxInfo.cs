using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace weHelpTax_.net6_.Model
{
    public partial class TaxInfo
    {
        [Key]
        public int Id { get; set; }
        public double SalaryFrom { get; set; }
        public double SalaryTo { get; set; }
        public double TaxRate { get; set; }
    }
}
