using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NIJ.Web.Data;
using Modelo.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NIJ.Web.Data.DAL.Cadastros;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace NIJ.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IESContext _context;
        private readonly ProjectDAL projectDAL;
        public ProjectController(IESContext context)
        {
            this._context = context;
            projectDAL = new ProjectDAL(context);
        }
        public async Task<IActionResult> Index()
        {
            return View(await projectDAL.GetProjectsOrderByName().ToListAsync());
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
                    await projectDAL.SaveProject(project);
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

            var project = await projectDAL.GetProjectById((long)id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("ProjectId, Name")] Project project, IFormFile foto)
        {
            if(id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var stream = new MemoryStream();
                    await foto.CopyToAsync(stream);
                    project.Foto = stream.ToArray();
                    project.FotoMineType = foto.ContentType;

                    await projectDAL.SaveProject(project);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProjectExists(project.ProjectId))
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

        private async Task<bool> ProjectExists(long? id)
        {
            return await projectDAL.GetProjectById((long) id) != null;
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await projectDAL.GetProjectById((long)id);
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

            var project = await projectDAL.GetProjectById((long)id);

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
            var project = await projectDAL.RemoveProjectById((long)id);
            
            return RedirectToAction(nameof(Index));
        }
        public async Task<FileContentResult> GetPhoto(long Id)
        {
            Project project = await projectDAL.GetProjectById(Id);
            
            if(project != null)
            {
                return File(project.Foto, project.FotoMineType);
            }

            return null;
        }
    }
}
