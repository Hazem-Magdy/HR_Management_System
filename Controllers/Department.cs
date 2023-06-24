using HR_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using HR_Management_System.DTO.Department;
using HR_Management_System.DTO.Employee;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.AspNetCore.Authorization;

namespace HR_Management_System.Controllers
{
    [Authorize(Roles = "Admin,HR,Accountant")]
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        [HttpPost("{IfManagerExistInDepartmentMove}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDepartment(DepartmentDTO departmentDTO, int IfManagerExistInDepartmentMove = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (IfManagerExistInDepartmentMove == 1)

                {
                    if (departmentDTO.ManagerId != null)
                    {
                        Employee employee = await _employeeService.GetByIdAsync(departmentDTO.ManagerId.Value);
                        Department department = await _departmentService.GetByIdAsync(employee.DepartmentId.Value);
                        department.EmployeeId = null;
                        employee.DepartmentId = departmentDTO.ManagerId;
                        await _departmentService.UpdateAsync(department.Id, department);
                        await _employeeService.UpdateAsync(employee.Id, employee);
                    }
                    return await Create(departmentDTO);
                }

                else
                {
                    if (departmentDTO.ManagerId != null)
                    {
                        var Employee = await _employeeService.GetByIdAsync(departmentDTO.ManagerId.Value);
                        if (Employee.DepartmentId != null)
                        {
                            return StatusCode(500, "The Manger Exist in another department Enter 1 to complete moving to another Department");
                        }
                        else
                        {
                            return await Create(departmentDTO);
                        }
                    }
                    departmentDTO.ManagerId = null;
                    await Create(departmentDTO);
                    return Ok("department added successfully.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // function to confirm and create Department 
        private async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            Boolean flag = false;
            // Map the DTO to the Department entity
            var department = new Department
            {
                Name = departmentDTO.DepartmentName,
                EmployeeId = departmentDTO.ManagerId
            };

            // Set the employee IDs for the department
            if (departmentDTO.EmployessIds.Count > 0)
            {
                foreach (int employeeId in departmentDTO.EmployessIds)
                {
                    var employee = await _employeeService.GetByIdAsync(employeeId);

                    if (employee != null)
                    {
                        department.Employees.Add(employee);
                        if (departmentDTO.ManagerId != null)
                        {
                            if (employee.Id != departmentDTO.ManagerId)
                            {
                                flag = true;
                            }
                        }
                    }
                }
                if (flag)
                {
                    department.NoEmployees = department.Employees.Count + 1;

                }
                else
                {
                    department.NoEmployees = department.Employees.Count;
                }
            }

            // Save the department to the database
            await _departmentService.AddAsync(department);

            return Ok("Department created successfully.");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR,Accountant")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllAsync(d => d.Employees);

                List<GetDepartmentsWithMangersNamesDTO> dTOs = new List<GetDepartmentsWithMangersNamesDTO>();

                foreach (var department in departments.ToList())
                {
                    GetDepartmentsWithMangersNamesDTO dTO = new GetDepartmentsWithMangersNamesDTO()
                    {
                        DepartmentId = department.Id,
                        DepartmentName = department.Name,
                        NOEmployees = department.NoEmployees.Value
                    };
                    if (department.EmployeeId != null)
                    {
                        dTO.MangerName = string.Concat(department.Employee.FirstName, " ", department.Employee.LastName);
                    }
                    else
                    {
                        dTO.MangerName = "Department has no manager yet.";
                    }

                    dTOs.Add(dTO);
                }
                return Ok(dTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,HR,Accountant")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetByIdAsync(id, d => d.Employees);
                if (department == null)
                {
                    return NotFound();
                }

                List<EmployeeDeptDetailsDTO> dTos = new List<EmployeeDeptDetailsDTO>();

                foreach (var employee in department.Employees)
                {

                    EmployeeDeptDetailsDTO employeeDTO = new EmployeeDeptDetailsDTO()
                    {
                        EmployeeId = employee.Id,
                        EmployeeFirstName = employee.FirstName,
                        EmployeeLastName = employee.LastName,
                        EmployeePosition = employee.Position.ToString(),
                    };
                    dTos.Add(employeeDTO);
                }

                var departmentDTO = new GetDepartmentWithEmployessDTO()
                {
                    Id = department.Id,
                    DepartmentName = department.Name,
                    ManagerName = department.Employee != null ? string.Concat(department.Employee.FirstName, " ", department.Employee.LastName) : string.Empty,
                    NoEmployees = department.NoEmployees.Value,
                    Employees = dTos
                };


                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var department = await _departmentService.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound();
                }

                // Update the department properties
                department.Name = departmentDTO.DepartmentName;
                department.EmployeeId = departmentDTO.ManagerId;

                if (departmentDTO.EmployessIds.Count != department.NoEmployees)
                {
                    department.NoEmployees = departmentDTO.EmployessIds.Count;
                }

                // Update the department employees
                if (departmentDTO.EmployessIds != null && departmentDTO.EmployessIds.Count > 0)
                {
                    department.Employees = new List<Employee>();

                    foreach (int employeeId in departmentDTO.EmployessIds)
                    {
                        var employee = await _employeeService.GetByIdAsync(employeeId);

                        if (employee != null)
                        {
                            department.Employees.Add(employee);
                        }
                    }
                }

                await _departmentService.UpdateAsync(id, department);
                return Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("delete/{deletedId}/target/{targetDepartmentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(int deletedId, int targetDepartmentId, [FromBody] List<int>? selectedEmployeeIds = null)
        {
            try
            {
                var department = await _departmentService.GetByIdAsync(deletedId, e => e.Employees);

                if (department == null)
                {
                    return NotFound();
                }

                var targetDepartment = await _departmentService.GetByIdAsync(targetDepartmentId, e => e.Employees);

                if (targetDepartment == null)
                {
                    return NotFound();
                }

                foreach (var employee in department.Employees)
                {
                    if (selectedEmployeeIds != null && selectedEmployeeIds.Contains(employee.Id))
                    {
                        employee.DepartmentId = targetDepartment.Id;
                        department.Employees.Add(employee);
                    }
                    else
                    {
                        employee.DepartmentId = null;
                        if (department.EmployeeId == employee.Id)
                        {
                            department.EmployeeId = null;
                        }
                        await _employeeService.DeleteAsync(employee.Id);
                    }
                }

                targetDepartment.NoEmployees = targetDepartment.Employees.Count;
                await _departmentService.DeleteAsync(deletedId);
                return Ok($"Department deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
