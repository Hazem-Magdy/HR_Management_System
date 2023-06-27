namespace HR_Management_System.DTO.Department
{
    public class DeleteDepartmentRequestDTO
    {
        public int? TargetDepartmentId { get; set; }
        public List<int>? SelectedEmployeeIds { get; set; }
    }
}
