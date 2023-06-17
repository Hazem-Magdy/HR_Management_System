using AutoMapper;
using HR_Management_System.DTO.Attendance;
using HR_Management_System.Models;

namespace HR_Management_System.Data.Helpers.Mappers
{
    public class AttendanceMappingProfile : Profile
    {
        public AttendanceMappingProfile()
        {

            #region AttendanceDTO
            CreateMap<Attendance, AttendanceDTO>()
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                    .ForMember(dest => dest.ProjectPhaseId, opt => opt.MapFrom(src => src.ProjectPhaseId))
                    .ForMember(dest => dest.ProjectTaskId, opt => opt.MapFrom(src => src.ProjectTaskId))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.HoursSpent, opt => opt.MapFrom(src => src.HoursSpent));

            CreateMap<AttendanceDTO, Attendance>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.ProjectPhaseId, opt => opt.MapFrom(src => src.ProjectPhaseId))
                .ForMember(dest => dest.ProjectTaskId, opt => opt.MapFrom(src => src.ProjectTaskId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.HoursSpent, opt => opt.MapFrom(src => src.HoursSpent));
            #endregion

            // Attendance list DTO 

            #region GetAttendancesInProjectDTO
            CreateMap<Attendance, GetAttendancesInProjectDTO>()
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                    .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : ""))
                    .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                    .ForMember(dest => dest.PhaseName, opt => opt.MapFrom(src => src.ProjectPhase.Name))
                    .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.ProjectTask.Name))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.HoursSpent, opt => opt.MapFrom(src => src.HoursSpent));

            CreateMap<GetAttendancesInProjectDTO, Attendance>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Project.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.ProjectPhase.Name, opt => opt.MapFrom(src => src.PhaseName))
                .ForMember(dest => dest.ProjectTask.Name, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.HoursSpent, opt => opt.MapFrom(src => src.HoursSpent)); 
            #endregion

        }
    }
}
