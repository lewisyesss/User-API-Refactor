using Microsoft.EntityFrameworkCore;
using Tests.Users.Api.Models;

namespace Tests.Users.Api
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Must stay as in memory database
            optionsBuilder.UseInMemoryDatabase("Tests.User.Api");
        }

        public DbSet<User> Users { get; set; }
    }
}
