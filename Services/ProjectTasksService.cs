﻿using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using System;

namespace HR_Management_System.Services
{
    public class ProjectTasksService : EntityBaseRepository<ProjectTask>, IProjectTasksService
    {
        public ProjectTasksService(AppDbContext _db) : base(_db) { }
    }
}