using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIs.Common;
using WebAPIs.DTOs;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Employee>> Get()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
                return NotFound();
            return Ok(new APIResponse<Employee> { data = employee, isSuccess = true, message = "user added successfully" });


        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(new APIResponse<Employee> { data = employee, isSuccess = true, message = "user added successfully" });

        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee employeeData)
        {
            if (employeeData == null || employeeData.Id == 0)
                return BadRequest();

            var employee = await _context.Employees.FindAsync(employeeData.Id);
            if (employee == null)
                return NotFound();
            employee.FirstName = employeeData.FirstName;
            employee.LastName = employeeData.LastName;
            employee.Address = employeeData.Address;
            employee.Email = employeeData.Email;
            employee.Phone = employeeData.Phone;
            await _context.SaveChangesAsync();
            return Ok(new APIResponse<Employee> { data = employee , isSuccess = true, message="user addedd successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Employees.FindAsync(id);
            if (product == null) return NotFound();
            _context.Employees.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(new APIResponse<Employee> { isSuccess = true , message = "Employee deleted successfully" });

        }
    }
}
