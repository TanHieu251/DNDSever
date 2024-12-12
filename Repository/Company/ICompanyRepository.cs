using DNDServer.DTO.Request;
using DNDServer.DTO.Response;

namespace DNDServer.Repository.Company
{
    public interface ICompanyRepository
    {
        Task UpdateCompanyAsync(DTOResCompany dtoCompany);
        Task <DTOResCompany> GetCompanyAsync ();

    }
}
