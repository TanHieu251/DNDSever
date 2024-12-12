using DNDServer.Data;
using DNDServer.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Repository.Company
{
    public class CompanyRepository :ICompanyRepository
    {
        private readonly DNDContext _context;

        public CompanyRepository(DNDContext context) {
            _context = context;
        }



        public async Task UpdateCompanyAsync(DTOResCompany dtoCompany)
        {
            if (dtoCompany == null)
            {
                throw new ArgumentNullException(nameof(dtoCompany), "Company DTO cannot be null");
            }

            var company = await _context.companies.FindAsync(dtoCompany.Id);

            if (company == null)
            {
                throw new KeyNotFoundException($"Company with ID {dtoCompany.Id} not found");
            }
            
            company.About = dtoCompany.About;
            company.Description = dtoCompany.Description;
            company.CommitQuality = dtoCompany.CommitQuality;
            company.CoreValues = dtoCompany.CoreValues;
            company.Customer = dtoCompany.Customer;
            company.Field = dtoCompany.Field;
            company.VisionMission = dtoCompany.VisionMission;
            company.Target = dtoCompany.Target;
            company.Name = dtoCompany.Name;
            company.Member = dtoCompany.Member;

            await _context.SaveChangesAsync();
        }

        public async Task<DTOResCompany> GetCompanyAsync()
        {
          
                var company = await _context.companies
                    .Select(c => new DTOResCompany
                    {
                        Id = c.Id,
                        CommitQuality = c.CommitQuality,
                        CoreValues = c.CoreValues,
                        Customer = c.Customer,
                        Field = c.Field,
                        VisionMission = c.VisionMission,
                        Target = c.Target,
                        Name = c.Name,
                        Member = c.Member,
                        Description=c.Description,
                        About=c.About
                    })
                    .FirstOrDefaultAsync();

                return company;
            }
           
        }
    }

