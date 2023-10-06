using Microsoft.EntityFrameworkCore;
using Modelo.Cadastros;
using System;
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

        public void RegistrarUsuario(long activityId, long userId)
        {
            var activity = _context.Activities.Where(a => a.ActivityId == activityId).Include(au => au.ActivitiesUsers).First();
            var user = _context.Users.Find(userId);
            activity.ActivitiesUsers.Add(new ActivityUser() { Activity = activity, User = user });

            _context.SaveChanges();
        }

        public IQueryable<User> GetUsersWithoutActivities(long? activityId)
        {
            var activity = _context.Activities.Where(a => a.ActivityId == activityId).Include(au => au.ActivitiesUsers).First();
            var activitiesUsers = activity.ActivitiesUsers.Select(au => au.UserId).ToArray();
            var activitiesWhitoutUsers = _context.Users.Where(u => !activitiesUsers.Contains(u.UserId));
            return activitiesWhitoutUsers;

        }
    }
}
