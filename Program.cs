using HR_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using HR_Management_System.Services.ClassesServices;
using HR_Management_System.Services.InterfacesServices;
using Hangfire;
using HR_Management_System.Data.Helpers.Mappers;
using Microsoft.Data.SqlClient;

namespace HR_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(
                op => op.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
            builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddScoped<IEmployeeService,EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IProjectPhaseService, ProjectPhaseService>();
            builder.Services.AddScoped<IProjectTasksService, ProjectTasksService>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<IEmployeeProjectsService, EmployeeProjectsService>();

            builder.Services.AddAutoMapper(
                            typeof(AttendanceMappingProfile), 
                            typeof(DepartmentMappingProfile), 
                            typeof(EmployeeMappingProfile), 
                            typeof(EmployeeProjectMappingProfile), 
                            typeof(ProjectMappingProfile), 
                            typeof(ProjectPhaseMappingProfile), 
                            typeof(ProjectTaskMappingProfile)
           );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            };

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();


            app.Run();
        }
    }
}