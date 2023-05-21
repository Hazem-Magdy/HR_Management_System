﻿using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using System;

namespace HR_Management_System.Services
{
    public class ProjectPhaseService : EntityBaseRepository<ProjectPhase>, IProjectPhaseService
    {
        public ProjectPhaseService(ITIDbContext _db) : base(_db) { }
    }
}