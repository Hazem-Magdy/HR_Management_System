using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IProjectService : IEntityBaseRepository<Project>
    {

        bool ProjectExists(int id);

        Task<Project> GetProjectByIdCustomAsync(int id);

        Task<List<Project>> GetAllProjectsCustomAsync();

    }

}
