﻿using HR_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using HR_Management_System.DTO.Department;
using HR_Management_System.DTO.Employee;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Threading.Tasks;
using System;

namespace HR_Management_System.Controllers
{
    [AdminHROnly]
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService, IMapper mapper)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost("{IfManagerExistInDepartmentMove}")]
        [AdminOnly]
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
                        if (employee.DepartmentId != null)
                        {
                            Department department = await _departmentService.GetByIdAsync(employee.DepartmentId.Value);
                            department.EmployeeId = null;
                            await _departmentService.UpdateAsync(department.Id, department);
                        }
                        //employee.DepartmentId = departmentDTO.ManagerId;
                        //await _employeeService.UpdateAsync(employee.Id, employee);
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
            Department department = _mapper.Map<DepartmentDTO, Department>(departmentDTO);
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
                if (flag && department.EmployeeId!=null)
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
            if (departmentDTO.ManagerId != null)
            {
                Employee employee = await _employeeService.GetByIdAsync(departmentDTO.ManagerId.Value);
                employee.DepartmentId = department.Id;
                await _employeeService.UpdateAsync(employee.Id, employee);
            }
            return Ok("Department created successfully.");
        }

        [HttpGet]
        [AdminHROnly]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllAsync(d => d.Employees, d=>d.Employee);

                List<GetDepartmentsWithMangersNamesDTO> dTOs = new List<GetDepartmentsWithMangersNamesDTO>();

                foreach (var department in departments.ToList())
                {
                    GetDepartmentsWithMangersNamesDTO dTO = _mapper.Map<Department, GetDepartmentsWithMangersNamesDTO>(department);
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
        [AdminHROnly]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetByIdAsync(id, d => d.Employees, d=>d.Employee);
                if (department == null)
                {
                    return NotFound("Department no longer exist.");
                }

                List<EmployeeDeptDetailsDTO> dTos = new List<EmployeeDeptDetailsDTO>();

                foreach (var employee in department.Employees)
                {
                    EmployeeDeptDetailsDTO employeeDTO = _mapper.Map<Employee, EmployeeDeptDetailsDTO>(employee);
                    dTos.Add(employeeDTO);
                }

                GetDepartmentWithEmployessDTO departmentDTO = _mapper.Map<Department, GetDepartmentWithEmployessDTO>(department);
                departmentDTO.Employees = dTos;
                return Ok(departmentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [AdminOnly]
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
                    return NotFound("Department no longer exist.");
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

        [HttpDelete("delete/{deletedId}")]
        [AdminOnly]
        public async Task<IActionResult> DeleteDepartment(int deletedId, [FromBody] DeleteDepartmentRequestDTO requestDTO)
        {
            try
            {
                var department = await _departmentService.GetByIdAsync(deletedId, e => e.Employees);

                if (department == null)
                {
                    return NotFound("Department no longer exist.");
                }

                if (requestDTO.TargetDepartmentId != null)
                {
                    var targetDepartment = await _departmentService.GetByIdAsync(requestDTO.TargetDepartmentId.Value, e => e.Employees);

                    if (targetDepartment == null)
                    {
                        return NotFound("Department no longer exist.");
                    }

                    foreach (var employee in department.Employees.ToList())
                    {
                        if (requestDTO.SelectedEmployeeIds != null && requestDTO.SelectedEmployeeIds.Contains(employee.Id))
                        {
                            employee.DepartmentId = targetDepartment.Id;
                            targetDepartment.Employees.Add(employee);
                        }
                        else
                        {
                            await _employeeService.DeleteAsync(employee.Id);
                        }
                    }
                    targetDepartment.NoEmployees = targetDepartment.Employees.Count;
                }
                else
                {
                    foreach (var employee in department.Employees.ToList())
                    {
                        await _employeeService.DeleteAsync(employee.Id);
                    }
                }

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
