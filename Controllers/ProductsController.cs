using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using DNDServer.Repository.Product;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepo;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        // API GET ALL PRODUCTS
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productRepo.GetAllProductsAsync();
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy danh sách sản phẩm thành công",
                    Data = products
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

        // API GET PRODUCT BY ID
        [HttpPost("GetProduct")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productRepo.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "Sản phẩm không tồn tại.",
                        Data = null
                    });
                }
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy sản phẩm thành công",
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

        // API ADD PRODUCT
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(DTOProduct dtoProduct)
        {
            try
            {
                await _productRepo.AddProductAsync(dtoProduct);
                return CreatedAtAction(nameof(GetProductById), new { id = dtoProduct.Id }, new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Sản phẩm đã được thêm thành công.",
                    Data = dtoProduct
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

        // API UPDATE PRODUCT
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(DTOProduct dtoProduct)
        {
            if (dtoProduct.Id <= 0) 
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
                await _productRepo.UpdateProductAsync(dtoProduct);
                return Ok(new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Cập nhật sản phẩm thành công.",
                    Data = dtoProduct
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
        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepo.DeleteProductAsync(id);
                return Ok(new DTOResponse { IsSuccess = false,Message= "Xoá sản phẩm thành công",Data=null});
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