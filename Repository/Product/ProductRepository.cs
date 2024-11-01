using DNDServer.Data;
using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using DNDServer.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNDServer.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly DNDContext _context;

        public ProductRepository(DNDContext context)
        {
            _context = context;
        }

        // Lấy tất cả sản phẩm
        public async Task<List<DTOResProduct>> GetAllProductsAsync()
        {
            return await _context.Products
                .Select(p => new DTOResProduct
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Description = p.Description,
                    Feature = p.Feature,
                    Specfication = p.Specfication,
                    Review = p.Review,
                    Status = p.Status,
                    StatusName = p.StatusName,
                    TypeData = p.TypeData,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ListImages = _context.ImgProducts.Where(i => i.ProductId == p.Id).ToList(),
                })
                .ToListAsync();
        }

        // Lấy sản phẩm theo ID
        public async Task<DTOResProduct> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new DTOResProduct
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Description = p.Description,
                    Feature = p.Feature,
                    Specfication = p.Specfication,
                    Review = p.Review,
                    Status = p.Status,
                    StatusName = p.StatusName,
                    TypeData = p.TypeData,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ListImages = _context.ImgProducts.Where(i => i.ProductId == p.Id).ToList(),
                })
                .FirstOrDefaultAsync();
        }

        // Thêm sản phẩm mới
        public async Task AddProductAsync(DTOProduct dtoProduct)
        {
            var product = new DNDServer.Model.Product
            {
                Code = dtoProduct.Code,
                Name = dtoProduct.Name,
                Description = dtoProduct.Description,
                Feature = dtoProduct.Feature,
                Specfication = dtoProduct.Specfication,
                Review = dtoProduct.Review,
                Status = dtoProduct.Status,
                StatusName = dtoProduct.StatusName,
                TypeData = dtoProduct.TypeData,
                Price = dtoProduct.Price,
                StockQuantity = dtoProduct.StockQuantity
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        // Cập nhật sản phẩm
        public async Task UpdateProductAsync(DTOProduct dtoProduct)
        {
            var product = await _context.Products.FindAsync(dtoProduct.Id);
            if (product != null)
            {
                product.Code = dtoProduct.Code;
                product.Name = dtoProduct.Name;
                product.Description = dtoProduct.Description;
                product.Feature = dtoProduct.Feature;
                product.Specfication = dtoProduct.Specfication;
                product.Review = dtoProduct.Review;
                product.Status = dtoProduct.Status;
                product.StatusName = dtoProduct.StatusName;
                product.TypeData = dtoProduct.TypeData;
                product.Price = dtoProduct.Price;
                product.StockQuantity = dtoProduct.StockQuantity;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        // Xóa sản phẩm
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}