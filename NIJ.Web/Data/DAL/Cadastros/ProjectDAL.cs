using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Data.DAL.Cadastros
{
    public class ProjectDAL
    {
        private IESContext _context;
        public ProjectDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Project> GetProjectsOrderByName()
        {
            return _context.Projects.OrderBy(p => p.Name);
        }

        public async Task<Project> GetProjectById(long id)
        {
            return await _context.Projects.Include(p => p.Activities).SingleOrDefaultAsync(m => m.ProjectId == id);
        }

        public async Task<Project> SaveProject(Project project)
        {
            if(project.ProjectId == null)
            {
                _context.Projects.Add(project);
            }
            else
            {
                _context.Update(project);
            }

            await _context.SaveChangesAsync();
            
            return project;
        }
        public async Task<Project> RemoveProjectById(long id)
        {
            Project project = await GetProjectById(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
