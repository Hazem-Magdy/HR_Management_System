using AutoMapper;
using HR_Management_System.DTO.CustomResult;
using HR_Management_System.DTO.Employee;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HR_Management_System.Controllers
{
    [AdminAccountantHREmployee]
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IAttendanceService _attendanceService;
        private readonly IProjectService _projectService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UploadImage _uploadImage;


        public EmployeeController(
            IEmployeeService employeeService,
            UserManager<User> userManager,
            IDepartmentService departmentService,
            IAttendanceService attendanceService,
            IProjectService projectService,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            UploadImage uploadImage
            )
        {
            _employeeService = employeeService;
            _userManager = userManager;
            _departmentService = departmentService;
            _projectService = projectService;
            _attendanceService = attendanceService;
            _roleManager = roleManager;
            _mapper = mapper;
            _uploadImage = uploadImage;
        }

        //create Employee & user 
        [HttpPost]
        [AdminHROnly]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO employeeDTO)
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
                    Employee employee = _mapper.Map<CreateEmployeeDTO, Employee>(employeeDTO);
                    
                    // employee.ProfileUrl = employeeDTO.EmployeeProfileUrl;
                    if (employeeDTO.EmployeeProfileUrl!=null)
                    {
                        //List<IFormFile> images = new List<IFormFile>();
                        //images.Add(employeeDTO.EmployeeProfileUrl);
                        //await _uploadImage.UploadToCloud(images);
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
                        if (employeeDTO.EmployeePosition.ToString() == "Admin")
                        {
                            if (await _roleManager.RoleExistsAsync("Admin"))
                            {
                                await _userManager.AddToRoleAsync(user, "Admin");
                            }
                            else
                            {

                                IdentityResult roleUser = await _roleManager.CreateAsync(new IdentityRole("Admin"));

                                await _userManager.AddToRoleAsync(user, "Admin");
                            }
                        }
                        else if (employeeDTO.EmployeePosition.ToString() == "HR")
                        {
                            if (await _roleManager.RoleExistsAsync("HR"))
                            {
                                await _userManager.AddToRoleAsync(user, "HR");
                            }
                            else
                            {

                                IdentityResult roleUser = await _roleManager.CreateAsync(new IdentityRole("HR"));

                                await _userManager.AddToRoleAsync(user, "HR");
                            }
                        }
                        else if (employeeDTO.EmployeePosition.ToString() == "Employee")
                        {
                            if (await _roleManager.RoleExistsAsync("Employee"))
                            {
                                await _userManager.AddToRoleAsync(user, "Employee");
                            }
                            else
                            {

                                IdentityResult roleUser = await _roleManager.CreateAsync(new IdentityRole("Employee"));

                                await _userManager.AddToRoleAsync(user, "Employee");
                            }
                        }
                        else
                        {
                            if (await _roleManager.RoleExistsAsync("Accountant"))
                            {
                                await _userManager.AddToRoleAsync(user, "Accountant");
                            }
                            else
                            {

                                IdentityResult roleUser = await _roleManager.CreateAsync(new IdentityRole("Accountant"));

                                await _userManager.AddToRoleAsync(user, "Accountant");
                            }
                        }
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
                        return Ok("User created successfully.");
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
        [AdminHREmployee]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                GetEmployeeByIdDTO employeeDTO = _mapper.Map<Employee, GetEmployeeByIdDTO>(employee);
                
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
        /*
         * Backend
            // Get the JWT token from the request headers
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Create a new instance of JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            // Parse the JWT token
            var decodedToken = handler.ReadJwtToken(token);

            // Get the ID claim from the token
            var idClaim = decodedToken.Claims.FirstOrDefault(c => c.Type == "id");

            // Check if the employee is authorized to access the resource
            if (idClaim.Value != id.ToString())
            {
                // Employee is not authorized to access the resource
                return Unauthorized();
            }
         * frontend 
         // Retrieve the JWT token from session storage
            const token = sessionStorage.getItem('jwtToken');

            // Send a request to the backend with the token in the headers
            const headers = new HttpHeaders({
              'Authorization': `Bearer ${token}`
            });

            this.http.get<Employee>(`/api/employee/${id}`, { headers }).subscribe(
              employee => {
                // ...
              },
              error => {
                // ...
              }
            );
         */
        
        
        [HttpPut("{id}")]
        [AdminHROnly]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDTO employeeDTO)
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
                    Department department = await _departmentService.GetByIdAsync(employee.DepartmentId.Value);
                    Department departmentDTO = await _departmentService.GetByIdAsync(employeeDTO.DepartmentId.Value);
                    department.NoEmployees--;
                    departmentDTO.NoEmployees++;
                    await _departmentService.UpdateAsync(id, department);
                    await _departmentService.UpdateAsync(id, departmentDTO);
                }
                // Update the employee properties
                //employee = _mapper.Map<UpdateEmployeeDTO, Employee>(employeeDTO);
                employee.FirstName = employeeDTO.EmployeeFirstName;
                employee.LastName = employeeDTO.EmployeeLastName;
                employee.SalaryPerHour = employeeDTO.EmployeeSalaryPerHour;
                employee.WorkingDaysPerWeek = employeeDTO.EmployeeWorkingDaysPerWeek;
                employee.RegularHoursPerDay = employeeDTO.EmployeeRegularHoursPerDay;
                employee.OvertimeRate = employeeDTO.EmployeeOvertimeRate;
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
        [AdminHROnly]
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
        [AdminHREmployee]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                List<GetAllEmployeesDTO> employeeDTOs = new List<GetAllEmployeesDTO>();

                foreach (var employee in employees)
                {
                    GetAllEmployeesDTO employeeDto = _mapper.Map<Employee, GetAllEmployeesDTO>(employee);

                    employeeDTOs.Add(employeeDto);
                }
                return Ok(employeeDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("GetEmployeesHoursAndTotoalCostInAllProjects")]
        [AdminAccountantOnly]
        public async Task<IActionResult> GetEmployeesHoursAndTotoalCostInAllProjects()
        {
            var employees = await _employeeService.GetAllAsync(p => p.Attendances);

            var employeeHours = employees.Select(employee => new
            {
                EmployeeId = employee.Id,
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                TotalHoursSpent = employee.Attendances.Sum(a => a.HoursSpent),
                TotalCost = employee.CalculateSalaryPerProject(employee.Attendances.Sum(a => a.HoursSpent))
            });

            return Ok(employeeHours);
        }

        
        [HttpGet("GetEmployeesCostsInProject/{projectId}")]
        [AdminAccountantOnly]
        public async Task<IActionResult> GetEmployeesCostsInProject(int projectId)
        {
            var project = await _projectService.GetProjectByIdCustom2Async(projectId);

            if (project == null)
            {
                return NotFound("Project not found.");
            }


            var employeeCosts = project.Employees.Select(employee =>
            {
                decimal totalHoursSpent = project.Attendances.Where(a => a.EmployeeId == employee.EmployeeId).Sum(a => a.HoursSpent);
                decimal totalCost = employee.Employee.CalculateSalaryPerProject(totalHoursSpent);
                return new
                {
                    EmployeeId = employee.Employee.Id,
                    EmployeeName = $"{employee.Employee.FirstName} {employee.Employee.LastName}",
                    TotalCost = totalCost
                };
            });

            return Ok(employeeCosts);
        }

        
        [HttpGet("GetEmployeeSalaryAndOverTime/{employeeId}/StartDate/{startDate}/EndDate/{endDate}")]
        [AdminAccountantHR]
        public async Task<IActionResult> GetEmployeeSalaryAndOverTime(int employeeId, DateTime startDate, DateTime endDate)
        {

            decimal overTime = 0;

            int numberOfDays = await _attendanceService.getNoDaysWorkingSpentAsync(employeeId, startDate, endDate);

            Employee employee = await _employeeService.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound("Employee doesn't Exist.");
            }

            decimal regularHours = employee.GetRegularHours(numberOfDays);

            decimal totalHours = await _attendanceService.getTotalHoursSpentAsync(employeeId, startDate, endDate);

            if (totalHours > regularHours)
            {
                overTime = totalHours - regularHours;
                //employee.OverTime = overTime;
                GetEmployeeSalaryAndOverTimeDTO dto = new GetEmployeeSalaryAndOverTimeDTO()
                {
                    EmployeeSalary = employee.CalculateSalaryPerPeriod(overTime, regularHours),
                    EmployeeOverTime = overTime
                };
                return Ok(dto);
            }
            else
            {
                //employee.OverTime = overTime = 0;
                GetEmployeeSalaryAndOverTimeDTO dto = new GetEmployeeSalaryAndOverTimeDTO()
                {
                    EmployeeSalary = employee.CalculateSalaryPerPeriod(overTime, regularHours),
                    EmployeeOverTime = overTime
                };
                return Ok(dto);
            }
        }


    }
}
