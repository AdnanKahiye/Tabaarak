using AspnetCoreMvcFull.Models.Entities;
using AspnetCoreMvcFull.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tabaarak.Data;

namespace Tabaarak.Controllers
{
    public class TicketController : Controller

    {

        private readonly ApplicationContext _context;

        public TicketController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Tickets()
        {
            var Ticket = await _context.Tickets.ToListAsync();
            return View(Ticket);
        }


        public IActionResult Add() => View();



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Add(TicketModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if a ticket with the same name already exists
            var existingTicket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.TicketName == model.TicketName);

            if (existingTicket != null)
            {
                // Set a TempData message to notify the user about the duplicate name
                TempData["ErrorMessage"] = "A ticket with this name already exists. Please choose a different name.";
                return RedirectToAction("Ticket"); // Redirect to the same form
            }

            // Proceed with creating a new ticket if the name is unique
            var NewTicket = new Tickets
            {
                TicketAmount = model.TicketAmount,
                TicketName = model.TicketName,
                TicketDescription = model.TicketDescription,
            };

            _context.Add(NewTicket);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Ticket created successfully !";
            return RedirectToAction("Add");
        }



        //Editing Method

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Subject entity to SubjectView
            var Ticket = new TicketModel
            {
                TicketId = ticket.TicketId,
                TicketName = ticket.TicketName,
                TicketAmount = ticket.TicketAmount,
                TicketDescription = ticket.TicketDescription,

            };

            return View(Ticket);
        }





        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(TicketModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return view with validation errors
            }

            // Retrieve the existing subject from the database
            var existingSubject = await _context.Tickets.FindAsync(model.TicketId);
            if (existingSubject == null)
            {
                return NotFound();
            }

            // Update properties
            existingSubject.TicketId = model.TicketId;
            existingSubject.TicketName = model.TicketName;
            existingSubject.TicketAmount = model.TicketAmount;
            existingSubject.TicketDescription = model.TicketDescription;

            // Save changes to the database
            _context.Update(existingSubject);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Ticket updated successfully !";


            return RedirectToAction("Tickets");
        }

        //Deleted

        // Delete -
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Convert to view model 

            var ticketView = new TicketModel
            {
                TicketId = ticket.TicketId,
                TicketName = ticket.TicketName,
                TicketAmount = ticket.TicketAmount,
                TicketDescription = ticket.TicketDescription,
            };

            return View(ticketView);
        }


        //Delete comfirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Tickets.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            // Remove the subject from the context and save changes
            _context.Tickets.Remove(subject);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Ticket Deleted successfully !";

            return RedirectToAction("Tickets");
        }



    }
}
