using HR_Management_System.Data.Base;
using HR_Management_System.Models;

namespace HR_Management_System.Services.InterfacesServices
{
    public interface IProjectPhaseService : IEntityBaseRepository<ProjectPhase> {

        public Task<IEnumerable<ProjectPhase>> getAllIncludeProjectAsync();

        public Task<IEnumerable<ProjectPhase>> GetAllprojectPhasesCustom(int projectId);
    }

}
