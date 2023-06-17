using AutoMapper;
using HR_Management_System.DTO.Employee;
using HR_Management_System.Models;

namespace HR_Management_System.Data.Helpers.Mappers
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            //Mapper.Map(source, destination)

            #region CreateEmployeeDTO
            // covert from createEmployeeDTo to Employee 
            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.EmployeeFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.EmployeeLastName))
                .ForMember(dest => dest.SalaryPerHour, opt => opt.MapFrom(src => src.EmployeeSalaryPerHour))
                .ForMember(dest => dest.OverTime, opt => opt.MapFrom(src => src.EmployeeOverTime))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.EmployeeSalary))
                .ForMember(dest => dest.ProfileUrl, opt => opt.MapFrom(src => src.EmployeeProfileUrl))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.EmployeePhone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmployeeEmail))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.EmployeePassword))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.EmployeePosition))
                .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.EmployeeHiringDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.EmployeeStatus));

            // covert from Employee to createEmployeeDTo  
            CreateMap<Employee, CreateEmployeeDTO>()
                .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmployeeSalaryPerHour, opt => opt.MapFrom(src => src.SalaryPerHour))
                .ForMember(dest => dest.EmployeeOverTime, opt => opt.MapFrom(src => src.OverTime))
                .ForMember(dest => dest.EmployeeSalary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.EmployeeProfileUrl, opt => opt.MapFrom(src => src.ProfileUrl))
                .ForMember(dest => dest.EmployeePhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.EmployeeEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmployeePassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.EmployeeHiringDate, opt => opt.MapFrom(src => src.HiringDate))
                .ForMember(dest => dest.EmployeeStatus, opt => opt.MapFrom(src => src.Status));
            #endregion

            #region EmployeeDeptDetailsDTO
            // covert from EmployeeDeptDetailsDTO to Employee
            CreateMap<EmployeeDeptDetailsDTO, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.EmployeeFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.EmployeeLastName))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.EmployeePosition));


            // covert from Employee to EmployeeDeptDetailsDTO
            CreateMap<Employee, EmployeeDeptDetailsDTO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.Position));

            #endregion

            #region GetAllEmployeeDTO
            CreateMap<GetAllEmployeesDTO, Employee>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmplyeeId))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.EmployeeFirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.EmployeeLastName))
                    .ForMember(dest => dest.SalaryPerHour, opt => opt.MapFrom(src => src.EmployeeSalaryPerHour))
                    .ForMember(dest => dest.OverTime, opt => opt.MapFrom(src => src.EmployeeOverTime))
                    .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.EmployeeSalary))
                    .ForMember(dest => dest.OvertimeRate, opt => opt.MapFrom(src => src.EmployeeOvertimeRate))
                    .ForMember(dest => dest.RegularHoursPerDay, opt => opt.MapFrom(src => src.EmployeeRegularHoursPerDay))
                    .ForMember(dest => dest.WorkingDaysPerWeek, opt => opt.MapFrom(src => src.EmployeeWorkingDaysPerWeek))
                    .ForMember(dest => dest.ProfileUrl, opt => opt.MapFrom(src => src.EmployeeProfileUrl))
                    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.EmployeePhone))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmployeeEmail))
                    .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.EmployeePosition))
                    .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.EmployeeHiringDate))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.EmployeeStatus))
                    .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<Employee, GetAllEmployeesDTO>()
                .ForMember(dest => dest.EmplyeeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmployeeSalaryPerHour, opt => opt.MapFrom(src => src.SalaryPerHour))
                .ForMember(dest => dest.EmployeeOverTime, opt => opt.MapFrom(src => src.OverTime))
                .ForMember(dest => dest.EmployeeSalary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.EmployeeOvertimeRate, opt => opt.MapFrom(src => src.OvertimeRate))
                .ForMember(dest => dest.EmployeeRegularHoursPerDay, opt => opt.MapFrom(src => src.RegularHoursPerDay))
                .ForMember(dest => dest.EmployeeWorkingDaysPerWeek, opt => opt.MapFrom(src => src.WorkingDaysPerWeek))
                .ForMember(dest => dest.EmployeeProfileUrl, opt => opt.MapFrom(src => src.ProfileUrl))
                .ForMember(dest => dest.EmployeePhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.EmployeeEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.EmployeeHiringDate, opt => opt.MapFrom(src => src.HiringDate))
                .ForMember(dest => dest.EmployeeStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            #endregion

            #region GetEmployeeByIdDTO
            CreateMap<GetEmployeeByIdDTO, Employee>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.EmployeeFirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.EmployeeLastName))
                    .ForMember(dest => dest.SalaryPerHour, opt => opt.MapFrom(src => src.EmployeeSalaryPerHour))
                    .ForMember(dest => dest.OverTime, opt => opt.MapFrom(src => src.EmployeeOverTime))
                    .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.EmployeeSalary))
                    .ForMember(dest => dest.OvertimeRate, opt => opt.MapFrom(src => src.EmployeeOvertimeRate))
                    .ForMember(dest => dest.RegularHoursPerDay, opt => opt.MapFrom(src => src.EmployeeRegularHoursPerDay))
                    .ForMember(dest => dest.WorkingDaysPerWeek, opt => opt.MapFrom(src => src.EmployeeWorkingDaysPerWeek))
                    .ForMember(dest => dest.ProfileUrl, opt => opt.MapFrom(src => src.EmployeeProfileUrl))
                    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.EmployeePhone))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmployeeEmail))
                    .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.EmployeePosition))
                    .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.EmployeeHiringDate))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.EmployeeStatus))
                    .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<Employee, GetEmployeeByIdDTO>()
                .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmployeeSalaryPerHour, opt => opt.MapFrom(src => src.SalaryPerHour))
                .ForMember(dest => dest.EmployeeOverTime, opt => opt.MapFrom(src => src.OverTime))
                .ForMember(dest => dest.EmployeeSalary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.EmployeeOvertimeRate, opt => opt.MapFrom(src => src.OvertimeRate))
                .ForMember(dest => dest.EmployeeRegularHoursPerDay, opt => opt.MapFrom(src => src.RegularHoursPerDay))
                .ForMember(dest => dest.EmployeeWorkingDaysPerWeek, opt => opt.MapFrom(src => src.WorkingDaysPerWeek))
                .ForMember(dest => dest.EmployeeProfileUrl, opt => opt.MapFrom(src => src.ProfileUrl))
                .ForMember(dest => dest.EmployeePhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.EmployeeEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.EmployeeHiringDate, opt => opt.MapFrom(src => src.HiringDate))
                .ForMember(dest => dest.EmployeeStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            #endregion

            #region UpdateEmployeeDTo
            CreateMap<UpdateEmployeeDTO, Employee>()
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.EmployeeFirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.EmployeeLastName))
                    .ForMember(dest => dest.SalaryPerHour, opt => opt.MapFrom(src => src.EmployeeSalaryPerHour))
                    .ForMember(dest => dest.OverTime, opt => opt.MapFrom(src => src.EmployeeOverTime))
                    .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.EmployeeSalary))
                    .ForMember(dest => dest.OvertimeRate, opt => opt.MapFrom(src => src.EmployeeOvertimeRate))
                    .ForMember(dest => dest.RegularHoursPerDay, opt => opt.MapFrom(src => src.EmployeeRegularHoursPerDay))
                    .ForMember(dest => dest.WorkingDaysPerWeek, opt => opt.MapFrom(src => src.EmployeeWorkingDaysPerWeek))
                    .ForMember(dest => dest.ProfileUrl, opt => opt.MapFrom(src => src.EmployeeProfileUrl))
                    .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.EmployeePhone))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmployeeEmail))
                    .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.EmployeePassword))
                    .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.EmployeePosition))
                    .ForMember(dest => dest.HiringDate, opt => opt.MapFrom(src => src.EmployeeHiringDate))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.EmployeeStatus))
                    .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<Employee, UpdateEmployeeDTO>()
                .ForMember(dest => dest.EmployeeFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.EmployeeLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmployeeSalaryPerHour, opt => opt.MapFrom(src => src.SalaryPerHour))
                .ForMember(dest => dest.EmployeeOverTime, opt => opt.MapFrom(src => src.OverTime))
                .ForMember(dest => dest.EmployeeSalary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.EmployeeOvertimeRate, opt => opt.MapFrom(src => src.OvertimeRate))
                .ForMember(dest => dest.EmployeeRegularHoursPerDay, opt => opt.MapFrom(src => src.RegularHoursPerDay))
                .ForMember(dest => dest.EmployeeWorkingDaysPerWeek, opt => opt.MapFrom(src => src.WorkingDaysPerWeek))
                .ForMember(dest => dest.EmployeeProfileUrl, opt => opt.MapFrom(src => src.ProfileUrl))
                .ForMember(dest => dest.EmployeePhone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.EmployeeEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.EmployeePassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.EmployeeHiringDate, opt => opt.MapFrom(src => src.HiringDate))
                .ForMember(dest => dest.EmployeeStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            #endregion


        }
    }
}
