using HR_Management_System.DTO.CustomResult;
using HR_Management_System.DTO.Employee;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
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

        public EmployeeController(
            IEmployeeService employeeService, 
            UserManager<User> userManager, 
            IDepartmentService departmentService
            )
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
                Employee employeeExist = await _employeeService.GetByEmailAsync(employeeDTO.EmployeeEmail);
                if (employeeExist == null)
                {
                    // Map the DTO to the Employee entity
                    var employee = new Employee
                    {
                        FirstName = employeeDTO.EmployeeFirstName,
                        LastName = employeeDTO.EmployeeLastName,
                        SalaryPerHour = employeeDTO.EmployeeSalaryPerHour,
                        OverTime = employeeDTO.EmployeeOverTime,
                        Salary = employeeDTO.EmployeeSalary,
                        Phone = employeeDTO.EmployeePhone,
                        Email = employeeDTO.EmployeeEmail,
                        Password = employeeDTO.EmployeePassword,
                        Position = employeeDTO.EmployeePosition,
                        HiringDate = employeeDTO.EmployeeHiringDate,
                        Status = employeeDTO.EmployeeStatus,
                        DepartmentId = employeeDTO.DepartmentId,
                    };

                    if (employeeDTO.EmployeeProfileUrl != null)
                    {
                        employee.ProfileUrl = employeeDTO.EmployeeProfileUrl;
                    }
                    

                    // create user
                    User user = new User();

                    Random random = new Random();

                    int randomNumber = random.Next(1, 100000);

                    user.UserName = string.Concat(employeeDTO.EmployeeFirstName, employeeDTO.EmployeeLastName, randomNumber.ToString());

                    user.Email = employeeDTO.EmployeeEmail;
                    user.PhoneNumber = employeeDTO.EmployeePhone;


                    IdentityResult createResult = await _userManager.CreateAsync(user, employeeDTO.EmployeePassword);

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
        public async Task<IActionResult> GetEmployeeById(int id)
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
                    EmployeeFirstName = employee.FirstName,
                    EmployeeLastName = employee.LastName,
                    EmployeeSalaryPerHour = employee.SalaryPerHour,
                    EmployeeSalary = employee.Salary,
                    EmployeeOverTime = employee.OverTime,
                    EmployeePhone = employee.Phone,
                    EmployeeEmail = employee.Email,
                    EmployeePosition = employee.Position,
                    EmployeeStatus = employee.Status,
                    DepartmentId = employee.DepartmentId
                };
                if (employee.ProfileUrl != null)
                {
                    employeeDTO.EmployeeProfileUrl = employee.ProfileUrl;
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
                employee.FirstName = employeeDTO.EmployeeFirstName;
                employee.LastName = employeeDTO.EmployeeLastName;
                employee.SalaryPerHour = employeeDTO.EmployeeSalaryPerHour;
                employee.OverTime = employeeDTO.EmployeeOverTime;
                employee.Salary = employeeDTO.EmployeeSalary;
                employee.ProfileUrl = employeeDTO.EmployeeProfileUrl;
                employee.Phone = employeeDTO.EmployeePhone;
                employee.Email = employeeDTO.EmployeeEmail;
                employee.Position = employeeDTO.EmployeePosition;
                employee.HiringDate = employeeDTO.EmployeeHiringDate;
                employee.Status = employeeDTO.EmployeeStatus;
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
                        EmployeeFirstName = employee.FirstName,
                        EmployeeLastName = employee.LastName,
                        EmployeeSalaryPerHour = employee.SalaryPerHour,
                        EmployeeSalary = employee.Salary,
                        EmployeeOverTime = employee.OverTime,
                        EmployeePhone = employee.Phone,
                        EmployeeEmail = employee.Email,
                        EmployeePosition = employee.Position,
                        EmployeeStatus = employee.Status,
                        DepartmentId = employee.DepartmentId,
                        EmployeeProfileUrl = employee.ProfileUrl
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
