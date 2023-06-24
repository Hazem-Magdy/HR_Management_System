using HR_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using HR_Management_System.Services.ClassesServices;
using HR_Management_System.Services.InterfacesServices;
using Hangfire;
using HR_Management_System.Data.Helpers.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using HR_Management_System.Data.Helpers;

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
            
            //builder.Services.AddTransient<IAuthorizationHandler, ViewEmployeeHandler>();
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; //check if the request is https
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:validIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:secretKey"]))
                };
            });

                
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewEmployeePolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Employee");
                    policy.Requirements.Add(new ViewEmployeeRequirement());
                });
            });

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

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}