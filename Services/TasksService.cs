using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using System;

namespace HR_Management_System.Services
{
    public class TasksService : EntityBaseRepository<Models.ProjectTask>, ITasksService
    {
        public TasksService(AppDbContext _db) : base(_db) { }
    }
}
