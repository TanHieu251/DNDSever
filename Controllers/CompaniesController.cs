using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DNDServer.Data;
using DNDServer.Model;
using DNDServer.DTO.Response;
using DNDServer.Repository.Company;
using DNDServer.DTO.Request;

namespace DNDServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        // GET: api/Companies
        [HttpGet("GetCompany")]
        public async Task<IActionResult> Getcompanies()
        {
            try
            {
                var company = await _companyRepo.GetCompanyAsync();
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy danh sách sản phẩm thành công",
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

        [HttpPost("UpdateCompany")]
        public async Task<IActionResult> UpdateCompanyAsync(DTOResCompany dTOResCompany)
        {
            if (dTOResCompany.Id <= 0)
            {
                return BadRequest(new DTOResponse
                {
                    IsSuccess = false,
                    Message = "ID không khớp với sản phẩm.",
                    Data = null
                });
            }

            try
            {
                await _companyRepo.UpdateCompanyAsync(dTOResCompany);
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Cập nhật sản phẩm thành công.",
                    Data = dTOResCompany
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
    }
}
