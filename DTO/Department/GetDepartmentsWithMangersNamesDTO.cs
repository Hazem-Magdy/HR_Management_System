namespace HR_Management_System.DTO.Department
{
    public class GetDepartmentsWithMangersNamesDTO
    {
        public List<string> MangersNames { get; set; } = new List<string>();

        public List<string> DepartmenstNames { get; set; } = new List<string>();

        public List<int> NOEmployeesInDepartment { get; set; } = new List<int>();
    }
}
