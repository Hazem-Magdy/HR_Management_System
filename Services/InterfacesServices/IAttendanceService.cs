using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IAttendanceService : IEntityBaseRepository<Attendance>
    {
        Task<decimal> getTotalHoursSpentAsync(int employeeId ,DateTime startDate , DateTime endDate );

        Task<int> getNoDaysWorkingSpentAsync(int employeeId, DateTime startDate, DateTime endDate);
    }

}
