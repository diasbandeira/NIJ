using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Data.DAL.Cadastros
{
    public class ActivityDAL
    {
        private IESContext _context;
        public ActivityDAL(IESContext context)
        {
            _context = context;
        }

        public IQueryable<Activity> GetActivitiesByName()
        {
            return _context.Activities.Include(i => i.Project).OrderBy(a => a.Description);
        }

        public async Task<Activity> GetActivityById(long? id)
        {
            var activity = await _context.Activities.SingleOrDefaultAsync(a => a.ActivityId == id);
            _context.Projects.Where(p => activity.ProjectId == p.ProjectId).Load();
            
            return activity;
        }

        public async Task<Activity> SaveActivity(Activity activity)
        {
            if(activity.ActivityId == null)
            {
                _context.Activities.Add(activity);
            }
            else
            {
                _context.Activities.Update(activity);

            }

            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<Activity> DeleteActivity(long? id)
        {
            Activity activity = await GetActivityById(id);

            await _context.SaveChangesAsync();

            return activity;
        }
    }
}
