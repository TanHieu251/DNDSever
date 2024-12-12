using AutoMapper.Features;
using DNDServer.Data;
using DNDServer.DTO.Request;
using DNDServer.DTO.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DNDServer.Repository.Project
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DNDContext _context;

        public ProjectRepository(DNDContext context)
        {
            _context = context;
        }

        public async Task<List<DTOResProject>> GetAllProjectsAsync()
        {
            return await _context.Projects
               .Select(p => new DTOResProject
               {
                   Id = p.Id,
                   Code = p.Code,
                   Name = p.Name,
                   Description = p.Description,
                   Feature = p.Feature,
                   DateStart = p.DateStart,
                   DateEnd= p.DateEnd,
                   Status = p.Status,
                   StatusName = p.StatusName,
                   TypeData = p.TypeData,
                   ListImages = _context.ImgProjects.Where(i => i.ProjectId == p.Id).ToList(),
               })
                .ToListAsync();
        }

        public async Task<DTOResProject> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
                .Where(p => p.Id == id)
                .Select(p => new DTOResProject
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Description = p.Description,
                    Feature = p.Feature,
                    DateStart = p.DateStart, 
                    DateEnd = p.DateEnd, 
                    Status = p.Status,
                    StatusName = p.StatusName,
                    TypeData = p.TypeData,
                    ListImages = _context.ImgProjects
                        .Where(i => i.ProjectId == p.Id)
                        .ToList() 
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddProjectAsync(DTOProject dtoProject)
        {
            if (dtoProject == null) throw new ArgumentNullException(nameof(dtoProject));

            var project = new DNDServer.Model.Project
            {
                Code = dtoProject.Code,
                Name = dtoProject.Name,
                Description = dtoProject.Description,
                Feature = dtoProject.Feature,
                DateStart = dtoProject.DateStart,
                DateEnd = dtoProject.DateEnd,
                Status = dtoProject.Status,
                StatusName = dtoProject.StatusName,
                TypeData = dtoProject.TypeData,
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(DTOProject dtoProject)
        {
            if (dtoProject == null) throw new ArgumentNullException(nameof(dtoProject));

            var project = await _context.Projects.FindAsync(dtoProject.Id);
            if (project == null)
            {
                throw new KeyNotFoundException("Project not found."); 
            }

            // Update the project properties
            project.Code = dtoProject.Code;
            project.Name = dtoProject.Name;
            project.Description = dtoProject.Description;
            project.Feature = dtoProject.Feature;
            project.DateStart = dtoProject.DateStart;
            project.DateEnd = dtoProject.DateEnd;
            project.Status = dtoProject.Status;
            project.StatusName = dtoProject.StatusName;
            project.TypeData = dtoProject.TypeData;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}