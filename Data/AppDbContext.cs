using HR_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_System.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public virtual DbSet<ProjectPhase> ProjectPhases { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Employee)
                .WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Project>()
                .HasMany(p => p.projectTasks)
                .WithOne(pt => pt.Project)
                .HasForeignKey(pt => pt.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.projectPhases)
                .WithOne(pp => pp.Project)
                .HasForeignKey(pp => pp.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Attendances)
                .WithOne(a => a.Project)
                .HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Attendances)
                .WithOne(a => a.Employee)
                .HasForeignKey(a => a.EmployeeId);

            modelBuilder.Entity<ProjectPhase>()
                .HasMany(pp => pp.Attendances)
                .WithOne(a => a.ProjectPhase)
                .HasForeignKey(a => a.ProjectPhaseId);

            modelBuilder.Entity<ProjectTask>()
                .HasMany(pt => pt.Attendances)
                .WithOne(a => a.ProjectTask)
                .HasForeignKey(a => a.ProjectTaskId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.employeeProjects)
                .WithOne(ep => ep.Employee)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.employeeProjects)
                .WithOne(ep => ep.Project)
                .HasForeignKey(ep => ep.ProjectId);

            modelBuilder.Entity<ProjectPhase>()
                .HasMany(pp => pp.Attendances)
                .WithOne(ep => ep.ProjectPhase)
                .HasForeignKey(ep => ep.ProjectPhaseId);
        }
    }
}
