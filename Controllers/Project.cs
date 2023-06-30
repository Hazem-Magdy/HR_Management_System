using Microsoft.AspNetCore.Mvc;
using HR_Management_System.Models;
using HR_Management_System.DTO.Project;
using HR_Management_System.DTO.ProjectPhase;
using HR_Management_System.DTO.ProjectTask;
using HR_Management_System.Services.InterfacesServices;
using HR_Management_System.DTO.Attendance;
using HR_Management_System.DTO.Employee;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using HR_Management_System.DTO.EmployeeProject;
using HR_Management_System.Data.Enums;

namespace HR_Management_System.Controllers
{
    [AdminAccountantHREmployee]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeProjectsService _employeeProjectsService;
        private readonly IProjectPhaseService _projectPhaseService;
        private readonly IMapper _mapper;


        public ProjectsController(
            IProjectService projectService,
            IEmployeeService employeeService,
            IEmployeeProjectsService employeeProjectsService,
            IProjectPhaseService projectPhaseService,
            IMapper mapper
        )
        {
            _projectService = projectService;
            _employeeService = employeeService;
            _employeeProjectsService = employeeProjectsService;
            _projectPhaseService = projectPhaseService;
            _mapper = mapper;
        }

        // GET: api/Projects
        [HttpGet]
        [AdminAccountantHREmployee]
        public async Task<ActionResult<List<GetAllProjectsDTO>>> GetAllProjects()
        {

            var projects = await _projectService.GetAllProjectsCustomAsync();

            List<GetAllProjectsDTO> projectDTOs = new List<GetAllProjectsDTO>();
            foreach (var project in projects)
            {
                List<ProjectPhaseWithIdDTO> projectPhaseDTOs = new List<ProjectPhaseWithIdDTO>();
                List<GetAttendancesInProjectDTO> projectAttendanceDTOs = new List<GetAttendancesInProjectDTO>();
                List<EmployeeDeptDetailsDTO> employeeDeptDetails = new List<EmployeeDeptDetailsDTO>();
                List<ProjectTaskWithIdDTO> projectTaskWithIdDTOs = new List<ProjectTaskWithIdDTO>();
                foreach (var phase in project.projectPhases)
                {
                    ProjectPhaseWithIdDTO projectPhaseDTO = new ProjectPhaseWithIdDTO()
                    {
                        PhaseId = phase.Id,
                        PhaseName = phase.Name.ToString(),
                        PhaseStartDate = phase.StartPhase,
                        PhaseEndDate = phase.EndPhase,
                        PhaseMilestone = phase.Milestone,
                        PhaseHrBudget = phase.HrBudget
                    };
                    projectPhaseDTOs.Add(projectPhaseDTO);
                }
                foreach (var task in project.projectTasks)
                {
                    ProjectTaskWithIdDTO projectTaskWithIdDTO = new ProjectTaskWithIdDTO()
                    {
                        Id = task.Id,
                        TaskName = task.Name,
                        TaskDescription = task.Description,
                        TotalHoursPerTask = task.ToltalHoursPerTask
                    };
                    projectTaskWithIdDTOs.Add(projectTaskWithIdDTO);
                }

                foreach (var attendance in project.Attendances)
                {
                    GetAttendancesInProjectDTO projectAttendanceDTO = new GetAttendancesInProjectDTO()
                    {
                        EmployeeId = attendance.EmployeeId,
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
                foreach (var employee in project.Employees.ToList())
                {
                    EmployeeDeptDetailsDTO employeeDept = new EmployeeDeptDetailsDTO()
                    {
                        EmployeeId = employee.Employee.Id,
                        EmployeeFirstName = employee.Employee.FirstName,
                        EmployeeLastName = employee.Employee.LastName,
                        EmployeePosition = employee.Employee.Position.ToString()
                    };
                    employeeDeptDetails.Add(employeeDept);
                }

                var projectDto = new GetAllProjectsDTO()
                {
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                    ProjectTotalBudget = project.TotalBudget,
                    ProjectHours = project.HoursBudget,
                    ProjectStatus = project.ProjectStatus.ToString(),
                    ProjectLocation = project.Location,
                    ProjectStartDate = project.StartDate,
                    ProjectEndDate = project.EndDate,
                    ProjectDescription = project.Description,
                    ProjectTasks = projectTaskWithIdDTOs,
                    EmployeesInProject = employeeDeptDetails,
                    ProjectPhases = projectPhaseDTOs,
                    ProjectAttendances = projectAttendanceDTOs
                };

                projectDTOs.Add(projectDto);
            }
            return Ok(projectDTOs);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        [AdminAccountantOnly]
        public async Task<ActionResult<ProjectDTO>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdCustomAsync(id);
            if (project == null)
            {
                return NotFound("Project no longer exist.");
            }

            List<ProjectPhaseDTO> projectPhaseDTOs = new List<ProjectPhaseDTO>();
            List<GetAttendancesInProjectDTO> projectAttendanceDTOs = new List<GetAttendancesInProjectDTO>();
            List<EmployeeDeptDetailsDTO> employeeDeptDetails = new List<EmployeeDeptDetailsDTO>();
            List<ProjectTaskWithIdDTO> projectTaskWithIdDTOs = new List<ProjectTaskWithIdDTO>();

            if (project.projectPhases != null)
            {
                foreach (var phase in project.projectPhases)
                {
                    ProjectPhaseDTO projectPhaseDTO = new ProjectPhaseDTO()
                    {
                        Id = phase.Id,
                        PhaseName = phase.Name.ToString(),
                        PhaseStartDate = phase.StartPhase,
                        PhaseEndDate = phase.EndPhase,
                        PhaseMilestone = phase.Milestone,
                        PhaseHrBudget = phase.HrBudget
                    };
                    projectPhaseDTOs.Add(projectPhaseDTO);
                }
            }


            if (project.projectTasks != null)
            {
                foreach (var task in project.projectTasks)
                {
                    ProjectTaskWithIdDTO projectTaskWithIdDTO = new ProjectTaskWithIdDTO()
                    {
                        Id = task.Id,
                        TaskName = task.Name,
                        TaskDescription = task.Description,
                        TotalHoursPerTask = task.ToltalHoursPerTask
                    };
                    projectTaskWithIdDTOs.Add(projectTaskWithIdDTO);
                }

            }
            if (project.Attendances != null)
            {
                foreach (var attendance in project.Attendances)
                {
                    GetAttendancesInProjectDTO projectAttendanceDTO = new GetAttendancesInProjectDTO()
                    {
                        EmployeeId = attendance.EmployeeId,
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
            }

            if (project.Employees != null)
            {
                foreach (var employee in project.Employees.ToList())
                {
                    EmployeeDeptDetailsDTO employeeDept = new EmployeeDeptDetailsDTO()
                    {
                        EmployeeId = employee.Employee.Id,
                        EmployeeFirstName = employee.Employee.FirstName,
                        EmployeeLastName = employee.Employee.LastName,
                        EmployeePosition = employee.Employee.ToString()
                    };
                    employeeDeptDetails.Add(employeeDept);
                }
            }

            var ProjectDto = new ProjectDTO
            {
                ProjectName = project.Name,
                ProjectTotalBudget = project.TotalBudget,
                ProjectHours = project.HoursBudget,
                ProjectStatus = project.ProjectStatus.ToString(),
                ProjectLocation = project.Location,
                ProjectStartDate = project.StartDate,
                ProjectEndDate = project.EndDate,
                ProjectDescription = project.Description,
                ProjectTasks = projectTaskWithIdDTOs,
                EmployeesInProject = employeeDeptDetails,
                ProjectPhases = projectPhaseDTOs,
                ProjectAttendances = projectAttendanceDTOs

            };

            return Ok(ProjectDto);
        }

        // POST: api/Projects
        [HttpPost]
        [AdminOnly]
        public async Task<ActionResult<Project>> CreateProject(CreateProjectDTO projectDTO)
        {
            if (ModelState.IsValid)
            {
                List<ProjectPhase> projectPhases = new List<ProjectPhase>();
                List<EmployeeProject> employeeProjects = new List<EmployeeProject>();
                foreach (ProjectPhaseWithNoIdDTO projectPhaseDTO in projectDTO.ProjectPhases)
                {
                    ProjectPhase projectPhase = _mapper.Map<ProjectPhaseWithNoIdDTO, ProjectPhase>(projectPhaseDTO);
                    projectPhases.Add(projectPhase);
                }
                Project project = _mapper.Map<CreateProjectDTO,Project>(projectDTO);

                Project createdProject = await _projectService.AddAsync(project);

                foreach (int EmployyeId in projectDTO.EmployeesInProjectIds)
                {
                    var employee = await _employeeService.GetByIdAsync(EmployyeId);

                    if (employee != null)
                    {
                        EmployeeProject employeeProject = new EmployeeProject()
                        {
                            EmployeeId = EmployyeId,
                            ProjectId = createdProject.Id
                        };
                        employeeProjects.Add(employeeProject);
                        await _employeeProjectsService.AddAsync(employeeProject);
                    }

                }
                project.Employees = employeeProjects;

                return Ok("project created successfully");
            }
            return BadRequest(ModelState);
        }


        // PUT: api/Projects/5 
        [HttpPut("{id}")]
        [AdminOnly]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectDTO projectDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var project = await _projectService.GetByIdAsync(id, p => p.projectPhases, e => e.Employees);

                if (project == null)
                    return NotFound("Project no longer exist.");

                List<EmployeeProject> employeeProjects = new List<EmployeeProject>();

                if (projectDTO.EmployeesInProjectIds.ToList().Count() > 0)
                {
                    var employeesInProject = await _employeeProjectsService.GetAllEmployeesCustom(project.Id);

                    foreach (var employeeProject in employeesInProject)
                    {

                        project.Employees.Remove(employeeProject);
                        await _employeeProjectsService.DeleteEmplyeeProjectCustom(employeeProject.ProjectId ,employeeProject.EmployeeId);
                    }

                    foreach (var EmployyeId in projectDTO.EmployeesInProjectIds)
                    {
                        var employee = await _employeeService.GetByIdAsync(EmployyeId);
                        if (employee != null)
                        {
                            EmployeeProject employeeProject = new EmployeeProject()
                            {
                                EmployeeId = EmployyeId,
                                ProjectId = project.Id
                            };
                            employeeProjects.Add(employeeProject);
                            await _employeeProjectsService.AddAsync(employeeProject);
                        }
                    }

                }

                project.Name = projectDTO.ProjectName;
                project.TotalBudget = projectDTO.ProjectTotalBudget;
                project.HoursBudget = projectDTO.ProjectHours;
                project.ProjectStatus = projectDTO.ProjectStatus;
                project.Location = projectDTO.ProjectLocation;
                project.StartDate = projectDTO.ProjectStartDate;
                project.EndDate = projectDTO.ProjectEndDate;
                project.Description = projectDTO.ProjectDescription;

                if (projectDTO.projectPhases.ToList().Count() > 0)
                {
                    var phasesInProject = await _projectPhaseService.GetAllprojectPhasesCustom(project.Id);

                    foreach (var projectPhase in phasesInProject)
                    {

                        project.projectPhases.Remove(projectPhase);
                        await _projectPhaseService.DeleteAsync(projectPhase.Id);
                    }

                    foreach (var phase in projectDTO.projectPhases.ToList())
                    {

                        ProjectPhase ProjectPhase = new ProjectPhase()
                        {
                            Name = phase.PhaseName,
                            EndPhase = phase.PhaseEndDate,
                            StartPhase = phase.PhaseStartDate,
                            HrBudget = phase.PhaseHrBudget,
                            Milestone = phase.PhaseMilestone,
                            ProjectId = project.Id,
                        };
                        project.projectPhases.Add(ProjectPhase);
                        await _projectPhaseService.AddAsync(ProjectPhase);

                    }

                }

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
        [AdminOnly]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectService.GetProjectByIdCustom2Async(id);
            if (project == null)
            {
                return NotFound("Project no longer exist.");
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
        [AdminAccountantOnly]
        public async Task<IActionResult> GetProjectTasksByProjectId(int projectId)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(projectId, t => t.projectTasks);

                if (project == null)
                    return NotFound("Project no longer exist.");
                List<GetProjectTasksByProjectIdDTO> dTOs = new List<GetProjectTasksByProjectIdDTO>();

                foreach (var projectTask in project.projectTasks.ToList())
                {
                    GetProjectTasksByProjectIdDTO dTO = new GetProjectTasksByProjectIdDTO()
                    {
                        Id = projectTask.Id,
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
        [AdminAccountantOnly]
        public async Task<IActionResult> GetProjectPhasesByProjectId(int projectId)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(projectId, p => p.projectPhases);

                if (project == null)
                    return NotFound("Project no longer exist.");

                List<GetProjectPhasesByProjectIdDTO> dTOs = new List<GetProjectPhasesByProjectIdDTO>();

                foreach (var projectPhase in project.projectPhases.ToList())
                {
                    GetProjectPhasesByProjectIdDTO dTO = new GetProjectPhasesByProjectIdDTO()
                    {
                        Id = projectPhase.Id,
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

        [HttpGet("GetProjectHoursAndTotalCost/{projectId}")]
        [AdminAccountantOnly]
        public async Task<IActionResult> GetProjectHoursAndTotalCost(int projectId)
        {
            var project = await _projectService.GetByIdAsync(projectId, p => p.Attendances);

            if (project == null)
            {
                return NotFound("Project not found.");
            }
            decimal totalCost = 0;

            foreach (var employee in project.Attendances)
            {
                var employeeDetails = await _employeeService.GetByIdAsync(employee.EmployeeId);

                if (employeeDetails != null)
                {
                    decimal totalCostPerEmployee = employeeDetails.CalculateSalaryPerProject(employee.HoursSpent);
                    if (totalCostPerEmployee != 0)
                    {
                        totalCost += totalCostPerEmployee;
                    }

                }
            }

            var projectHours = new
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                TotalHoursSpent = project.Attendances.Sum(a => a.HoursSpent),
                TotalCost = totalCost
            };

            return Ok(projectHours);
        }

        [HttpGet("GetProjectsHoursAndTotalCosts")]
        [AdminAccountantOnly]
        public async Task<IActionResult> GetProjectsHoursAndTotalCosts()
        {
            var projects = await _projectService.GetAllProjectsCustomAsync();

            var projectHours = projects.Select(project => new
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                TotalHoursSpent = project.Attendances.Sum(a => a.HoursSpent),
                TotalCost = project.Attendances.Where(p => p.ProjectId == project.Id).Sum(e => e.HoursSpent * e.Employee.SalaryPerHour)
            });

            return Ok(projectHours);
        }
    }
}
