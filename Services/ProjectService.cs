using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_System.Services
{
    public class ProjectService : EntityBaseRepository<Project>, IProjectService
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext _db) : base(_db) {
            _context = _db;
        }
        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
