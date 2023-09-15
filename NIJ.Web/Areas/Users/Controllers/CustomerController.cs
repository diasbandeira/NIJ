using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NIJ.Web.Data;
using NIJ.Web.Data.DAL.Cadastros;
using Modelo.Cadastros;
using Microsoft.EntityFrameworkCore;

namespace NIJ.Web.Areas.Users.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IESContext _context;
        private readonly CustomerDAL customerDAL;

        public CustomerController(IESContext context)
        {
            _context = context;
            customerDAL = new CustomerDAL(context);
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            return View(await customerDAL.GetCustomersByName().ToListAsync());
        }

        public async Task<IActionResult> GetViewCustomerById(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var customer = await customerDAL.GetCustomerById((long)id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            return await GetViewCustomerById(id);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            return await GetViewCustomerById(id);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cnpj, Name, CreatedAt")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await customerDAL.SaveCustomer(customer);
                    return RedirectToAction(nameof(Index));
                }                
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel salvar o registro");
                
                
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(long? id, [Bind("CustomerId, Cnpj, Name, CreatedAt")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await customerDAL.SaveCustomer(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CustomerExists(customer.CustomerId))
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

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var customer = await customerDAL.DeleteCustomer((long)id);
            TempData["Message"] = "Customer " + customer.Name.ToUpper() + "foi removido.";
            
            return RedirectToAction(nameof(Index));
            
        }


        //// POST: Customer/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}        

        

        private async Task<bool> CustomerExists(long? id)
        {
            return await customerDAL.GetCustomerById((long)id) != null;
        }
    }
}
