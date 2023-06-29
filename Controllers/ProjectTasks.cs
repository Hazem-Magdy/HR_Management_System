using HR_Management_System.DTO;
using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.DTO.ProjectTask;
using HR_Management_System.Models;
using HR_Management_System.Services.ClassesServices;
using HR_Management_System.Services.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HR_Management_System.Controllers
{
    [AdminAccountantOnly]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTasksService _projectTaskService;
        private readonly IProjectService _projectService;

        public ProjectTaskController(IProjectTasksService projectTaskService, IProjectService projectService)
        {
            _projectTaskService = projectTaskService;
            _projectService = projectService;
        }

        [HttpGet]
        [AdminAccountantOnly]
        public async Task<IActionResult> GetAllProjectsTasks()
        {
            IEnumerable<ProjectTask> projectsTasks = await _projectTaskService.getAllIncludinProjectAsync();
            List<GetAllProjectsTasksDTO> projectsTasksDTOs = new List<GetAllProjectsTasksDTO>();

            if (projectsTasks == null)
            {
                return NotFound("Project Task no longer exist.");
            }

            try
            {
                foreach (var projectTask in projectsTasks.ToList())
                {
                    var projectTaskDto = new GetAllProjectsTasksDTO
                    {
                        TaskId = projectTask.Id,
                        TaskName = projectTask.Name,
                        TaskDescription = projectTask.Description,
                        ToltalHoursPerTask = projectTask.ToltalHoursPerTask,
                        ProjectName = projectTask.Project.Name

                    };
                    projectsTasksDTOs.Add(projectTaskDto);
                }
                return Ok(projectsTasksDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [AdminOnly]
        public async Task<IActionResult> CreateProjectTask(ProjectTaskDTO projectTaskDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to the ProjectTask model
            var projectTask = new ProjectTask
            {
                Name = projectTaskDTO.TaskName,
                Description = projectTaskDTO.TaskDescription,
                ToltalHoursPerTask = projectTaskDTO.TotalHoursPerTask,
                ProjectId = projectTaskDTO.ProjectId
            };

            try
            {
                // Save the project task to the database
                await _projectTaskService.AddAsync(projectTask);

                return Ok("Project task created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the project task: {ex.Message}");
            }
        }

        // get task With project name by taskId 
        [HttpGet("{id}")]
        [AdminAccountantOnly]
        public async Task<IActionResult> GetProjectTaskById(int id)
        {
            var projectTask = await _projectTaskService.GetByIdAsync(id, p=>p.Project);

            if (projectTask== null)
                return NotFound("Project Task no longer exist.");
            var TaskWhithProjectNameDTO = new TaskWhithProjectNameDTO()
            {
                
                TaskName = projectTask.Name,
                TaskDescription = projectTask.Description,
                TotalHoursPerTask = projectTask.ToltalHoursPerTask,
                ProjectName = projectTask.Project.Name
            };

            return Ok(TaskWhithProjectNameDTO);
        }

        [HttpPut("{id}")]
        [AdminAccountantOnly]
        public async Task<IActionResult> UpdateProjectTask(int id, UpdateProjectTaskDTO projectTaskDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectTask = await _projectTaskService.GetByIdAsync(id);

            if (projectTask == null)
                return NotFound("Project Task no longer exist.");

            // Update the project task properties
            projectTask.Name = projectTaskDTO.TaskName;
            projectTask.Description = projectTaskDTO.TaskDescription;
            projectTask.ToltalHoursPerTask = projectTaskDTO.TotalHoursPerTask;

            try
            {
                await _projectTaskService.UpdateAsync(id, projectTask);
                return Ok("Project task updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the project task: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [AdminOnly]
        public async Task<IActionResult> DeleteProjectTask(int id)
        {
            var projectTask = await _projectTaskService.GetByIdAsync(id);

            if (projectTask == null)
                return NotFound("Project Task no longer exist.");

            try
            {
                await _projectTaskService.DeleteAsync(id);
                return Ok("Project task deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the project task: {ex.Message}");
            }
        }
    }
}
