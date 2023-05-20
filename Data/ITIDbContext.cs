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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeProject>()
            .HasKey(ep => new { ep.EmployeeId, ep.ProjectId, ep.ProjectPhaseId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.ProjectPhase)
                .WithMany(pp => pp.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectPhaseId);
        }

    }
}
