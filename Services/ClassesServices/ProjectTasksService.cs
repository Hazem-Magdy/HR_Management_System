using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using System;

namespace HR_Management_System.Services.ClassesServices
{
    public class ProjectTasksService : EntityBaseRepository<ProjectTask>, IProjectTasksService
    {
        public ProjectTasksService(AppDbContext _db) : base(_db) { }
    }
}
