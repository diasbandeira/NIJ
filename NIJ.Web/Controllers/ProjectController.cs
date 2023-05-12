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
    }
}
