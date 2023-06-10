﻿using Microsoft.AspNetCore.Mvc;
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
            List<ProjectPhaseDTO> projectPhaseDTOs = new List<ProjectPhaseDTO>();
            foreach (var project in projects)
            {
                projectPhaseDTOs.Clear();
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
                    Phases = projectPhaseDTOs
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
                        HrBudget = projectPhaseDTO.HrBudget
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
                    Description = projectDTO.ProjectLocation,
                    projectPhases = projectPhases
                };
                await _projectService.AddAsync(project);
                return Ok(projectPhases);
            }
                /*
                List<ProjectPhaseDTO> projectPhaseDTOs = new List<ProjectPhaseDTO>();
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
                    projectPhaseDTOs.Clear();
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
                    if (projectPhaseDTOs.Count != 0)
                    {
                        foreach (var phase in projectPhaseDTOs)
                        {
                            ProjectPhase projectPhase = new ProjectPhase()
                            {
                                Name = phase.Name,
                                StartPhase = phase.StartDate,
                                EndPhase = phase.EndDate,
                                Milestone = phase.Milestone,
                                HrBudget = phase.HrBudget
                            };
                            project.projectPhases.Add(projectPhase);
                        }
                    }

                    //foreach (var taskId in projectDTO.ProjectTasksIds)
                    //{
                    //    var task = await _projectTaskService.GetByIdAsync(taskId);
                    //    if (task != null)
                    //    {
                    //        project.projectTasks.Add(task);
                    //    }
                    //}

                    //foreach (var employeeProjectId in projectDTO.EmployeesInProjectIds)
                    //{
                    //    var employeeProject = await _employeeProjectService.GetByIdAsync(employeeProjectId);
                    //    if (employeeProject != null)
                    //    {
                    //        project.employeeProjects.Add(employeeProject);
                    //    }
                    //}

                    await _projectService.AddAsync(project);

                    //return CreatedAtAction("GetProject", new { id = project.Id }, project);
                    return Ok(project);
                }*/

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
