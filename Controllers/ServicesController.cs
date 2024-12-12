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
using DNDServer.Repository.Services;

namespace DNDServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            servicesRepository = servicesRepository;
        }

        // GET: api/Services
        [HttpGet("GetService")]
        public async Task<ActionResult<IEnumerable<Services>>> Getservicesdb()
        {
            try
            {
                var company = await servicesRepository.GetServicesAsync();
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy danh sách dịch vụ thành công",
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


    }
}
