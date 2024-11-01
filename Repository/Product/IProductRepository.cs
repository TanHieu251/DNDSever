using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNDServer.Repository.Product
{
    public interface IProductRepository
    {
        Task<List<DTOResProduct>> GetAllProductsAsync();
        Task<DTOResProduct> GetProductByIdAsync(int id);
        Task AddProductAsync(DTOProduct dtoProduct);
        Task UpdateProductAsync(DTOProduct dtoProduct);
        Task DeleteProductAsync(int id);
    }
}