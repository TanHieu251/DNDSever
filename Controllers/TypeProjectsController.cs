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
    public class TypeProjectsController : ControllerBase
    {
        private readonly DNDContext _context;
        private readonly ITypeProjectRepository _typeProjectRepo;

        public TypeProjectsController(DNDContext context, ITypeProjectRepository typeProjectRepository)
        {
            _context = context;
            _typeProjectRepo = typeProjectRepository;
        }



        // API ADD TYPE Project
        [HttpPost("AddTypeProject")]
        public async Task<IActionResult> AddTypeProject(DTOTypeProject model)
        {
            try
            {
                // Call the repository method to add the type Project
                DTOResponse response = await _typeProjectRepo.AddTypeProjectAsync(model);

                if (response.IsSuccess == true)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeProject(int id, TypeProject typeProject)
        {
            if (id != typeProject.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeProjectExists(id))
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


        [HttpGet]
        public async Task<IActionResult> GetAllTypeProjects()
        {
            try
            {
                var typeProjects = await _typeProjectRepo.GetAllTypeProjectAsync();
                return Ok(typeProjects);
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

        // GET: api/TypeProjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeProjectById(int id)
        {
            try
            {
                var typeProject = await _typeProjectRepo.GetTypeProjectAsync(id);
                if (typeProject == null)
                {
                    return NotFound();
                }
                return Ok(typeProject);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeProject(int id)
        {
            try
            {
                var response = await _typeProjectRepo.DeleteTypeProjectAsync(id);
                if (response.IsSuccess)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(response);
                }
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



        private bool TypeProjectExists(int id)
        {
            return _context.TypeProjects.Any(e => e.Id == id);
        }
    }
}