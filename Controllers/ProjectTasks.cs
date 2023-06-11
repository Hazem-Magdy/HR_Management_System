﻿using HR_Management_System.DTO;
using HR_Management_System.DTO.ProjectTask;
using HR_Management_System.Models;
using HR_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management_System.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> CreateProjectTask(ProjectTaskDTO projectTaskDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to the ProjectTask model
            var projectTask = new ProjectTask
            {
                Name = projectTaskDTO.Name,
                Description = projectTaskDTO.Description,
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
        public async Task<IActionResult> GetProjectTaskById(int id)
        {
            var projectTask = await _projectTaskService.GetByIdAsync(id, p=>p.Project);

            if (projectTask== null)
                return NotFound();
            var TaskWhithProjectNameDTO = new TaskWhithProjectNameDTO()
            {
                Name = projectTask.Name,
                Description = projectTask.Description,
                TotalHoursPerTask = projectTask.ToltalHoursPerTask,
                projectName = projectTask.Project.Name
            };

            return Ok(TaskWhithProjectNameDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjectTask(int id, UpdateProjectTaskDTO projectTaskDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var projectTask = await _projectTaskService.GetByIdAsync(id);

            if (projectTask == null)
                return NotFound();

            // Update the project task properties
            projectTask.Name = projectTaskDTO.Name;
            projectTask.Description = projectTaskDTO.Description;
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
        public async Task<IActionResult> DeleteProjectTask(int id)
        {
            var projectTask = await _projectTaskService.GetByIdAsync(id);

            if (projectTask == null)
                return NotFound();

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
