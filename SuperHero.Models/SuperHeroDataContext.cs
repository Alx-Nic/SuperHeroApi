using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace HeroMSVC.Models
{
    //if not using Identity you should inherit from DBContext
    public class SuperHeroDataContext : IdentityDbContext<IdentityUser>
    {
        public SuperHeroDataContext(DbContextOptions<SuperHeroDataContext> options) : base(options)
        {
        }

        public DbSet<SuperHero.SuperHero> SuperHeroes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var initialHeroes = new List<SuperHero.SuperHero>()
            {
                new SuperHero.SuperHero()
                {
                    Id = 1,
                    Name = "SpiderMan",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "NewYork City",
                    Power = 85

                }
            };

            builder.Entity<SuperHero.SuperHero>().HasData(initialHeroes);
        }

    }
}
