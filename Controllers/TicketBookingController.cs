using Microsoft.AspNetCore.Mvc;
using Tabaarak.Data;
using Tabaarak.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace Tabaarak.Controllers
{
    public class TicketBookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationContext _context;

        public TicketBookingController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: TicketBooking/Create
        public async Task<IActionResult> Create(int id)
        {
            // Check if an id is provided to filter a specific customer
            if (id != 0)
            {
                // Load only the customer with the specified id
                ViewBag.Customers = await _context.Customers
                    .Where(c => c.CustomerID == id)
                    .ToListAsync();
            }
            else
            {
                // Load all customers if no specific id is provided
                ViewBag.Customers = await _context.Customers.ToListAsync();
            }

            // Load only ticket names (types)
            var ticketTypes = await _context.Tickets
                .Select(t => t.TicketName) 
                .Distinct()
                .ToListAsync();

            // Ensure ViewBag.TicketTypes is not null
            ViewBag.TicketTypes = ticketTypes ?? new List<string>();

            return View();
        }




        // POST: TicketBooking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketBooking booking)
        {
            
                // Save the booking i
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create"); // Redirect to the list or confirmation page
            

            
        }




    }
}
