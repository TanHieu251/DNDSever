using DNDServer.Data;
using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using DNDServer.Helpers;
using DNDServer.Model;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Repository.Project
{
    public class TypeProjectRepository : ITypeProjectRepository
    {
        private readonly DNDContext _context;

        public TypeProjectRepository(DNDContext context)
        {
            _context = context;
        }



        //THEM LOAI DU AN
        public async Task<DTOResponse> AddTypeProjectAsync(DTOTypeProject model)
        {
            try
            {
                // Validate the model
                if (model == null || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "Model không hợp lệ.",
                        Data = null
                    };
                }

                var typeProjectEntity = new TypeProject
                {
                    Name = model.Name
                };

                await _context.TypeProjects.AddAsync(typeProjectEntity);
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
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";

                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi khi thêm sản phẩm loại: {ex.Message}. Inner exception: {innerExceptionMessage}",
                    Data = null
                };
            }
        }

        //DELETE
        public async Task<DTOResponse> DeleteTypeProjectAsync(int id)
        {
            try
            {
                var existingTypeProject = await _context.TypeProjects.FindAsync(id);

                if (existingTypeProject != null)
                {
                    _context.TypeProjects.Remove(existingTypeProject);
                    await _context.SaveChangesAsync();

                    return new DTOResponse
                    {
                        IsSuccess = true,
                        Message = $"Xoá danh mục sản phẩm thành công",
                    };
                }
                else
                {
                    return new DTOResponse
                    {
                        IsSuccess = false,
                        Message = $"Danh mục cần xoá không tìm thấy ",
                    };
                }
            }
            catch (Exception ex)
            {
                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Xoá danh mục sản phẩm không thành công",
                };
            }
        }


        //GET ALL

        public async Task<DTOResponse> GetAllTypeProjectAsync()
        {
            try
            {
                var typeProjects = await _context.TypeProjects.ToListAsync();

                var dtoTypeProjects = typeProjects.Select(tp => new DTOTypeProject
                {
                    Id = tp.Id,
                    Name = tp.Name ?? "Unnamed" // Provide a default value if Name is null
                }).ToList();

                return new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Lấy danh sách danh mục sản phẩm thành công",
                    Data = dtoTypeProjects
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
        public async Task<DTOResponse> GetTypeProjectAsync(int id)
        {
            try
            {
                var typeProject = await _context.TypeProjects.FindAsync(id);

                if (typeProject != null)
                {
                    var dtoTypeProject = new DTOTypeProject
                    {
                        Id = typeProject.Id,
                        Name= typeProject.Name 
                    };

                    return new DTOResponse
                    {
                        IsSuccess = true,
                        Message = "TypeProject retrieved successfully",
                        Data = dtoTypeProject
                    };
                }
                else
                {
                    return new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "TypeProject not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new DTOResponse
                {
                    IsSuccess = false,
                    Message = $"Error retrieving TypeProject: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<DTOResponse> UpdateTypeProjectAsync(DTOTypeProject model)
        {
            try
            {
                // Find the type project in the database
                var existingTypeProject = await _context.TypeProjects.FindAsync(model.Id);
                if (existingTypeProject == null)
                {
                    return new DTOResponse
                    {
                        IsSuccess = false,
                        Message = "Sản phẩm loại không tồn tại.",
                        Data = null
                    };
                }

                // Update the properties
                existingTypeProject.Name = model.Name;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return new DTOResponse
                {
                    IsSuccess = true,
                    Message = "Cập nhật sản phẩm loại thành công!",
                    Data = existingTypeProject
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
    }

}

