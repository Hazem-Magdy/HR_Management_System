using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Map the DTO to the Employee entity
                var employee = new Employee
                {
                    FirstName = employeeDTO.FirstName,
                    LastName = employeeDTO.LastName,
                    SalaryPerHour = employeeDTO.SalaryPerHour,
                    OverTime = employeeDTO.OverTime,
                    Salary = employeeDTO.Salary,
                    Phone = employeeDTO.Phone,
                    Email = employeeDTO.Email,
                    Password = employeeDTO.Password,
                    Position = employeeDTO.Position,
                    HiringDate = employeeDTO.HiringDate,
                    Status = employeeDTO.Status,
                    DepartmentId = employeeDTO.DepartmentId,
                };

                if(employeeDTO.ProfileUrl != null)
                {
                    employee.ProfileUrl = employeeDTO.ProfileUrl;   
                }

                
                // Save the employee to the database
                await _employeeService.AddAsync(employee);
                return Ok("Employee created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIDAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = await _employeeService.GetByIDAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                // Update the employee properties
                employee.FirstName = employeeDTO.FirstName;
                employee.LastName = employeeDTO.LastName;
                employee.SalaryPerHour = employeeDTO.SalaryPerHour;
                employee.OverTime = employeeDTO.OverTime;
                employee.Salary = employeeDTO.Salary;
                employee.ProfileUrl = employeeDTO.ProfileUrl;
                employee.Phone = employeeDTO.Phone;
                employee.Email = employeeDTO.Email;
                employee.Password = employeeDTO.Password;
                employee.Position = employeeDTO.Position;
                employee.HiringDate = employeeDTO.HiringDate;
                employee.Status = employeeDTO.Status;
                employee.DepartmentId = employeeDTO.DepartmentId;

                await _employeeService.UpdateAsync(id, employee);
                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIDAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                await _employeeService.DeleteAsync(id);
                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
