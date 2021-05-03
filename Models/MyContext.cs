using Microsoft.EntityFrameworkCore;
using Bbq.Models;

namespace Bbq.Models
{
        public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users { get;set; }

        public DbSet<BbqEvent> BbqEvents { get;set; }

        public DbSet<Rsvp> Rsvps { get;set; }
    }
}