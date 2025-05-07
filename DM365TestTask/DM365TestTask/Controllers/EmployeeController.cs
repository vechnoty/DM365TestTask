using DM365TestTask.Models;
using DM365TestTask.Repository;
using Microsoft.AspNetCore.Mvc;
using DM365TestTask.DTO;
using Microsoft.EntityFrameworkCore;


namespace DM365TestTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("employees")]
        public async Task<IActionResult> GetEmpoyees()
        {
            try
            {
                var employees = await _appDbContext.Employees.ToListAsync();
                return Ok(employees);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost("employees")]
        public async Task<IActionResult> AddEmployee(EmployeeDto employee)
        {
            try
            {
                var newEmployee = new Employee(employee.Name);
                await _appDbContext.Employees.AddAsync(newEmployee);
                await _appDbContext.SaveChangesAsync();
                return Ok(newEmployee);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
    }
}
