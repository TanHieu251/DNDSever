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



        // API ADD TYPE PRODUCT
        [HttpPost("AddTypeProduct")]
        public async Task<IActionResult> AddTypeProduct(DTOTypeProduct model)
        {
            try
            {
                // Call the repository method to add the type product
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

        // PUT: api/TypeProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeProduct(int id, TypeProduct typeProduct)
        {
            if (id != typeProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeProductExists(id))
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
        [HttpGet("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeProduct(int id)
        {
            try
            {
                var response = await _typeProductRepo.DeleteTypeProductAsync(id);
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

        private bool TypeProductExists(int id)
        {
            return _context.TypeProducts.Any(e => e.Id == id);
        }
    }
}
