using Microsoft.AspNetCore.Mvc;
using HR_Management_System.Models;
using HR_Management_System.DTO.Project;
using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.DTO.ProjectTask;
using HR_Management_System.Services.InterfacesServices;
using HR_Management_System.DTO.Attendance;
using HR_Management_System.DTO.EmployeeProject;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IProjectPhaseService _projectPhaseService;
        private readonly IProjectTasksService _projectTaskService;
        private readonly IEmployeeService _employeeService;



        public ProjectsController(
            IProjectService projectService,
            IProjectPhaseService projectPhaseService,
            IProjectTasksService projectTaskService,
            IEmployeeService employeeService
        )
        {
            _projectService = projectService;
            _projectPhaseService = projectPhaseService;
            _projectTaskService = projectTaskService;
            _employeeService = employeeService;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<List<GetAllProjectsDTO>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllAsync(
                p => p.projectPhases,
                p => p.projectTasks,
                p => p.Employees,
                p => p.Attendances
            );

            List<GetAllProjectsDTO> projectDTOs = new List<GetAllProjectsDTO>();
            foreach (var project in projects)
            {
                List<ProjectPhaseWithIdDTO> projectPhaseDTOs = new List<ProjectPhaseWithIdDTO>();
                foreach (var phase in project.projectPhases)
                {
                    ProjectPhaseWithIdDTO projectPhaseDTO = new ProjectPhaseWithIdDTO()
                    {
                        PhaseId = phase.Id,
                        PhaseName = phase.Name,
                        PhaseStartDate = phase.StartPhase,
                        PhaseEndDate = phase.EndPhase,
                        PhaseMilestone = phase.Milestone,
                        PhaseHrBudget = phase.HrBudget
                    };
                    projectPhaseDTOs.Add(projectPhaseDTO);
                }
                var projectDto = new GetAllProjectsDTO()
                {
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                    ProjectTotalBudget = project.TotalBudget,
                    ProjectHours = project.HoursBudget,
                    ProjectStatus = project.ProjectStatus,
                    ProjectLocation = project.Location,
                    ProjectStartDate = project.StartDate,
                    ProjectEndDate = project.EndDate,
                    ProjectDescription = project.Description,
                    ProjectTasksIds = project.projectTasks.Select(a => a.Id).ToList(),
                    EmployeesInProjectIds = project.Employees.Select(e => e.Id).ToList(),
                    ProjectPhases = projectPhaseDTOs
                };

                projectDTOs.Add(projectDto);
            }
            return Ok(projectDTOs);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdCustomAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            List<ProjectPhaseDTO> projectPhaseDTOs = new List<ProjectPhaseDTO>();
            List<GetAttendancesInProjectDTO> projectAttendanceDTOs = new List<GetAttendancesInProjectDTO>();
            foreach (var phase in project.projectPhases)
            {
                ProjectPhaseDTO projectPhaseDTO = new ProjectPhaseDTO()
                {
                    PhaseName = phase.Name,
                    PhaseStartDate = phase.StartPhase,
                    PhaseEndDate = phase.EndPhase,
                    PhaseMilestone = phase.Milestone,
                    PhaseHrBudget = phase.HrBudget
                };
                projectPhaseDTOs.Add(projectPhaseDTO);
            }


            foreach (var attendance in project.Attendances)
            {
                GetAttendancesInProjectDTO projectAttendanceDTO = new GetAttendancesInProjectDTO()
                {
                    EmployeeName = string.Concat(attendance.Employee.FirstName, " ", attendance.Employee.LastName),
                    ProjectName = attendance.Project.Name,
                    PhaseName = attendance.ProjectPhase.Name.ToString(),
                    TaskName = attendance.ProjectTask.Name,
                    Date = attendance.Date,
                    Description = attendance.Description,
                    HoursSpent = attendance.HoursSpent


                };
                projectAttendanceDTOs.Add(projectAttendanceDTO);
            }


            var ProjectDto = new ProjectDTO
            {
                ProjectName = project.Name,
                ProjectTotalBudget = project.TotalBudget,
                ProjectHours = project.HoursBudget,
                ProjectStatus = project.ProjectStatus,
                ProjectLocation = project.Location,
                ProjectStartDate = project.StartDate,
                ProjectEndDate = project.EndDate,
                ProjectDescription = project.Description,
                ProjectTasksIds = project.projectTasks.Select(a => a.Id).ToList(),
                EmployeesInProjectIds = project.Employees.Select(e => e.Id).ToList(),
                ProjectPhases = projectPhaseDTOs,
                ProjectAttendances = projectAttendanceDTOs

            };

            return Ok(ProjectDto);
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(CreateProjectDTO projectDTO)
        {
            if (ModelState.IsValid)
            {
                List<ProjectPhase> projectPhases = new List<ProjectPhase>();
                List<EmployeeProject> employeeProjects = new List<EmployeeProject>();
                foreach (ProjectPhaseDTO projectPhaseDTO in projectDTO.ProjectPhases)
                {
                    ProjectPhase projectPhase = new ProjectPhase
                    {
                        Name = projectPhaseDTO.PhaseName,
                        StartPhase = projectPhaseDTO.PhaseStartDate,
                        EndPhase = projectPhaseDTO.PhaseEndDate,
                        Milestone = projectPhaseDTO.PhaseMilestone,
                        HrBudget = projectPhaseDTO.PhaseHrBudget
                    };
                    projectPhases.Add(projectPhase);
                }

                var project = new Project
                {
                    Name = projectDTO.ProjectName,
                    TotalBudget = projectDTO.ProjectTotalBudget,
                    HoursBudget = projectDTO.ProjectHours,
                    ProjectStatus = projectDTO.ProjectStatus,
                    Location = projectDTO.ProjectLocation,
                    StartDate = projectDTO.ProjectStartDate,
                    EndDate = projectDTO.ProjectEndDate,
                    Description = projectDTO.ProjectDescription,
                    projectPhases = projectPhases,
                };

                Project createdProject = await _projectService.AddAsync(project);

                foreach (int EmployyeId in projectDTO.EmployeesInProjectIds)
                {
                    var employee = await _employeeService.GetByIdAsync(EmployyeId);

                    if (employee != null)
                    {
                        EmployeeProject employeeProject = new EmployeeProject()
                        {
                            EmployeeId = EmployyeId,
                            ProjectId = createdProject.Id,

                        };
                        employeeProjects.Add(employeeProject);
                    }
                    
                }
                project.Employees = employeeProjects;


                
                return Ok("project created successfully");
            }
            return BadRequest(ModelState);
        }


        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectDTO projectDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = await _projectService.GetByIdAsync(id, p => p.projectPhases);

            if (project == null)
                return NotFound();

            project.Name = projectDTO.ProjectName;
            project.TotalBudget = projectDTO.ProjectTotalBudget;
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

        // get project tasks by projectId
        [HttpGet("/api/GetProjectTasks/{projectId}")]
        public async Task<IActionResult> GetProjectTasksByProjectId(int projectId)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(projectId, t => t.projectTasks);

                if (project == null)
                    return NotFound();
                List<GetProjectTasksByProjectIdDTO> dTOs = new List<GetProjectTasksByProjectIdDTO>();

                foreach (var projectTask in project.projectTasks.ToList())
                {
                    GetProjectTasksByProjectIdDTO dTO = new GetProjectTasksByProjectIdDTO()
                    {
                        TaskName = projectTask.Name,
                        TaskDescription = projectTask.Description,
                        TotalHoursPerTask = projectTask.ToltalHoursPerTask
                    };
                    dTOs.Add(dTO);
                }
                return Ok(dTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving project tasks: {ex.Message}");
            }
        }

        [HttpGet("/api/GetProjectPhases/{projectId}")]
        public async Task<IActionResult> GetProjectPhasesByProjectId(int projectId)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(projectId, p => p.projectPhases);

                if (project == null)
                    return NotFound();

                List<GetProjectPhasesByProjectIdDTO> dTOs = new List<GetProjectPhasesByProjectIdDTO>();

                foreach (var projectPhase in project.projectPhases.ToList())
                {
                    GetProjectPhasesByProjectIdDTO dTO = new GetProjectPhasesByProjectIdDTO()
                    {
                        PhaseName = projectPhase.Name,
                        PhaseStartDate = projectPhase.StartPhase,
                        PhaseEndDate = projectPhase.EndPhase,
                        PhaseHrBudget = projectPhase.HrBudget,
                        PhaseMilestone = projectPhase.Milestone
                    };

                    dTOs.Add(dTO);
                }
                return Ok(dTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving project tasks: {ex.Message}");
            }
        }

        [HttpGet("ProjectExists/{projectId}")]
        public bool ProjectExists(int projectId)
        {
            return _projectService.ProjectExists(projectId);
        }
    }
}
