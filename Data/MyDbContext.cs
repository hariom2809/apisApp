using apisApp.Models;
using Microsoft.EntityFrameworkCore;

namespace apisApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options)
        : base(options) 
        { 
        }
        public DbSet<User> Users { get; set; }
    }
}
