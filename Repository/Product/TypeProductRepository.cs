using DNDServer.Data;
using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using DNDServer.Helpers;
using DNDServer.Model;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Repository.Product
{
    public class TypeProductRepository : ITypeProductRepository
    {
        private readonly DNDContext _context;

        public TypeProductRepository(DNDContext context)
        {
            _context = context;
        }


        public async Task<DTOResponse> AddTypeProductAsync(DTOTypeProduct model)
        {
            try
            {
                var typeProductEntity = new TypeProduct
                {
                    Name = model.Name
                };

                await _context.TypeProducts.AddAsync(typeProductEntity);
                await _context.SaveChangesAsync();

                return new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Thêm loại sản phẩm thành công!",
                    Data = model
                };
            }
            catch (Exception ex)
            {
                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi khi thêm sản phẩm loại: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<DTOResponse> UpdateTypeProductAsync(DTOTypeProduct model)
        {
            try
            {
                // Tìm sản phẩm loại trong cơ sở dữ liệu
                var existingTypeProduct = await _context.TypeProducts.FindAsync(model.Id);
                if (existingTypeProduct == null)
                {
                    return new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "Sản phẩm loại không tồn tại.",
                        Data = null
                    };
                }

                // Cập nhật các thuộc tính
                existingTypeProduct.Name = model.Name;

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                return new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Cập nhật sản phẩm loại thành công!",
                    Data = existingTypeProduct
                };
            }
            catch (Exception ex)
            {
                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi khi cập nhật sản phẩm loại: {ex.Message}",
                    Data = null
                };
            }
        }

        //DELETE
        public async Task<DTOResponse> DeleteTypeProductAsync(int id)
        {
            try
            {
                var existingTypeProduct = await _context.TypeProducts.FindAsync(id);

                if (existingTypeProduct != null)
                {
                    _context.TypeProducts.Remove(existingTypeProduct);
                    await _context.SaveChangesAsync();

                    return new DTOResponse
                    {
                        IsSuccess = true,
                        Message = "Xoá danh mục sản phẩm thành công"
                    };
                }
                else
                {
                    return new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "Danh mục cần xoá không tìm thấy"
                    };
                }
            }
            catch (Exception ex)
            {
                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = "Xoá danh mục sản phẩm không thành công: " + ex.Message
                };
            }
        }


        //GET ALL

        public async Task<DTOResponse> GetAllTypeProductAsync()
        {
            try
            {
                var typeProducts = await _context.TypeProducts.ToListAsync();

                var dtoTypeProducts = typeProducts.Select(tp => new DTOTypeProduct
                {
                    Id = tp.Id,
                }).ToList();

                return new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy danh sách danh mục sản phẩm thành công",
                    Data = dtoTypeProducts
                };
            }
            catch (Exception ex)
            {
                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi khi lấy danh sách: {ex.Message}",
                    Data = null
                };
            }
        }

        /// GET BY ID 
        public async Task<DTOResponse> GetTypeProductAsync(int id)
        {
            try
            {
                var typeProduct = await _context.TypeProducts.FindAsync(id);

                if (typeProduct != null)
                {
                    var dtoTypeProduct = new DTOTypeProduct
                    {
                        Id = typeProduct.Id,
                    };

                    return new DTOResponse
                    {
                        IsSuccess = true,
                        Message = "TypeProduct retrieved successfully",
                        Data = dtoTypeProduct
                    };
                }
                else
                {
                    return new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "TypeProduct not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Error retrieving TypeProduct: {ex.Message}",
                    Data = null
                };
            }
        }


    }

}

