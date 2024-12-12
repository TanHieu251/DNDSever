using DNDServer.DTO.Response;

namespace DNDServer.Repository.Services
{
    public interface IServicesRepository
    {
        Task<List<DTOResServices>> GetServicesAsync();

    }
}
