using DNDServer.Data;
using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using Microsoft.EntityFrameworkCore;

namespace DNDServer.Repository.New
{
    public class NewRepository : INewRepository
    {
        private readonly DNDContext _context;

        public NewRepository(DNDContext context) 
        
        {
            _context = context;
        }
        public async Task AddNewAsync(DTOResNew dTOResNew)
        {
            var addNew = new DNDServer.Model.New
            {
                Name = dTOResNew.Name,
                Description = dTOResNew.Description,
                Intro = dTOResNew.Intro,
                Img = dTOResNew.Img,
            };

            await _context.news.AddAsync(addNew);
            await _context.SaveChangesAsync();
        }

        public async Task<DTOResNew> DeleteNewAsync(int id)
        {
            var news = await _context.news.FindAsync(id);
            if (news != null)
            {
                _context.news.Remove(news); 
                await _context.SaveChangesAsync();

                var result = new DTOResNew
                {
                    Name = news.Name,
                    Description = news.Description,
                    Intro = news.Intro,
                    Img = news.Img,
                };

                return result; 
            }

            return null; 
        }

        public async Task<List<DTOResNew>> GetAllNewAsync()
        {
            return await _context.news
                .Select(p => new DTOResNew
                {
                    Id = p.Id,
                    Name= p.Name,
                    Description= p.Description,
                    Intro= p.Intro,
                    Img = p.Img,
                })
                .ToListAsync();
        }

        public async Task<DTOResNew> GetNewAsync(int id)
        {
            return await _context.news
                           .Where(p => p.Id == id)
                           .Select(p => new DTOResNew
                           {
                              Id = p.Id,
                              Name = p.Name,
                              Description=p.Description,
                              Intro=p.Intro,
                              Img=p.Img
                           })
                           .FirstOrDefaultAsync();
        }



        public async Task<DTOResNew> UpdateNewAsync(DTOResNew dto)
        {
            var news = await _context.news.FindAsync(dto.Id);
            if (news == null)
            {
                throw new KeyNotFoundException("Tin tức không tìm thấy."); 
            }

            news.Name = dto.Name;
            news.Description = dto.Description;
            news.Intro = dto.Intro;
            news.Img = dto.Img;

            _context.news.Update(news);
            await _context.SaveChangesAsync();

            var result = new DTOResNew
            {
                Id = news.Id,
                Name = news.Name,
                Description = news.Description,
                Intro = news.Intro,
                Img = news.Img,
            };

            return result; // Trả về thông tin đã cập nhật
        }
    }
}
