using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IAttendanceService : IEntityBaseRepository<Attendance>
    {

        public Attendance IsAttendInSpecificDay(DateTime date, int employeeId);



    }

}
