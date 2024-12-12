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
using DNDServer.Repository.Product;

namespace DNDServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProductsController : ControllerBase
    {
        private readonly DNDContext _context;
        private readonly ITypeProductRepository _typeProductRepo;

        public TypeProductsController(DNDContext context, ITypeProductRepository typeProductRepository)
        {
            _context = context;
            _typeProductRepo=typeProductRepository;
        }



        // API ADD TYPE Product
        [HttpPost("AddTypeProduct")]
        public async Task<IActionResult> AddTypeProduct(DTOTypeProduct model)
        {
            try
            {
                // Call the repository method to add the type Product
                DTOResponse response = await _typeProductRepo.AddTypeProductAsync(model);

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


        [HttpPost("UpdateTypeProduct")]
        public async Task<IActionResult> UpdateTypeProduct(DTOTypeProduct model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest(new DTOResponse
                {
                    IsSuccess = false,
                    Message = "Model không hợp lệ.",
                    Data = null
                });
            }

            var response = await _typeProductRepo.UpdateTypeProductAsync(model);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return StatusCode(500, response);
        }


        [HttpPost("GetAllTypeProduct")]
        public async Task<IActionResult> GetAllTypeProducts()
        {
            try
            {
                var typeProducts = await _typeProductRepo.GetAllTypeProductAsync();
                return Ok(typeProducts);
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

        // GET: api/TypeProducts/5
        [HttpPost("GetTypeProduct")]
        public async Task<IActionResult> GetTypeProductById(int id)
        {
            try
            {
                var typeProduct = await _typeProductRepo.GetTypeProductAsync(id);
                if (typeProduct == null)
                {
                    return NotFound();
                }
                return Ok(typeProduct);
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

        [HttpPost("DeleteTypeProduct")]
        public async Task<IActionResult> DeleteTypeProduct(int id)
        {
            try
            {
                var response = await _typeProductRepo.DeleteTypeProductAsync(id);
                if (response.IsSuccess)
                {
                    return Ok(new DTOResponse
                    {
                        IsSuccess = true,
                        Message = "Xoá sản phẩm thành công",

                    });
                }
                else
                {
                    return Ok(new DTOResponse
                    {
                        IsSuccess = true,
                        Message = "Xoá loại dự án thành công, không tìm thấy",

                    });
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



        private bool TypeProductExists(int id)
        {
            return _context.TypeProducts.Any(e => e.Id == id);
        }
    }
}
