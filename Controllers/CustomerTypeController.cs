using AspnetCoreMvcFull.Models.Entities;
using AspnetCoreMvcFull.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabaarak.Data;

namespace Tabaarak.Controllers
{
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationContext _context;

        public CustomerTypeController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Add() => View();


        public async Task<IActionResult> CustomerTypes()
        {
            var Ticket = await _context.CustomerTypes.ToListAsync();
            return View(Ticket);
        }




        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(CutomerTypeModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if a CustomerType with the same Customtype already exists
                var existingType = await _context.CustomerTypes
                    .FirstOrDefaultAsync(ct => ct.Customtype == model.Customtype);

                if (existingType != null)
                {
                    // Set TempData message if the Customtype already exists
                    TempData["ErrorMessage"] = "This customer type already exists.";
                    return RedirectToAction("Add"); // Redirect to show the message
                }

                // Proceed with adding the new CustomerType
                var newType = new CustomerType
                {
                    Customtype = model.Customtype,
                    CustomDescription = model.CustomDescription,
                };

                _context.Add(newType);
                await _context.SaveChangesAsync();

                // Set TempData message for success
                TempData["SuccessMessage"] = "Customer type added successfully!";
                return RedirectToAction("Add");
            }

            return View(model);
        }


        //Editing 



        public async Task<IActionResult> Edit(int id)
        {
            var CustomTyp = await _context.CustomerTypes.FindAsync(id);
            if (CustomTyp == null)
            {
                return NotFound();
            }

            // Subject entity to SubjectView
            var CustomTypObj = new CutomerTypeModel
            {
                CustomerId = CustomTyp.CustomerId,
                Customtype = CustomTyp.Customtype,
                CustomDescription = CustomTyp.CustomDescription,

            };

            return View(CustomTypObj);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(CutomerTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return view with validation errors
            }

            // Retrieve the existing subject from the database
            var existingSubject = await _context.CustomerTypes.FindAsync(model.CustomerId);
            if (existingSubject == null)
            {
                return NotFound();
            }

            // Update properties
            existingSubject.CustomerId = model.CustomerId;
            existingSubject.Customtype = model.Customtype;
            existingSubject.CustomDescription = model.CustomDescription;

            // Save changes to the database
            _context.Update(existingSubject);
            await _context.SaveChangesAsync();

            return RedirectToAction("CustomerTypes");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var CustomerTypeDelete = await _context.CustomerTypes.FindAsync(id);
            if (CustomerTypeDelete == null)
            {
                return NotFound();
            }

            // Convert to view model 

            var CustomerObj = new CutomerTypeModel
            {
                CustomerId = CustomerTypeDelete.CustomerId,
                Customtype = CustomerTypeDelete.Customtype,
                CustomDescription = CustomerTypeDelete.CustomDescription,
            };

            return View(CustomerObj);
        }


        //Delete comfirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var CustomTypeDelete = await _context.CustomerTypes.FindAsync(id);
            if (CustomTypeDelete == null)
            {
                return NotFound();
            }

            // Remove the subject from the context and save changes
            _context.CustomerTypes.Remove(CustomTypeDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("CustomerTypes");
        }



    }
}
