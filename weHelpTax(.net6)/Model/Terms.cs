using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace weHelpTax_.net6_.Model
{
    public class Terms
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? EmpId { get; set; }
        public bool Term1 { get; set; }
        public bool Term2 { get; set; }
        public bool Term3 { get; set; }
    }
}
