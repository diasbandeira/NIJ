using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIJ.Web.Data;
using NIJ.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IESContext _context;

        public ActivityController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var activities = await _context.Activities.Include(i => i.Project).OrderBy(a => a.ActivityId).ToListAsync();
            return View(activities);
        }

        //GET: Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description", "StartedAt", "EndedAt", "Status")] Activity activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(activity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel inserir os dados");
            }

            return View(activity);            

        }

        //Get
        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.Where(a => a.ActivityId == id).SingleOrDefaultAsync();

            if(activity == null)
            {
                return NotFound();
            }
            return View(activity);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ActivityId", "Description", "StartedAt", "EndedAt", "Status")] Activity activity)
        {
            
            if(activity.ActivityId != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExist(activity.ActivityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }                    
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(activity);
            
        }

        //Get
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return NotFound();

            var activity = await _context.Activities.Where(a => a.ActivityId == id).SingleOrDefaultAsync();
            
            if (activity == null)
                return NotFound();

            return View(activity);
        }

        //Get: Activity/Delete/{id}
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return NotFound();
            
            var activity = await _context.Activities.Where(a => a.ActivityId == id).SingleOrDefaultAsync();
            
            if (activity == null)
                return NotFound();

            return View(activity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var activity = await _context.Activities.Where(a => a.ActivityId == id).SingleOrDefaultAsync();
            _context.Remove(activity);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Atividade " + activity.Description.ToUpper() + " foi removida.";

            return RedirectToAction(nameof(Index));
        }


        private bool ActivityExist(long? activityId)
        {
            return _context.Activities.Any(a => a.ActivityId == activityId);
        }
    }
}
