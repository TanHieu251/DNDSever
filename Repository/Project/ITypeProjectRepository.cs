using DNDServer.DTO.Request;
using DNDServer.DTO.Response;

namespace DNDServer.Repository.Project
{
    public interface ITypeProjectRepository
    {
        public Task<DTOResponse> AddTypeProjectAsync(DTOTypeProject model);
        public Task<DTOResponse> UpdateTypeProjectAsync(DTOTypeProject model);
        public Task<DTOResponse> DeleteTypeProjectAsync(int id);
        public Task<DTOResponse> GetAllTypeProjectAsync();
        public Task<DTOResponse> GetTypeProjectAsync(int id);
        
    }
}
