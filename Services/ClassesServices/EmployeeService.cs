using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.EntityFrameworkCore;
using System;

namespace HR_Management_System.Services.ClassesServices
{
    public class EmployeeService : EntityBaseRepository<Employee>, IEmployeeService
    {
        private readonly AppDbContext db;

        public EmployeeService(AppDbContext _db) : base(_db)
        {
            db = _db;
        }


        public async Task<Employee> GetByEmailAsync(string email)
        {
            Employee existEmployee = await db.Employees.FirstOrDefaultAsync(m => m.Email == email);
            db.SaveChanges();
            return existEmployee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                return await db.Employees
                    .Include(e => e.Projects)
                        .ThenInclude(p => p.Project)
                            .ThenInclude(pp => pp.projectTasks)
                    .Include(e => e.Projects)
                        .ThenInclude(p => p.Project)
                            .ThenInclude(pp => pp.projectPhases)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                throw new Exception($"An error occurred while retrieving the employee: {ex.Message}");
            }
        }
    }
}
