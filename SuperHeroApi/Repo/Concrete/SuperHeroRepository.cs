using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Data;
using SuperHeroApi.Modles;
using SuperHeroApi.Repo.Abstract;

namespace SuperHeroApi.Repo.Concrete
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private SuperHeroDataContext _context;
        private readonly ILogger<SuperHeroRepository> logger;

        public SuperHeroRepository(
            SuperHeroDataContext context, 
            ILogger<SuperHeroRepository> logger,
            FooService fooService
            )
        {
            this._context = context;
            this.logger = logger;

            logger.LogInformation($"SuperHeroRepository created, and got {fooService.myNumber}");
        }

        public async Task<SuperHero?> AddSuperHero(SuperHero newSuperHero)
        {
            var hero = await this._context.SuperHeroes.AddAsync(newSuperHero);

            var result = await this._context.SaveChangesAsync();

            return result > 0 ? hero.Entity : null;
        }

        public async Task<int?> DeleteSuperHero(int id)
        {
            var hero = await this._context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return null;
            }

            this._context.SuperHeroes.Remove(hero);

            var result = await this._context.SaveChangesAsync();

            return id;
        }

        public async Task<SuperHero?> GetSuperHeroById(int id)
        {
            return await this._context.SuperHeroes.FindAsync(id);
        }

        public async Task<IEnumerable<SuperHero?>> GetSuperHeroesAsync()
        {
            return await this._context.SuperHeroes.ToListAsync();
        }

        public async Task<SuperHero?> UpdateSuperHero(int id, SuperHero updateSuperHero)
        {
            var dbHero = await this._context.SuperHeroes.FindAsync(id);

            if (dbHero == null) { return null; }

            var result = UpdateHero(updateSuperHero, dbHero);

            this._context.SuperHeroes.Update(result);

            await this._context.SaveChangesAsync();

            return result;
        }

        private static SuperHero UpdateHero(SuperHero hero, SuperHero? dbHero)
        {

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            return dbHero;
        }
    }
}
