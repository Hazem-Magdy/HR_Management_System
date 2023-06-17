using AutoMapper;
using HR_Management_System.DTO.EmployeeProject;
using HR_Management_System.Models;

namespace HR_Management_System.Data.Helpers.Mappers
{
    public class EmployeeProjectMappingProfile : Profile
    {
        public EmployeeProjectMappingProfile()
        {

            #region EmployeeProjectDTO
            CreateMap<EmployeeProject, EmployeeProjectDTO>()
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                    .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId));

            CreateMap<EmployeeProjectDTO, EmployeeProject>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId)); 
            #endregion

            
        }
    }
}
