using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Modles;

namespace SuperHeroApi.Data
{
    public class SuperHeroDataContext : DbContext
    {
        public SuperHeroDataContext(DbContextOptions<SuperHeroDataContext> options) : base(options)
        {
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
