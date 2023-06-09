﻿using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IEmployeeService : IEntityBaseRepository<Employee>
    {

        Task<Employee> GetByEmailAsync(string email);
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
    }

}
