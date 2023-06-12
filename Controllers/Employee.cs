using HR_Management_System.DTO.CustomResult;
using HR_Management_System.DTO.Employee;
using HR_Management_System.Models;
using HR_Management_System.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly UserManager<User> _userManager;

        public EmployeeController(IEmployeeService employeeService, UserManager<User> userManager, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _userManager = userManager;
            _departmentService = departmentService;
        }

        //create Employee & user 
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(AddUpdateEmployeeDTO employeeDTO)
        {
            CustomResultDTO result = new CustomResultDTO();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Boolean flag = true;
            try
            {
                Employee employeeExist = await _employeeService.GetByEmailAsync(employeeDTO.Email);
                if (employeeExist == null)
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

                    if (employeeDTO.ProfileUrl != null)
                    {
                        employee.ProfileUrl = employeeDTO.ProfileUrl;
                    }
                    

                    // create user
                    User user = new User();

                    Random random = new Random();

                    int randomNumber = random.Next(1, 100000);

                    user.UserName = string.Concat(employeeDTO.FirstName, employeeDTO.LastName, randomNumber.ToString());

                    user.Email = employeeDTO.Email;
                    user.PhoneNumber = employeeDTO.Phone;


                    IdentityResult createResult = await _userManager.CreateAsync(user, employeeDTO.Password);

                    if (createResult.Succeeded)
                    {
                        result.IsPass = true;
                        result.Message = "account created successfully.";
                    }
                    else
                    {
                        flag = false;
                        result.IsPass = false;
                        result.Message = "account created failed.";
                        result.Data = ModelState;

                    }
                    if (flag)
                    {
                        // Save the employee to the database
                        await _employeeService.AddAsync(employee);
                        return Ok("Employee created successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "Can't Create Employee, User isn't valid");
                    }
                }
                else
                {
                    return StatusCode(409, "Email Already Exist, please use another Email");
                }
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
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                var employeeDTO = new GetEmployeeByIdDTO
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    SalaryPerHour = employee.SalaryPerHour,
                    Salary = employee.Salary,
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
        public async Task<IActionResult> UpdateEmployee(int id, AddUpdateEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                if (employee.DepartmentId != employeeDTO.DepartmentId)
                {
                    Department department= await _departmentService.GetByIdAsync(employee.DepartmentId.Value);
                    Department departmentDTO= await _departmentService.GetByIdAsync(employeeDTO.DepartmentId.Value);
                    department.NoEmployees--;
                    departmentDTO.NoEmployees++;
                    await _departmentService.UpdateAsync(id, department);
                    await _departmentService.UpdateAsync(id, departmentDTO);
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
                var employee = await _employeeService.GetByIdAsync(id);
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
                List<GetAllEmployeesDTO> employeeDTOs = new List<GetAllEmployeesDTO>();

                foreach (var employee in employees)
                {
                    var employeeDto = new GetAllEmployeesDTO
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
