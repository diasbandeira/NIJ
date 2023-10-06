using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NIJ.Web.Data;
using NIJ.Web.Data.DAL.Cadastros;
using Modelo.Cadastros;

namespace NIJ.Web.Areas.Entry.Controllers
{
    [Area("Entry")]
    public class UserController : Controller
    {
        private readonly IESContext _context;
        private readonly ProjectDAL projectDAL;
        private readonly ActivityDAL activityDAL;
        private readonly UserDAL userDAL;

        public UserController(IESContext context)
        {
            _context = context;
            projectDAL = new ProjectDAL(context);
            activityDAL = new ActivityDAL(context);
            userDAL = new UserDAL (context);
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult AdicionarUsuario()
        {
            PrepararViewBags(projectDAL.GetProjectsOrderByName().ToList(), new List<Activity>().ToList(), new List<User>().ToList());

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarUsuario([Bind("ProjectId, ActivityId, UserId")] AdicionarUsuarioModel model )
        {
            if(model.ProjectId == 0 || model.ActivityId == 0 || model.UserId == 0)
            {
                ModelState.AddModelError("", "É preciso selecionar todos os dados");
            }
            else
            {
                activityDAL.RegistrarUsuario((long)model.ActivityId, (long)model.UserId);
                PrepararViewBags(
                    projectDAL.GetProjectsOrderByName().ToList(),
                    activityDAL.GetActivitiesByName().ToList(),
                    activityDAL.GetUsersWithoutActivities(model.ActivityId).ToList()
                );
                
            }
            return View(model);
        }

        public void PrepararViewBags(List<Project> projects, List<Activity> activities, List<User> users)
        {
            projects.Insert(0, new Project { ProjectId = 0, Name = "Selecione um Projeto" });
            ViewBag.Projects = projects;
            
            activities.Insert(0, new Activity { ActivityId = 0, Description = "Selecione uma atividade" });
            ViewBag.Activities = activities;
            
            users.Insert(0, new User { UserId = 0, Name = "Selecione um usuario" });
            ViewBag.Users = users;
        }
    }
}
