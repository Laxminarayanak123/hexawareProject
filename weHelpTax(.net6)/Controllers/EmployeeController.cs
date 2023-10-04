using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using log4net;
using Serilog;
using weHelpTax_.net6_.Model;

namespace weHelpTax_.net6_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        //private static readonly ILog log = LogManager.GetLogger(typeof(EmployeeController));

        private readonly ILogger<EmployeeController> _logger;

        private readonly taxPayerApplicationContext taxContext;

        public EmployeeController(taxPayerApplicationContext tax , ILogger<EmployeeController> logger)
        {
            taxContext = tax;
            _logger = logger;
            _logger.LogInformation("employee controller called");
        }
        [HttpGet]
        public IActionResult Index()
        {

            _logger.LogInformation("employee all data called");

            List<Employee> employee = taxContext.Employee.ToList();

            List<Employee> l2 = new List<Employee>();
            foreach(Employee e in employee)
            {
                if(e.IsAlive == true)
                {
                    l2.Add(e);
                }
            }
            
            if(employee.Count<1)
            {
                return StatusCode(204);
            }
            else
            {
                return StatusCode(200, l2);
            }
           
        }
        [HttpGet("GetEmpDetails/{EmployeeId}")]
        public IActionResult GetEmployeeDetail(int EmployeeId)
        {
            Employee employee = taxContext.Employee.Find(EmployeeId);
            if (employee == null)
            {
                return StatusCode(404, "Employee not available");
            }
            else
            {
                return StatusCode(200, employee);
            }

        }

        [HttpGet("SearchEmpDetails/{EmployeeString}")]
        public IActionResult GetEmployeeDetail(string EmployeeString)
        {
            _logger.LogInformation("get specific employee");

            List<Employee> employees = taxContext.Employee
            .Where(e =>
                e.FirstName.ToLower().Contains(EmployeeString.ToLower()) ||
                e.LastName.ToLower().Contains(EmployeeString.ToLower())
            )
            .ToList();

            employees = employees
            .GroupBy(e => e.Id)
            .Select(group => group.First())
            .ToList();
            //Employee employee = taxContext.Employee.Find(EmployeeId);
            if (employees.Count == 0)
            {
                //"Employees not available"
                return StatusCode(404, "Employees not available");
            }
            else
            {
                return StatusCode(200, employees);
            }

        }

        [HttpDelete("DeleteEmp/{EmployeeId}")]
        public IActionResult DeleteEmployee(int EmployeeId)
        {
            _logger.LogInformation("delete an employee called");

            Console.WriteLine(EmployeeId);
            Employee employee = taxContext.Employee.Find(EmployeeId);
            if (employee == null)
            {
                return StatusCode(404, "Course Id not available");
            }
            else
            {
                //taxContext.Employee.Remove(employee);
                employee.IsAlive= false;
                taxContext.SaveChanges();
                return Ok();
            }

        }


        [HttpPost("AddEmp")]
        public IActionResult AddNewEmployee([FromBody]Employee employee)
        {
            _logger.LogInformation("Add employee called");

            Console.WriteLine("in add emp "+employee.FirstName);
            if (employee.HireDate == null)
            {
                Console.WriteLine("inside hiredate");
                employee.HireDate = DateTime.Now;
            }
            if(employee.DateOfBirth == null)
            {
                employee.DateOfBirth = DateTime.Now;
            }
            employee.IsAlive = true;

                taxContext.Employee.Add(employee);
                taxContext.SaveChanges();
                return Ok();
          
            

        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody]Employee employee)
        {
            _logger.LogInformation("Update employee called over!");


            //employee.HireDate = (DateTime)employee.HireDate;
            //employee.DateOfBirth= (DateTime)employee.DateOfBirth;

            //employee.IsAlive= true;

            Console.WriteLine(employee);

            Employee original = taxContext.Employee.Find(id);

            if (employee.FirstName != null)
            {
                Console.WriteLine(employee.FirstName);
                original.FirstName = employee.FirstName;
            }
            if(employee.LastName != null)
            {
                Console.WriteLine(employee.LastName);
                original.LastName = employee.LastName;
            }
            if(employee.Email!= null)
            { 
                Console.WriteLine(employee.Email);
                original.Email = employee.Email;
            }
            if(employee.DepartmentId != null)
            {
                Console.WriteLine(employee.DepartmentId);
                original.DepartmentId = employee.DepartmentId;
            }
            if(employee.PhoneNumber!= null)
            {
                Console.WriteLine(employee.PhoneNumber);
                original.PhoneNumber = employee.PhoneNumber;
            }
            if(employee.DateOfBirth!= null)
            {
                Console.WriteLine(employee.DateOfBirth);

                original.DateOfBirth = employee.DateOfBirth;
            }
            if(employee.HireDate!= null)
            {
                Console.WriteLine(employee.HireDate);
                original.HireDate = employee.HireDate;
            }



            //taxContext.Employee.Update(employee);
            taxContext.SaveChanges();

            return Ok();
        }
    }
}
