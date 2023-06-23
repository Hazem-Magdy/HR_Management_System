using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.Models;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HR_Management_System.Controllers
{
    [ApiController]
    [Route("api/projectphases")]
    [Authorize(Roles = "Admin Accountant")]
    public class ProjectPhaseController : ControllerBase
    {
        private readonly IProjectPhaseService _projectPhaseService;
        private readonly IProjectService _projectService;

        public ProjectPhaseController(IProjectPhaseService projectPhaseService, IProjectService projectService)
        {
            _projectPhaseService = projectPhaseService;
            _projectService = projectService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin Accountant")]
        public async Task<IActionResult> GetAllProjectsPhases()
        {
            IEnumerable<ProjectPhase> projectsPhases = await _projectPhaseService.getAllIncludeProjectAsync();
            List<GetAllProjectsPhasesDTO> projectsPhasesDTOs = new List<GetAllProjectsPhasesDTO>();

            if (projectsPhases == null)
            {
                return NotFound();
            }

            try
            {
                foreach (var projectPhase in projectsPhases.ToList())
                {
                    var projectPhaseDto = new GetAllProjectsPhasesDTO
                    {
                        PhaseId= projectPhase.Id,
                        PhaseName = projectPhase.Name.ToString(),
                        PhaseStartDate= projectPhase.StartPhase,
                        PhaseEndDate= projectPhase.EndPhase,
                        PhaseHrBudget= projectPhase.HrBudget,
                        PhaseMilestone= projectPhase.Milestone,
                        ProjectName= projectPhase.Project.Name
                    };
                    projectsPhasesDTOs.Add(projectPhaseDto);
                }
                return Ok(projectsPhasesDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("{projectId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProjectPhase(int projectId, CreateUpdateProjectPhaseDTO projectPhaseDTO)
        {
            Project project = await _projectService.GetByIdAsync(projectId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Map the DTO to the ProjectPhase entity
                var projectPhase = new ProjectPhase
                {
                    Name = projectPhaseDTO.PhaseName,
                    StartPhase = projectPhaseDTO.PhaseStartDate,
                    EndPhase = projectPhaseDTO.PhaseEndDate,
                    Milestone = projectPhaseDTO.PhaseMilestone,
                    HrBudget = projectPhaseDTO.PhaseHrBudget,
                    ProjectId = projectId,  
                };

                project.projectPhases.Add(projectPhase);

                // Save the project phase to the database
                await _projectPhaseService.AddAsync(projectPhase);

                return Ok($"Project phase assigned to project: {project.Name} successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin Accountant")]
        public async Task<IActionResult> GetProjectPhaseById(int id)
        {
            try
            {
                var projectPhase = await _projectPhaseService.GetByIdAsync(id);
                if (projectPhase == null)
                {
                    return NotFound();
                }
                ProjectPhaseDTO projectPhaseDTO = new ProjectPhaseDTO
                {
                    Id = projectPhase.Id,
                    PhaseName = projectPhase.Name.ToString(),
                    PhaseStartDate = projectPhase.StartPhase,
                    PhaseEndDate = projectPhase.EndPhase,
                    PhaseMilestone = projectPhase.Milestone,
                    PhaseHrBudget = projectPhase.HrBudget,
                };
                return Ok(projectPhaseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProjectPhase(int id, CreateUpdateProjectPhaseDTO projectPhaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var projectPhase = await _projectPhaseService.GetByIdAsync(id);
                if (projectPhase == null)
                {
                    return NotFound();
                }

                // Update the project phase properties
                projectPhase.Name = projectPhaseDTO.PhaseName;
                projectPhase.StartPhase = projectPhaseDTO.PhaseStartDate;
                projectPhase.EndPhase = projectPhaseDTO.PhaseEndDate;
                projectPhase.Milestone = projectPhaseDTO.PhaseMilestone;
                projectPhase.HrBudget = projectPhaseDTO.PhaseHrBudget;

                await _projectPhaseService.UpdateAsync(id, projectPhase);
                return Ok("Project phase updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProjectPhase(int id)
        {
            try
            {
                var projectPhase = await _projectPhaseService.GetByIdAsync(id);
                if (projectPhase == null)
                {
                    return NotFound();
                }

                await _projectPhaseService.DeleteAsync(id);
                return Ok("Project phase deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
