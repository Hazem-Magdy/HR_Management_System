using AutoMapper;
using HR_Management_System.DTO.Project;
using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.Models;

namespace HR_Management_System.Data.Helpers.Mappers
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            #region CreateProjectDTO
            CreateMap<Project, CreateProjectDTO>()
                    .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ProjectTotalBudget, opt => opt.MapFrom(src => src.TotalBudget))
                    .ForMember(dest => dest.ProjectHours, opt => opt.MapFrom(src => src.HoursBudget))
                    .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                    .ForMember(dest => dest.ProjectLocation, opt => opt.MapFrom(src => src.Location))
                    .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.EndDate))
                    .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Description));
            //.ForMember(dest => dest.ProjectPhases, opt => opt.MapFrom(src => src.projectPhases))
            //.ForMember(dest => dest.EmployeesInProjectIds, opt => opt.MapFrom(src => src.Employees.Select(e => e.EmployeeId)));

            CreateMap<CreateProjectDTO, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.TotalBudget, opt => opt.MapFrom(src => src.ProjectTotalBudget))
                .ForMember(dest => dest.HoursBudget, opt => opt.MapFrom(src => src.ProjectHours))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.ProjectLocation))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProjectDescription));
            #endregion

            #region GetAllProjectsDTO
            CreateMap<Project, GetAllProjectsDTO>()
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ProjectTotalBudget, opt => opt.MapFrom(src => src.TotalBudget))
                    .ForMember(dest => dest.ProjectHours, opt => opt.MapFrom(src => src.HoursBudget))
                    .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                    .ForMember(dest => dest.ProjectLocation, opt => opt.MapFrom(src => src.Location))
                    .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.EndDate))
                    .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Description));
            //.ForMember(dest => dest.ProjectAttendances, opt => opt.MapFrom(src => src.Attendances))
            //.ForMember(dest => dest.ProjectPhases, opt => opt.MapFrom(src => src.projectPhases))
            //.ForMember(dest => dest.ProjectTasks, opt => opt.MapFrom(src => src.projectTasks))
            //.ForMember(dest => dest.EmployeesInProject, opt => opt.MapFrom(src => src.Employees.Select(ep => ep.Employee)));

            CreateMap<GetAllProjectsDTO, Project>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProjectId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.TotalBudget, opt => opt.MapFrom(src => src.ProjectTotalBudget))
                .ForMember(dest => dest.HoursBudget, opt => opt.MapFrom(src => src.ProjectHours))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.ProjectLocation))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProjectDescription));
            //.ForMember(dest => dest.Attendances, opt => opt.MapFrom(src => src.ProjectAttendances))
            //.ForMember(dest => dest.projectPhases, opt => opt.MapFrom(src => src.ProjectPhases))
            //.ForMember(dest => dest.projectTasks, opt => opt.MapFrom(src => src.ProjectTasks))
            //.ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.EmployeesInProject));

            #endregion

            #region GetProjectPhasesByProjectIdDTO
            CreateMap<ProjectPhase, GetProjectPhasesByProjectIdDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PhaseName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.PhaseStartDate, opt => opt.MapFrom(src => src.StartPhase))
                    .ForMember(dest => dest.PhaseEndDate, opt => opt.MapFrom(src => src.EndPhase))
                    .ForMember(dest => dest.PhaseMilestone, opt => opt.MapFrom(src => src.Milestone))
                    .ForMember(dest => dest.PhaseHrBudget, opt => opt.MapFrom(src => src.HrBudget));

            CreateMap<GetProjectPhasesByProjectIdDTO, ProjectPhase>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PhaseName))
                .ForMember(dest => dest.StartPhase, opt => opt.MapFrom(src => src.PhaseStartDate))
                .ForMember(dest => dest.EndPhase, opt => opt.MapFrom(src => src.PhaseEndDate))
                .ForMember(dest => dest.Milestone, opt => opt.MapFrom(src => src.PhaseMilestone))
                .ForMember(dest => dest.HrBudget, opt => opt.MapFrom(src => src.PhaseHrBudget));
            #endregion

            #region ProjectDTO
            CreateMap<Project, ProjectDTO>()
                    .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ProjectTotalBudget, opt => opt.MapFrom(src => src.TotalBudget))
                    .ForMember(dest => dest.ProjectHours, opt => opt.MapFrom(src => src.HoursBudget))
                    .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                    .ForMember(dest => dest.ProjectLocation, opt => opt.MapFrom(src => src.Location))
                    .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.EndDate))
                    .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Description));
            //.ForMember(dest => dest.ProjectAttendances, opt => opt.MapFrom(src => src.Attendances))
            //.ForMember(dest => dest.ProjectPhases, opt => opt.MapFrom(src => src.projectPhases))
            //.ForMember(dest => dest.ProjectTasks, opt => opt.MapFrom(src => src.projectTasks))
            //.ForMember(dest => dest.EmployeesInProject, opt => opt.MapFrom(src => src.Employees));

            CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.TotalBudget, opt => opt.MapFrom(src => src.ProjectTotalBudget))
                .ForMember(dest => dest.HoursBudget, opt => opt.MapFrom(src => src.ProjectHours))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.ProjectLocation))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProjectDescription));
            //.ForMember(dest => dest.Attendances, opt => opt.MapFrom(src => src.ProjectAttendances))
            //.ForMember(dest => dest.projectPhases, opt => opt.MapFrom(src => src.ProjectPhases))
            //.ForMember(dest => dest.projectTasks, opt => opt.MapFrom(src => src.ProjectTasks))
            //.ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.EmployeesInProject)); 
            #endregion

            #region UpdateProjectDTO
            CreateMap<Project, UpdateProjectDTO>()
                    .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ProjectTotalBudget, opt => opt.MapFrom(src => src.TotalBudget))
                    .ForMember(dest => dest.ProjectHours, opt => opt.MapFrom(src => src.HoursBudget))
                    .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                    .ForMember(dest => dest.ProjectLocation, opt => opt.MapFrom(src => src.Location))
                    .ForMember(dest => dest.ProjectStartDate, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.ProjectEndDate, opt => opt.MapFrom(src => src.EndDate))
                    .ForMember(dest => dest.ProjectDescription, opt => opt.MapFrom(src => src.Description));

            CreateMap<UpdateProjectDTO, Project>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(dest => dest.TotalBudget, opt => opt.MapFrom(src => src.ProjectTotalBudget))
                .ForMember(dest => dest.HoursBudget, opt => opt.MapFrom(src => src.ProjectHours))
                .ForMember(dest => dest.ProjectStatus, opt => opt.MapFrom(src => src.ProjectStatus))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.ProjectLocation))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ProjectStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.ProjectEndDate))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProjectDescription)); 
            #endregion

        }

    }
}
