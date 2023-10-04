//using JWTWebAuthentication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using weHelpTax_.net6_.Model;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Tls;
//using System.Net.Mail;

namespace weHelpTax_.net6_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {

        private readonly taxPayerApplicationContext taxContext;

        public TaxCalculatorController(taxPayerApplicationContext tax)
        {
            taxContext = tax;
        }


        [HttpGet("{empid}")]
        public IActionResult Calculate( int empid)
        {
            Console.WriteLine("testing");
            Salary s = taxContext.Salary.FirstOrDefault(x => x.EmpId == empid);

            double salary = 0;
            if (s == null)
            {
                //return NotFound();
                double dummy = 25000 * 12;
                salary = dummy;
            }
            else
            {
                salary = (double)s.Salary1 * 12;
            }

            //using tax_info here

            List<TaxInfo> t1 = taxContext.TaxInfo.ToList();

            int taxId = 0;
            for(int i=0;i<t1.Count;i++)
            {
                if (salary < t1[i].SalaryFrom)
                {
                    if (i == 0)
                    {
                        taxId = 1;
                        break;
                    }
                    taxId = i;
                    Console.WriteLine(taxId);
                    break;
                }
            }
            if (taxId == 0)
            {
                taxId = t1[(t1.Count)-1].Id;
                Console.WriteLine(taxId);
            }


            //calculating tax and outputting it
            TaxInfo item = t1.Find(x => x.Id == taxId);

            double taxAmount = salary * item.TaxRate;



            //employee data
            Employee emp = taxContext.Employee.Find(empid);


            //check if tax is paid
            List<Payment> p1 = taxContext.Payment.ToList();
            bool paid = false;
            int paymentId = 0;
            foreach(Payment p in p1)
            {
                if(p.EmpId == empid)
                {
                    paid = true;
                    paymentId = p.Id;
                }
            }


            var taxData = new
            {
                amount = taxAmount,
                name = emp.FirstName + " " + emp.LastName,
                email = emp.Email,
                paid,
                paymentId


            };
            return StatusCode(200,taxData);



        }


        [HttpPost("payment")]
        public IActionResult Payment([FromQuery]int empId, [FromQuery]string Amount)
        {
            Console.WriteLine("New test");
            decimal Amount1 = decimal.Parse(Amount);
            //decimal Amount1 = 1000;

            List<Employee> l1 = taxContext.Employee.ToList();
            List<Payment> l2 = taxContext.Payment.ToList();

            bool flag = false;
            foreach (Employee e in l1)
            {
                if (e.Id == empId)
                {
                    flag = true;
                    break;
                }
            }

            //if employee is available , do this process bruuh
            bool flag2 = false;
            if (flag)
            {
                foreach(Payment p  in l2)
                {
                    if(p.EmpId == empId)
                    {
                        flag2 = true;
                    }
                }
            }
            else
            {
                return StatusCode(404, "Employee not available");
            }


            //if flag2==true then that means tax is already paid
            if (!flag2)
            {
                Payment p1 = new Payment
                {
                    EmpId = empId,
                    PaymentTime = DateTime.Now,
                    Amount = Amount1
                };

                taxContext.Payment.Add(p1);
                taxContext.SaveChanges();

                //get the transaction id
                List<Payment> p2 = taxContext.Payment.ToList();
                int paymentId = 0;
                foreach (Payment k in p2)
                {
                    if(k.EmpId == empId)
                    {
                        paymentId = k.Id; 
                        break;
                    }
                }

                return StatusCode(200,paymentId);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("getReceipt/{id}")]
        public IActionResult GetReceipt(int id)
        {
            Console.WriteLine("GetReceipt"+id);
            Payment payment = taxContext.Payment.Find(id);
            if (payment == null)
            {
                return StatusCode(404, "Payment not available");

            }
            else
            {
                return StatusCode(200, payment);
            }

        }

        [HttpGet("getAllPayments")]
        public IActionResult GetAllPayments() 
        {
            List<Payment> l1 = taxContext.Payment.ToList();
            return StatusCode(200, l1);

        }


        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail([FromForm] string to, [FromForm] IFormFile attachment, [FromForm]string empName)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("WeHelpTax", "laxminarayanakoyyana1@gmail.com")); // Replace with your Gmail address
                message.To.Add(new MailboxAddress(empName, to));
                message.Subject = "Tax Payment Successfull !";

                var body = new TextPart("plain")
                {
                    Text = "Please find the attachment,The below is the bill you requested on WeHelpTax(WHT) portal. Have a good day ! 😊"
                };

                var multipart = new Multipart("mixed");
                multipart.Add(body);

                if (attachment != null && attachment.Length > 0)
                {
                    var mimeAttachment = new MimePart(attachment.ContentType)
                    {
                        Content = new MimeContent(attachment.OpenReadStream(), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = attachment.FileName
                    };

                    multipart.Add(mimeAttachment);
                }
                else
                {
                    return BadRequest("Attachment not found");
                }

                message.Body = multipart;

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true); // Use Gmail's SMTP server and port 465
                    await client.AuthenticateAsync("laxminarayanakoyyana1@gmail.com", "zebksnwaxdcbnpif"); // Replace with your Gmail address and password

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                Console.WriteLine("Email sent");
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                }
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("getTerms")]
        public IActionResult GetTerms()
        {
            List<Terms> t1 = taxContext.Terms.ToList();
            return StatusCode(200, t1);
        }
        [HttpGet("termId/{id}")]
        public IActionResult GetTermsById(int id)
        {
            List<Terms> t1 = taxContext.Terms.ToList();
            foreach (Terms t in t1)
            {
                if(t.EmpId == id)
                {
                    return StatusCode(200, t);
                }
            }
            return StatusCode(200, "not available");
        }

        [HttpGet("termSalary/{id}")]
        public IActionResult GetSalaryByTerm(int id)
        {
            List<TermSalary> t1 = taxContext.TermSalary.ToList();
            foreach (TermSalary t in t1)
            {
                if (t.EmpId == id)
                {
                    return StatusCode(200, t);
                }
            }
            return StatusCode(200, "not available");
        }

    }



}
