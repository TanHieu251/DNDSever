using DNDServer.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Repository.New
{
    public interface INewRepository
    {
        public Task<List<DTOResNew>> GetAllNewAsync();
        public Task<DTOResNew> GetNewAsync(int id);
        public Task AddNewAsync(DTOResNew dTOResNew);

        public Task<DTOResNew> UpdateNewAsync(DTOResNew dto);
        public Task<DTOResNew> DeleteNewAsync(int id);

    }
}
