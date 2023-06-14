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
    public class ProjectController : Controller
    {
        private readonly IESContext _context;
        public ProjectController(IESContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.OrderBy(p => p.Name).ToListAsync());
        }
        //Get Project /Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel inserir os dados.");
            }
            return View(project);
        }
        //Get: Project/Edit/{id}
        public async Task<IActionResult> Edit(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ProjectId, Name")] Project project)
        {
            if(id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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

            return View(project);
        }

        private bool ProjectExists(long? id)
        {
            return _context.Projects.Any(p => p.ProjectId == id);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.Include(p => p.Activities).SingleOrDefaultAsync(p => p.ProjectId == id);
            if(project == null)
            {
                return NotFound();
            }
                
            return View(project);
        }

        //Get: Project/Delete/{id}
        public async Task<IActionResult> Delete(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(p => p.ProjectId == id);

            if(project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        //POST: Project/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.ProjectId == id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
