using HR_Management_System.Data.Enums;
using HR_Management_System.DTO;
using HR_Management_System.Models;
using HR_Management_System.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Attendance : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAttendanceService _attendanceService;
        private readonly IEmployeeService _employeeService;

        public Attendance(UserManager<User> userManager, IAttendanceService attendanceService, IEmployeeService employeeService)
        {

            _userManager = userManager;
            _attendanceService = attendanceService;
            _employeeService = employeeService;
        }

        [HttpPost("Attend")]
        public async Task<ActionResult<CustomResultDTO>> Attend(AttendanceDTO attendanceDTO)
        {
            // response layer
            CustomResultDTO result = new CustomResultDTO();
            try
            {
                // cheack if user exist 
                Models.User existUser = await _userManager.FindByNameAsync(attendanceDTO.UserName);
                if (existUser == null)
                {
                    result.IsPass = false;
                    result.Message = "user not exist on the system.";
                }
                else
                {
                    // get employee id
                    Models.Employee employee = await _employeeService.GetByEmailAsync(existUser.Email);
                    int employeeId = employee.Id;

                    // attend the employee to the system
                    TimeSpan onTime = new TimeSpan(8, 0, 0);
                    TimeSpan lateTime = new TimeSpan(8, 30, 0);
                    TimeSpan leaveTime = new TimeSpan(18, 0, 0);

                    TimeSpan attendTime = DateTime.Now.TimeOfDay;

                    AttendanceStatus attendStatus = AttendanceStatus.Absent;
                    // <=8
                    if (attendTime <= onTime)
                        attendStatus = AttendanceStatus.OnTime;
                    //>8 && <8.30 
                    else if (attendTime > onTime && attendTime <= lateTime)
                        attendStatus = AttendanceStatus.Late;

                    else if (attendTime > lateTime || attendTime > leaveTime)
                        attendStatus = AttendanceStatus.Absent;

                    Models.Attendance attendance = new Models.Attendance()
                    {
                        Date = DateTime.Today,
                        TimeIn = attendTime,
                        TimeOut = leaveTime,
                        AttendanceStatus = attendStatus,
                        EmployeeId = employeeId,

                    };
                    Models.Attendance addedAttendance = await _attendanceService.AddAsync(attendance);
                    if (addedAttendance != null)
                    {
                        // return data
                        result.IsPass = true;
                        result.Message = "user attend successfully to the system.";
                        result.Data = new
                        {
                            UserName = attendanceDTO.UserName,
                            Date = attendance.Date.ToString("yyyy-MM-dd"),
                            AttendaceStatues = attendStatus.ToString(),
                            TimeIn = attendance.TimeIn,
                            Timeout = attendance.TimeOut
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                result.IsPass = false;
                result.Message = $"An error occurred while processing the request :{ex.Message}.";
            }
            return result;
        }
    }
}
