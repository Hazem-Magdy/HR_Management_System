using HR_Management_System.DTO.Attendance;
using HR_Management_System.DTO.CustomResult;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
                // cheack if employee exist 
                Employee existingEmployee = await _employeeService.GetByIdAsync(attendanceDTO.EmployeeId);

                if (existingEmployee == null)
                {
                    result.IsPass = false;
                    result.Message = "employee not exist on the system.";
                }
                else
                {
                    // employee is existing in the system
                    // attend the employee to the system
                    Models.Attendance attendance = new Models.Attendance()
                    {
                        EmployeeId = attendanceDTO.EmployeeId,
                        ProjectId = attendanceDTO.ProjectId,
                        ProjectPhaseId = attendanceDTO.ProjectPhaseId,
                        ProjectTaskId = attendanceDTO.ProjectTaskId,
                        Date = attendanceDTO.Date,
                        HoursSpent = attendanceDTO.HoursSpent,
                        Description = attendanceDTO.Description,
                    };

                    Models.Attendance addedAttendance = await _attendanceService.AddAsync(attendance);

                    if (addedAttendance != null)
                    {
                        // return data 
                        result.IsPass = true;
                        result.Message = "user attend successfully to the system.";
                    }
                    else
                    {
                        result.IsPass = false;
                        result.Message = "attend didnt save correctly in the system.";
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
