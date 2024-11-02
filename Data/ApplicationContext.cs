using AspnetCoreMvcFull.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Tabaarak.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Tabaarak.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TicketBooking> TicketBookings { get; set; } 
    }
}
