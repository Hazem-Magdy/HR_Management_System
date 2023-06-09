using Microsoft.AspNetCore.Mvc;
using HR_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Services;
using HR_Management_System.DTO;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IProjectPhaseService _projectPhaseService;
        private readonly IProjectTasksService _projectTaskService;
        private readonly IEmployeeProjectService _employeeProjectService;


        public ProjectsController(
            IProjectService projectService,
            IProjectPhaseService projectPhaseService,
            IProjectTasksService projectTaskService,
            IEmployeeProjectService employeeProjectService
        )
        {
            _projectService = projectService;
            _projectPhaseService = projectPhaseService;
            _projectTaskService = projectTaskService;
            _employeeProjectService = employeeProjectService;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetProjects()
        {
            var projects = await _projectService.GetAllAsync();
            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();
            foreach (var project in projects)
            {
                var ProjectDto = new ProjectDTO
                {
                    ProjectName = project.Name,
                    TotalBudget = project.TotalBudget,
                    ProjectHours = project.HoursBudget,
                    ProjectStatus = project.ProjectStatus,
                    ProjectLocation = project.Location,
                    ProjectStartDate = project.StartDate,
                    ProjectEndDate = project.EndDate,
                    ProjectDescription = project.Description,
                    ProjectTasksIds = project.projectTasks.Select(a => a.Id).ToList(),
                    EmployeesInProjectIds = project.employeeProjects.Select(e => e.Id).ToList(),
                    //Phases = project.projectPhases.Select(p => p.Id).ToList()
                };
                projectDTOs.Add(ProjectDto);
            }
            return Ok(projectDTOs);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await _projectService.GetByIDAsync(id);

            if (project == null)
            {
                return NotFound();
            }
            var ProjectDto = new ProjectDTO
            {
                ProjectName = project.Name,
                TotalBudget = project.TotalBudget,
                ProjectHours = project.HoursBudget,
                ProjectStatus = project.ProjectStatus,
                ProjectLocation = project.Location,
                ProjectStartDate = project.StartDate,
                ProjectEndDate = project.EndDate,
                ProjectDescription = project.Description,
                ProjectTasksIds = project.projectTasks.Select(a=>a.Id).ToList(),
                EmployeesInProjectIds = project.employeeProjects.Select(e=>e.Id).ToList(),
                //Phases = project.projectPhases.Select(p => p.Id).ToList()
            };

            return ProjectDto;
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(ProjectDTO projectDTO)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Name = projectDTO.ProjectName,
                    TotalBudget = projectDTO.TotalBudget,
                    HoursBudget = projectDTO.ProjectHours,
                    ProjectStatus = projectDTO.ProjectStatus,
                    Location = projectDTO.ProjectLocation,
                    StartDate = projectDTO.ProjectStartDate,
                    EndDate = projectDTO.ProjectEndDate,
                    Description = projectDTO.ProjectLocation
                };

                foreach (var taskId in projectDTO.ProjectTasksIds)
                {
                    var task = await _projectTaskService.GetByIdAsync(taskId);
                    if (task != null)
                    {
                        project.projectTasks.Add(task);
                    }
                }

                foreach (var employeeProjectId in projectDTO.EmployeesInProjectIds)
                {
                    var employeeProject = await _employeeProjectService.GetByIdAsync(employeeProjectId);
                    if (employeeProject != null)
                    {
                        project.employeeProjects.Add(employeeProject);
                    }
                }

                await _projectService.AddAsync(project);

                return CreatedAtAction("GetProject", new { id = project.Id }, project);
            }

            return BadRequest(ModelState);
        }


        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDTO projectDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await _projectService.GetByIDAsync(id);

            if (project == null)
                return NotFound();

            project.Name = projectDTO.ProjectName;
            project.TotalBudget = projectDTO.TotalBudget;
            project.HoursBudget = projectDTO.ProjectHours;
            project.ProjectStatus = projectDTO.ProjectStatus;
            project.Location = projectDTO.ProjectLocation;
            project.StartDate = projectDTO.ProjectStartDate;
            project.EndDate = projectDTO.ProjectEndDate;
            project.Description = projectDTO.ProjectDescription;

            try
            {
                await _projectService.UpdateAsync(id, project);
                return Ok("Project updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the project: {ex.Message}");
            }
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectService.GetByIDAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            try
            {
                await _projectService.DeleteAsync(id);
                return Ok("Project deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the project: {ex.Message}");
            }
        }

        [HttpGet("ProjectExists/{id}")]
        public bool ProjectExists(int id)
        {
            return _projectService.ProjectExists(id);
        }
    }
}
