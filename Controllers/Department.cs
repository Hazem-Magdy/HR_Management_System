using HR_Management_System.Models;
using HR_Management_System.DTO;
using HR_Management_System.Services;
using Microsoft.AspNetCore.Mvc;


namespace HR_Management_System.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Map the DTO to the Department entity
                var department = new Department
                {
                    Name = departmentDTO.Name,
                    EmployeeId = departmentDTO.ManagerId
                };

                // Set the employee IDs for the department
                if (departmentDTO.EmployessIds != null && departmentDTO.EmployessIds.Count > 0)
                {
                    department.Employees = new List<Employee>();

                    foreach (int employeeId in departmentDTO.EmployessIds)
                    {
                        var employee = await _employeeService.GetByIDAsync(employeeId);

                        if (employee != null)
                        {
                            department.Employees.Add(employee);
                        }
                    }
                }

                // Save the department to the database
                await _departmentService.AddAsync(department);

                return Ok("Department created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("/api/GetDepartmentsWithMangersNames")]
        public async Task<IActionResult> GetDepartmentsWithMangersNames()
        {
            try
            {
                var departments = await _departmentService.GetAllAsync(d=>d.Employees);

                GetDepartmentsWithMangersNamesDTO dTO = new GetDepartmentsWithMangersNamesDTO()
                {
                    DepartmenstNames = departments.Select(d=>d.Name).ToList(),
                    MangersNames = departments.Select(d => string.Concat(d.Employee.FirstName, " ", d.Employee.LastName) ).ToList(),
                };

                foreach(var dpt in departments)
                {
                    dTO.NOEmployeesInDepartment.Add(dpt.Employees.Count());
                }

                return Ok(dTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                var department = await _departmentService.GetByIdAsync(id,d=>d.Employees);
                if (department == null)
                {
                    return NotFound();
                }

                GetDepartmentsWithMangerNameDTO departmentDTO = new GetDepartmentsWithMangerNameDTO()
                {
                    DepartmentName = department.Name,
                    MangerName = string.Concat(department.Employee.FirstName, " ", department.Employee.LastName),
                    EmployeeUnderWork = department.Employees.Count
                };
                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var department = await _departmentService.GetByIDAsync(id);
                if (department == null)
                {
                    return NotFound();
                }

                // Update the department properties
                department.Name = departmentDTO.Name;
                department.EmployeeId = departmentDTO.ManagerId;

                // Update the department employees
                if (departmentDTO.EmployessIds != null && departmentDTO.EmployessIds.Count > 0)
                {
                    department.Employees = new List<Employee>();

                    foreach (int employeeId in departmentDTO.EmployessIds)
                    {
                        var employee = await _employeeService.GetByIDAsync(employeeId);

                        if (employee != null)
                        {
                            department.Employees.Add(employee);
                        }
                    }
                }

                await _departmentService.UpdateAsync(id,department);
                return Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department = await _departmentService.GetByIDAsync(id);
                if (department == null)
                {
                    return NotFound();
                }

                await _departmentService.DeleteAsync(id);
                return Ok("Department deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
