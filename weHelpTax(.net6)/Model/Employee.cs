using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace weHelpTax_.net6_.Model
{
    public partial class Employee
    {
        public Employee()
        {
            Payment = new HashSet<Payment>();
            Salary = new HashSet<Salary>();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DepartmentId { get; set; }
        public bool? IsAlive { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Salary> Salary { get; set; }
    }
}
