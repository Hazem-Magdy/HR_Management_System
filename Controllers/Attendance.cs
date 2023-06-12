using HR_Management_System.DTO.Attendance;
using HR_Management_System.DTO.CustomResult;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
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

        [HttpPost]
        public async Task<ActionResult<CustomResultDTO>> Attend(AttendanceDTO attendanceDTO)
        {
            // response DTO
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
                    // user is existing in the system
                    // get employee id
                    Models.Employee employee = await _employeeService.GetByEmailAsync(existUser.Email);
                    int employeeId = employee.Id;

                    Models.Attendance attendToday =_attendanceService.IsAttendInSpecificDay(DateTime.Today, employeeId);

                    if (attendToday == null)
                    {
                        // attend the employee to the system

                        Models.Attendance attendance = new Models.Attendance()
                        {
                            
                            EmployeeId = employeeId,
                            ProjectId = attendanceDTO.ProjectId,
                            ProjectPhaseId= attendanceDTO.ProjectPhaseId,
                            ProjectTaskId= attendanceDTO.ProjectTaskId,
                            Date = DateTime.Today,
                            HoursSpent = attendanceDTO.HoursSpent,

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
                                Date = attendance.Date.ToString("yyyy-MM-dd")
                            };

                        }
                    }
                    else
                    {
                        result.IsPass = false;
                        result.Message = "user attend to the system before.";
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
