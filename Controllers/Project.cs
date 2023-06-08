using Microsoft.AspNetCore.Mvc;
using HR_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using HR_Management_System.Services;

namespace HR_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService )
        {
            _projectService = projectService;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            return await _projectService.GetAllAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _projectService.GetByIDAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject(Project project)
        {
            await _projectService.AddAsync(project);

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            try
            {
                await _projectService.UpdateAsync(id, project);
            }
            catch (DbUpdateConcurrencyException)
            {

                bool projectExists = _projectService.ProjectExists(id);
                if (!projectExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

            await  _projectService.DeleteAsync(id);


            return NoContent();
        }

        [HttpGet("api/ProjectExists/id")]
        public bool ProjectExists(int id)
        {
            return _projectService.ProjectExists(id);
        }
    }
}
