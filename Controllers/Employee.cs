using HR_Management_System.Models;
using HR_Management_System.Models.DTOs;
using HR_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Controllers
{
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
                var employeeDTO = new EmployeeDTO
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    SalaryPerHour = employee.SalaryPerHour,
                    Salary  =employee.Salary,
                    OverTime = employee.OverTime,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    Position = employee.Position,
                    Status = employee.Status,
                    DepartmentId = employee.DepartmentId
                };
                if (employee.ProfileUrl != null)
                {
                    employeeDTO.ProfileUrl = employee.ProfileUrl;
                }
                return Ok(employeeDTO);
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
                List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
                
                foreach (var employee in employees)
                {
                    var employeeDto = new EmployeeDTO
                    {
                        EmplyeeId = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        SalaryPerHour = employee.SalaryPerHour,
                        Salary = employee.Salary,
                        OverTime = employee.OverTime,
                        Phone = employee.Phone,
                        Email = employee.Email,
                        Position = employee.Position,
                        Status = employee.Status,
                        DepartmentId = employee.DepartmentId,
                        ProfileUrl = employee.ProfileUrl
                    };
                    employeeDTOs.Add(employeeDto);
                }
                return Ok(employeeDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
