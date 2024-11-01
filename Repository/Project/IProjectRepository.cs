using DNDServer.DTO.Request;
using DNDServer.DTO.Response;

namespace DNDServer.Repository.Project
{
    public interface IProjectRepository
    {
        Task<List<DTOResProject>> GetAllProjectsAsync();
        Task<DTOResProject> GetProjectByIdAsync(int id);
        Task AddProjectAsync(DTOProject dtoProject);
        Task UpdateProjectAsync(DTOProject dtoProject);
        Task DeleteProjectAsync(int id);
    }
}
