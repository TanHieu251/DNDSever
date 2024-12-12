using DNDServer.DTO.Response;
using DNDServer.Data;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Repository.Services
{
    public class ServicesRepository :IServicesRepository
    {
        private readonly DNDContext _context;

        public ServicesRepository(DNDContext contex)
        {
            _context = contex;
        }

        public async Task<List<DTOResServices>> GetServicesAsync()
        {
            var service = await _context.servicesdb
                    .Select(c => new DTOResServices
                    {
                        Id = c.Id,
                        Name=c.Name,
                        Description=c.Description,
                        Characteristic=c.Characteristic,
                        Process =c.Process
                    })
                    .ToListAsync();

            return service;
        }
    
    }
}
