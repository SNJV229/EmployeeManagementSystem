using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Runtime.Intrinsics.X86;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _employeecontext;

        public EmployeeController(EmployeeContext employeecontext)
        {
            _employeecontext = employeecontext;

            if (_employeecontext.Employees.Count() == 0)
            {
                _ = _employeecontext.Employees.Add(new Employee
                {
                    EmployeeId = 001,
                    EmployeeName = "Peter Wilson",
                    EmployeeEmail = "pwil@abc.com",
                    Address = "dhaka"

                });

                _employeecontext.Employees.Add(new Employee
                {
                    EmployeeId = 002,
                    EmployeeName = "Peter maq",
                    EmployeeEmail = "pmaq@abc.com",
                    Address = "Bangalore"

                });
                _employeecontext.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable GetAll()
        {
            return _employeecontext.Employees.ToList();
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult GetById(int id)
        {
            var item = _employeecontext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if(item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        //[FromBody] attribute says use details of the new Employee from the HTTP request body
        [HttpPost]
        public IActionResult Create([FromBody] Employee item)
        {
            if(item == null)
            {
                return BadRequest();
            }
            _employeecontext.Employees.Add(item);
            _employeecontext.SaveChanges();

            //CreatedAtRoute returns URL to the newly created resource when we invoke the POST method.
            return CreatedAtRoute("GetById", new { id = item.EmployeeId }, item);
        }

        [HttpPut]
        public IActionResult Update(long id, [FromBody] Employee item)
        {
            if (item == null || item.EmployeeId != id)
            {
                return BadRequest();
            }

            var employee = _employeecontext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeId = item.EmployeeId;
            employee.EmployeeName = item.EmployeeName;
            employee.EmployeeEmail = item.EmployeeEmail;
            employee.Address = item.Address;

            _employeecontext.Employees.Update(employee);
            _employeecontext.SaveChanges();
            return new NoContentResult();  // return 204(NoContent)
         }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var employee = _employeecontext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if(employee == null)
            {
                return NotFound();
            }
            _employeecontext.Employees.Remove(employee);
            _employeecontext.SaveChanges();
            return new NoContentResult();
        }
    }
}
