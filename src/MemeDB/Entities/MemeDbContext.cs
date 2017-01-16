using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MemeDB.Entities
{
    public class MemeDbContext : IdentityDbContext<User>
    {
        public MemeDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Meme> Memes { get; set; }
    }
}
