using Microsoft.AspNetCore.Mvc;
using NIJ.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Controllers
{
    public class ActivityController : Controller
    {
        private static IList<Activity> activities = new List<Activity>()
        {
            new Activity()
            {
                ActivityId = 1,
                Description = "Atividade 2",
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddHours(1),
                Status = Status.Started
            },
            new Activity()
            {
                ActivityId = 2,
                Description = "Atividade 3",
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddHours(2),
                Status = Status.Pause
            },
            new Activity()
            {
                ActivityId = 3,
                Description = "Atividade 3",
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddHours(3),
                Status = Status.Ended
            },
            new Activity()
            {
                ActivityId = 4,
                Description = "Atividade 4",
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddHours(4),
                Status = Status.Deleted
            }
        };
        public IActionResult Index()
        {
            return View(activities);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Activity activity)
        {
            activities.Add(activity);
            activity.ActivityId = activities.Select(a => a.ActivityId).Max() + 1;

            return RedirectToAction("Index");

        }

        //Get
        public ActionResult Edit(long id)
        {
            return View(activities.Where(a => a.ActivityId == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity activity)
        {
            activities.Remove(activities.Where(a => a.ActivityId == activity.ActivityId).First());
            activities.Add(activity);

            return RedirectToAction("Index");
        }

        //Get
        public ActionResult Details(long id)
        {
            return View(activities.Where(i => i.ActivityId == id).First());
        }

        //GET
        public ActionResult Delete(long id)
        {
            return View(activities.Where(i => i.ActivityId == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Activity activity)
        {
            activities.Remove(activities.Where(i => i.ActivityId == activity.ActivityId).First());
            return RedirectToAction("Index");
        }
    }
}
