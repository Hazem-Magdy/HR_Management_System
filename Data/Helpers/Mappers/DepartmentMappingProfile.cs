using AutoMapper;
using HR_Management_System.DTO.Department;
using HR_Management_System.Models;

namespace HR_Management_System.Data.Helpers.Mappers
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {

            #region DepaermentDTO
            CreateMap<DepartmentDTO, Department>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.ManagerId))
                    .ForMember(dest => dest.Employees, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.NoEmployees, opt => opt.Ignore())
                    .ForMember(dest => dest.Employee, opt => opt.Ignore());

            CreateMap<Department, DepartmentDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.EmployessIds, opt => opt.Ignore());
            #endregion

            #region GetDepartmentWithManagerName
            CreateMap<Department, GetDepartmentsWithMangerNameDTO>()
                    .ForMember(dest => dest.MangerName, opt => opt.MapFrom(src => src.EmployeeId != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : ""))
                    .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.EmployeeUnderWork, opt => opt.MapFrom(src => src.NoEmployees.HasValue ? src.NoEmployees.Value : 0));

            CreateMap<GetDepartmentsWithMangerNameDTO, Department>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.NoEmployees, opt => opt.MapFrom(src => src.EmployeeUnderWork));
            #endregion

            #region GetDepartmentsWithMangersNamesDTO
            CreateMap<Department, GetDepartmentsWithMangersNamesDTO>()
                    .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.MangerName, opt => opt.MapFrom(src => src.EmployeeId != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : "Department has no manager yet."))
                    .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.NOEmployees, opt => opt.MapFrom(src => src.NoEmployees.HasValue ? src.NoEmployees.Value : 0));

            CreateMap<GetDepartmentsWithMangersNamesDTO, Department>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.NoEmployees, opt => opt.MapFrom(src => src.NOEmployees));
            #endregion

            #region GetDepartmentWithEmployessDTO
            CreateMap<Department, GetDepartmentWithEmployessDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.EmployeeId != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : "Department has no manager yet."))
                    .ForMember(dest => dest.NoEmployees, opt => opt.MapFrom(src => src.NoEmployees))
                    .ForMember(dest => dest.Employees, opt => opt.Ignore());
            CreateMap<GetDepartmentWithEmployessDTO, Department>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.NoEmployees, opt => opt.MapFrom(src => src.NoEmployees));

            #endregion

        }
    }
}
