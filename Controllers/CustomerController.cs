using Microsoft.AspNetCore.Mvc;
using Tabaarak.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tabaarak.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Tabaarak.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationContext _context;

        public CustomerController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> customer()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        // GET: Customer/Create
        [Authorize]
        public async Task < IActionResult> Create()
        {
            var customTypes = await _context.CustomerTypes
                .Select(ct => new SelectListItem
                {
                    Value = ct.Customtype,
                    Text = ct.Customtype
                }).ToListAsync();

            // Pass the list of customer types to the view
            ViewData["CustomerTypes"] = customTypes;
            return View();
        }


        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer model)
        {
            
            
                _context.Add(model);
                await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Customer created successfully !";

            return View();
        }





        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            else {

                var customTypes = await _context.CustomerTypes
                        .Select(ct => new SelectListItem
                        {
                            Value = ct.Customtype,
                            Text = ct.Customtype
                        }).ToListAsync();

                // Pass the list of customer types to the view
                ViewData["CustomerTypes"] = customTypes;

                return View(customer);

            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer model)
        {

            
                _context.Update(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Customer updated successfully!";
                return RedirectToAction("customer");



            
        }






    }
}
