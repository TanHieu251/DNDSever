using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DNDServer.Data;
using DNDServer.Model;
using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using DNDServer.Repository.Project;

namespace DNDServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectsController(IProjectRepository projectRepository)
        {
            _projectRepo = projectRepository;
        }

        // API GET ALL PROJECTS
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectRepo.GetAllProjectsAsync();
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy danh sách dự án thành công",
                    Data = projects
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }

        // API GET PROJECT BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                var project = await _projectRepo.GetProjectByIdAsync(id);
                if (project == null)
                {
                    return NotFound(new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "Dự án không tồn tại.",
                        Data = null
                    });
                }
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy dự án thành công",
                    Data = project
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }

        // API ADD PROJECT
        [HttpPost]
        public async Task<IActionResult> AddProject(DTOProject dtoProject)
        {
            try
            {
                await _projectRepo.AddProjectAsync(dtoProject);
                return CreatedAtAction(nameof(GetProjectById), new { id = dtoProject.Id }, new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Dự án đã được thêm thành công.",
                    Data = dtoProject
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }

        // API UPDATE PROJECT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, DTOProject dtoProject)
        {
            if (id != dtoProject.Id)
            {
                return BadRequest(new DTOResponse
                {
                    IsSuccess = false,
                    Message = "ID không khớp với dự án.",
                    Data = null
                });
            }

            try
            {
                await _projectRepo.UpdateProjectAsync(dtoProject);
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Cập nhật dự án thành công.",
                    Data = dtoProject
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }

        // API DELETE PROJECT
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectRepo.DeleteProjectAsync(id);
                return NoContent(); // Trả về NoContent nếu xóa thành công
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}