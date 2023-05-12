using HR_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;

namespace HR_Management_System.Data
{
    public class ITIDbContext: DbContext
    {
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectPhase> ProjectPhases { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public ITIDbContext(DbContextOptions<ITIDbContext> options) : base(options) { }
        
    }
}
