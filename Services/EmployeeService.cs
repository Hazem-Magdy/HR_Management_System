using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HR_Management_System.Services
{
    public class EmployeeService : EntityBaseRepository<Employee>, IEmployeeService
    {
        private readonly ITIDbContext db;

        public EmployeeService(ITIDbContext _db) : base(_db)
        {
            db = _db;
        }


        public async Task<Employee> GetByEmailAsync(string email)
        {
            Employee existEmployee = await db.Employees.FirstOrDefaultAsync(m=>m.Email == email);
            db.SaveChanges();
            return existEmployee;
        }
    }
}
