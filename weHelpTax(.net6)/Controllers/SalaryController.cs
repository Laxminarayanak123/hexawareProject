using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using weHelpTax_.net6_.Model;


namespace weHelpTax_.net6_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly taxPayerApplicationContext salContext;

        public SalaryController(taxPayerApplicationContext tax)
        {
            salContext = tax;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Salary> salary = salContext.Salary.ToList();

            if (salary.Count < 1)
            {
                return StatusCode(204);
            }
            else
            {
                return StatusCode(200, salary);
            }

        }

        [HttpGet("GetSal/{id}")]
        public IActionResult GetEmployeeDetail(int id)
        {
            //Employee employee = salContext.Employee.Find(EmployeeId);
            System.Console.WriteLine("hello");
            List<Salary> l1 = salContext.Salary.ToList();
            foreach(Salary s in l1)
            {
                if (id == s.Id)
                {
                    return StatusCode(200,s);
                }
            }

                return StatusCode(404, "Emp id not available");


        }

        [HttpDelete("DeleteSal/{SalId}")]
        public IActionResult DeleteSalary(int SalId)
        {
            //Employee employee = salContext.Employee.Find(EmployeeId);
            Salary sal = salContext.Salary.Find(SalId);
            if (sal == null)
            {
                return StatusCode(404, "not available");
            }
            else
            {
                salContext.Salary.Remove(sal);
                salContext.SaveChanges();
                return Ok();
            }

        }


        [HttpPost("AddSal")]
        public IActionResult AddNewEmployee([FromQuery]int empId, [FromQuery]string sal)
        {
            decimal sal1 = decimal.Parse(sal);
            int id = empId;

            //checking if the employee exists in the employee table
            bool ref1 = false;
            List<Employee> l1 = salContext.Employee.ToList();
            foreach(Employee emp in l1)
            {
                if(emp.Id== id)
                {
                    ref1 = true;
                }
            }

            //checking if the employee exists in the salary table
            bool ref2 = true;
            List<Salary>l2 = salContext.Salary.ToList();
            foreach(Salary salary in l2)
            {
                if(salary.EmpId== id)
                {
                    ref2 = false;
                }
            }

            if (ref1 && ref2)
            {
                Salary s1 = new Salary() { EmpId=id, Salary1=sal1};
                salContext.Salary.Add(s1);
                salContext.SaveChanges();
                return Ok();
            }
            else
            {
                return StatusCode(403,"no employee found with the given id  or salary of the employee already exists");
            }
        }


        [HttpPut("UpdateSal")]
        public IActionResult UpdateSalary([FromQuery]int id2 , [FromQuery]decimal salary1) 
        {
            //System.Console.WriteLine("byee");
            //decimal sal1 = decimal.Parse(salary1);

            Console.WriteLine(" "+id2+" "+salary1);
            Salary s = salContext.Salary.Find(id2);

            s.Salary1 = salary1;

            salContext.SaveChanges();


            return Ok();
        }
    }
}
