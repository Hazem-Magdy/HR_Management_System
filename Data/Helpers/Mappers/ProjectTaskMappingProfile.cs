using AutoMapper;
using HR_Management_System.DTO.ProjectTask;
using HR_Management_System.Models;

namespace HR_Management_System.Data.Helpers.Mappers
{
    public class ProjectTaskMappingProfile : Profile
    {
        public ProjectTaskMappingProfile()
        {
            #region GetProjectTasksByProjectIdDTO
            CreateMap<ProjectTask, GetProjectTasksByProjectIdDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.TotalHoursPerTask, opt => opt.MapFrom(src => src.ToltalHoursPerTask));

            CreateMap<GetProjectTasksByProjectIdDTO, ProjectTask>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(dest => dest.ToltalHoursPerTask, opt => opt.MapFrom(src => src.TotalHoursPerTask));
            #endregion

            #region ProjectTaskDTO
            CreateMap<ProjectTask, ProjectTaskDTO>()
                    .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.TotalHoursPerTask, opt => opt.MapFrom(src => src.ToltalHoursPerTask))
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));

            CreateMap<ProjectTaskDTO, ProjectTask>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(dest => dest.ToltalHoursPerTask, opt => opt.MapFrom(src => src.TotalHoursPerTask))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
            #endregion

            #region ProjectTaskWithIdDTO
            CreateMap<ProjectTask, ProjectTaskWithIdDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.TotalHoursPerTask, opt => opt.MapFrom(src => src.ToltalHoursPerTask))
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));

            CreateMap<ProjectTaskWithIdDTO, ProjectTask>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(dest => dest.ToltalHoursPerTask, opt => opt.MapFrom(src => src.TotalHoursPerTask))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));
            #endregion

            #region TaskWithProjectNameDTO
            CreateMap<ProjectTask, TaskWhithProjectNameDTO>()
                    .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.TotalHoursPerTask, opt => opt.MapFrom(src => src.ToltalHoursPerTask))
                    .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name));

            CreateMap<TaskWhithProjectNameDTO, ProjectTask>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(dest => dest.ToltalHoursPerTask, opt => opt.MapFrom(src => src.TotalHoursPerTask));
            #endregion

            #region UpdateProjectTaskDTO
            CreateMap<ProjectTask, UpdateProjectTaskDTO>()
                    .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaskDescription, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.TotalHoursPerTask, opt => opt.MapFrom(src => src.ToltalHoursPerTask));

            CreateMap<UpdateProjectTaskDTO, ProjectTask>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TaskDescription))
                .ForMember(dest => dest.ToltalHoursPerTask, opt => opt.MapFrom(src => src.TotalHoursPerTask)); 
            #endregion

        }
    }
}
