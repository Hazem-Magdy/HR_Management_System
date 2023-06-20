using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IProjectTasksService : IEntityBaseRepository<ProjectTask> {
        public Task<IEnumerable<ProjectTask>> getAllIncludinProjectAsync();
    }

}
