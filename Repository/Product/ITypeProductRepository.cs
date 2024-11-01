using DNDServer.DTO.Request;
using DNDServer.DTO.Response;

namespace DNDServer.Repository.Product
{
    public interface ITypeProductRepository
    {
        public Task<DTOResponse> AddTypeProductAsync(DTOTypeProduct model);
        public Task<DTOResponse> UpdateTypeProductAsync(DTOTypeProduct model);
        public Task<DTOResponse> DeleteTypeProductAsync(int id);
        public Task<DTOResponse> GetAllTypeProductAsync();
        public Task<DTOResponse> GetTypeProductAsync(int id);
    }
}
