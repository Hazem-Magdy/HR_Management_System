using HR_Management_System.Controllers;
using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Models;
using System;
using System.Linq.Expressions;

namespace HR_Management_System.Services
{
    public class AttendanceService : EntityBaseRepository<Models.Attendance>, IAttendanceService
    {
        private readonly ITIDbContext db;

        public AttendanceService(ITIDbContext _db) : base(_db)
        {
            db = _db;
        }

        public Models.Attendance IsAttendInSpecificDay(DateTime date, int employeeId)
        {
            Models.Attendance employeeAttended = db.Attendances.SingleOrDefault(a => a.EmployeeId == employeeId && a.Date == date);

            return employeeAttended;
        }

        
    }
}
