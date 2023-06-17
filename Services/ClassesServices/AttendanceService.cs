using HR_Management_System.Data;
using HR_Management_System.Data.Base;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.EntityFrameworkCore;

namespace HR_Management_System.Services.ClassesServices
{
    public class AttendanceService : EntityBaseRepository<Models.Attendance>, IAttendanceService
    {
        private readonly AppDbContext db;

        public AttendanceService(AppDbContext _db) : base(_db)
        {
            db = _db;
        }

        public async Task<int> getNoDaysWorkingSpentAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            int noDays = await db.Attendances
                .Where(a => a.EmployeeId == employeeId && (a.Date >= startDate && a.Date <= endDate))
                .CountAsync();

            return noDays;
        }

        public async Task<decimal> getTotalHoursSpentAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            decimal totalHoursSpent = await db.Attendances
                .Where(a => a.EmployeeId == employeeId && (a.Date >= startDate && a.Date <= endDate))
                .SumAsync(a => a.HoursSpent);
            return totalHoursSpent;
        }
    }
}
