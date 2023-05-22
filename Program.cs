﻿using HR_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using HR_Management_System.Services;

namespace HR_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ITIDbContext>(
                op => op.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
            builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ITIDbContext>();
            builder.Services.AddScoped<IEmployeeService,EmployeeService>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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


            app.MapControllers();


            app.Run();
        }
    }
}