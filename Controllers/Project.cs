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
            var projects = await _projectService.GetAllAsync(
                p => p.projectPhases,
                p => p.projectTasks,
                p => p.employeeProjects,
                p => p.Attendances
            );

            List<ProjectDTO> projectDTOs = new List<ProjectDTO>();
            //List<ProjectPhaseDTO> projectPhaseDTOs = new List<ProjectPhaseDTO>();
            foreach (var project in projects)
            {
                List<ProjectPhaseDTO> projectPhaseDTOs = new List<ProjectPhaseDTO>();
                //projectPhaseDTOs.Clear();
                foreach (var phase in project.projectPhases)
                {
                    ProjectPhaseDTO projectPhaseDTO = new ProjectPhaseDTO()
                    {
                        Name = phase.Name,
                        StartDate = phase.StartPhase,
                        EndDate = phase.EndPhase,
                        Milestone = phase.Milestone,
                        HrBudget = phase.HrBudget
                    };
                    projectPhaseDTOs.Add(projectPhaseDTO);
                }
                var projectDto = new ProjectDTO
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
                    Phases = projectPhaseDTOs
                };


                projectDTOs.Add(projectDto);
            }
            return Ok(projectDTOs);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProject(int id)
        {
            var project = await _projectService.GetByIdAsync(id);

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
                ProjectTasksIds = project.projectTasks.Select(a => a.Id).ToList(),
                EmployeesInProjectIds = project.employeeProjects.Select(e => e.Id).ToList(),
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
                List<ProjectPhase> projectPhases = new List<ProjectPhase>();
                foreach (ProjectPhaseDTO projectPhaseDTO in projectDTO.Phases)
                {
                    ProjectPhase projectPhase = new ProjectPhase
                    {
                        Name = projectPhaseDTO.Name,
                        StartPhase = projectPhaseDTO.StartDate,
                        EndPhase = projectPhaseDTO.EndDate,
                        Milestone = projectPhaseDTO.Milestone,
                        HrBudget = projectPhaseDTO.HrBudget,
                        ProjectId = 0 // Temporary placeholder value
                    };
                    projectPhases.Add(projectPhase);
                }
                var project = new Project
                {
                    Name = projectDTO.ProjectName,
                    TotalBudget = projectDTO.TotalBudget,
                    HoursBudget = projectDTO.ProjectHours,
                    ProjectStatus = projectDTO.ProjectStatus,
                    Location = projectDTO.ProjectLocation,
                    StartDate = projectDTO.ProjectStartDate,
                    EndDate = projectDTO.ProjectEndDate,
                    Description = projectDTO.ProjectDescription,

                };

                await _projectService.AddAsync(project);

                // Assign the project ID to each project phase
                foreach (var phase in projectPhases.ToList())
                {
                    phase.ProjectId = project.Id;
                    await _projectPhaseService.AddAsync(phase);
                }

                return Ok("project created successfully");
            }
            return BadRequest(ModelState);
        }


        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDTO projectDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await _projectService.GetByIdAsync(id);

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
            var project = await _projectService.GetByIdAsync(id);
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
