using HR_Management_System.DTO;
using HR_Management_System.Models;
using HR_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Controllers
{
    [ApiController]
    [Route("api/projectphases")]
    public class ProjectPhaseController : ControllerBase
    {
        private readonly IProjectPhaseService _projectPhaseService;

        public ProjectPhaseController(IProjectPhaseService projectPhaseService)
        {
            _projectPhaseService = projectPhaseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectPhase(ProjectPhaseDTO projectPhaseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Map the DTO to the ProjectPhase entity
                var projectPhase = new ProjectPhase
                {
                    Name = projectPhaseDTO.Name,
                    StartPhase = projectPhaseDTO.StartDate,
                    EndPhase = projectPhaseDTO.EndDate,
                    Milestone = projectPhaseDTO.Milestone,
                    HrBudget = projectPhaseDTO.HrBudget
                };

                // Save the project phase to the database
                await _projectPhaseService.AddAsync(projectPhase);

                return Ok("Project phase created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectPhase(int id)
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
                    Name = projectPhase.Name,
                    StartDate = projectPhase.StartPhase,
                    EndDate = projectPhase.EndPhase,
                    Milestone = projectPhase.Milestone,
                    HrBudget = projectPhase.HrBudget,
                };
                return Ok(projectPhaseDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectPhase(int id, ProjectPhaseDTO projectPhaseDTO)
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
                projectPhase.Name = projectPhaseDTO.Name;
                projectPhase.StartPhase = projectPhaseDTO.StartDate;
                projectPhase.EndPhase = projectPhaseDTO.EndDate;
                projectPhase.Milestone = projectPhaseDTO.Milestone;
                projectPhase.HrBudget = projectPhaseDTO.HrBudget;

                await _projectPhaseService.UpdateAsync(id, projectPhase);
                return Ok("Project phase updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
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
