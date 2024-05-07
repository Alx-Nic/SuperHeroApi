using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Modles;

namespace SuperHeroApi.Data
{
    //if not using Identity you must inherit from DBContext
    public class SuperHeroDataContext : IdentityDbContext<IdentityUser>
    {
        public SuperHeroDataContext(DbContextOptions<SuperHeroDataContext> options) : base(options)
        {
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
