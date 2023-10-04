using Microsoft.EntityFrameworkCore;
using weHelpTax_.net6_.Model;

namespace weHelpTax_.net6_.Model
{
    public class taxPayerApplicationContext : DbContext
    {
        public taxPayerApplicationContext()
        {
        }

        public taxPayerApplicationContext(DbContextOptions<taxPayerApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }
        public virtual DbSet<TaxInfo> TaxInfo { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Terms> Terms { get; set; }
        public virtual DbSet<TermSalary> TermSalary { get; set; }



    }
}
