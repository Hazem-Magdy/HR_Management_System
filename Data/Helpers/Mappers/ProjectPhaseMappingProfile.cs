
using AutoMapper;
using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.Models;

namespace HR_Management_System.Data.Helpers.Mappers
{
    public class ProjectPhaseMappingProfile : Profile
    {
        public ProjectPhaseMappingProfile()
        {
            #region ProjectPhaseDTO
            CreateMap<ProjectPhase, ProjectPhaseDTO>()
                   .ForMember(dest => dest.PhaseName, opt => opt.MapFrom(src => src.Name))
                   .ForMember(dest => dest.PhaseStartDate, opt => opt.MapFrom(src => src.StartPhase))
                   .ForMember(dest => dest.PhaseEndDate, opt => opt.MapFrom(src => src.EndPhase))
                   .ForMember(dest => dest.PhaseMilestone, opt => opt.MapFrom(src => src.Milestone))
                   .ForMember(dest => dest.PhaseHrBudget, opt => opt.MapFrom(src => src.HrBudget));

            CreateMap<ProjectPhaseDTO, ProjectPhase>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PhaseName))
                .ForMember(dest => dest.StartPhase, opt => opt.MapFrom(src => src.PhaseStartDate))
                .ForMember(dest => dest.EndPhase, opt => opt.MapFrom(src => src.PhaseEndDate))
                .ForMember(dest => dest.Milestone, opt => opt.MapFrom(src => src.PhaseMilestone))
                .ForMember(dest => dest.HrBudget, opt => opt.MapFrom(src => src.PhaseHrBudget));
            #endregion

            #region ProjectPahseWithIdDTO
            CreateMap<ProjectPhase, ProjectPhaseWithIdDTO>()
                    .ForMember(dest => dest.PhaseId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.PhaseName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.PhaseStartDate, opt => opt.MapFrom(src => src.StartPhase))
                    .ForMember(dest => dest.PhaseEndDate, opt => opt.MapFrom(src => src.EndPhase))
                    .ForMember(dest => dest.PhaseMilestone, opt => opt.MapFrom(src => src.Milestone))
                    .ForMember(dest => dest.PhaseHrBudget, opt => opt.MapFrom(src => src.HrBudget));

            CreateMap<ProjectPhaseWithIdDTO, ProjectPhase>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PhaseId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PhaseName))
                .ForMember(dest => dest.StartPhase, opt => opt.MapFrom(src => src.PhaseStartDate))
                .ForMember(dest => dest.EndPhase, opt => opt.MapFrom(src => src.PhaseEndDate))
                .ForMember(dest => dest.Milestone, opt => opt.MapFrom(src => src.PhaseMilestone))
                .ForMember(dest => dest.HrBudget, opt => opt.MapFrom(src => src.PhaseHrBudget));
            #endregion

            #region ProjectPhaseWithNoIdDTO
            CreateMap<ProjectPhase, ProjectPhaseWithNoIdDTO>()
                    .ForMember(dest => dest.PhaseName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.PhaseStartDate, opt => opt.MapFrom(src => src.StartPhase))
                    .ForMember(dest => dest.PhaseEndDate, opt => opt.MapFrom(src => src.EndPhase))
                    .ForMember(dest => dest.PhaseMilestone, opt => opt.MapFrom(src => src.Milestone))
                    .ForMember(dest => dest.PhaseHrBudget, opt => opt.MapFrom(src => src.HrBudget));

            CreateMap<ProjectPhaseWithNoIdDTO, ProjectPhase>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PhaseName))
                .ForMember(dest => dest.StartPhase, opt => opt.MapFrom(src => src.PhaseStartDate))
                .ForMember(dest => dest.EndPhase, opt => opt.MapFrom(src => src.PhaseEndDate))
                .ForMember(dest => dest.Milestone, opt => opt.MapFrom(src => src.PhaseMilestone))
                .ForMember(dest => dest.HrBudget, opt => opt.MapFrom(src => src.PhaseHrBudget)); 
            #endregion

        }
    }
}
