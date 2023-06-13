namespace HR_Management_System.DTO.Department
{
    public class GetDepartmentsWithMangersNamesDTO
    {
        public int DepartmentId { get; set; }
        public string MangerName { get; set; } 

        public string DepartmentName { get; set; } 

        public int NOEmployees { get; set; } 
    }
}
