using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DNDServer.Data;
using DNDServer.Model;
using DNDServer.Repository.New;
using DNDServer.DTO.Response;
using DNDServer.DTO.Request;

namespace DNDServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewRepository _newRepo;

        public NewsController(INewRepository newRepo)
        {
            _newRepo = newRepo;
        }

        [HttpGet("GetAllNew")]
        public async Task<IActionResult> GetAllNew()
        {
            try
            {
                var company = await _newRepo.GetAllNewAsync();
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy danh sách tin tức thành công",
                    Data = company
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

        // API GET NEW BY ID
        [HttpPost("GetNew")]
        public async Task<IActionResult> GetNew(int id)
        {
            try
            {
                var product = await _newRepo.GetNewAsync(id);
                if (product == null)
                {
                    return NotFound(new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "Tin tức không tồn tại.",
                        Data = null
                    });
                }
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy tin tức thành công",
                    Data = product
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

        // API ADD NEW
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew(DTOResNew dTOResNew)
        {
            try
            {
                await _newRepo.AddNewAsync(dTOResNew);
                return CreatedAtAction(nameof(GetNew), new { id = dTOResNew.Id }, new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Tin tức đã được thêm thành công.",
                    Data = dTOResNew
                });
            }
            catch (DbUpdateException dbEx) // Catch database update exceptions specifically
            {
                // Log the inner exception details
                var innerException = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;

                return StatusCode(500, new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {innerException}",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                // Log general exceptions
                return StatusCode(500, new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                });
            }
        }

        // API UPDATE NEW
        [HttpPost("UpdateNew")]
        public async Task<IActionResult> UpdateNew(DTOResNew dTOResNew)
        {
            if (dTOResNew.Id <= 0)
            {
                return BadRequest(new DTOResponse
                {
                    IsSuccess = false,
                    Message = "ID không khớp với tin tức.",
                    Data = null
                });
            }

            try
            {
                await _newRepo.UpdateNewAsync(dTOResNew);
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Cập nhật tin tức thành công.",
                    Data = dTOResNew
                });
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new DTOResponse
                {
                    IsSuccess = false,
                    Message = knfEx.Message,
                    Data = null
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

        // API DELETE PRODUCT
        [HttpPost("DeleteNew")]
        public async Task<IActionResult> DeleteNew(int id)
        {
            try
            {
                await _newRepo.DeleteNewAsync(id);
                return Ok(new DTOResponse { IsSuccess = false, Message = "Xoá tin tức thành công", Data = null });
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
