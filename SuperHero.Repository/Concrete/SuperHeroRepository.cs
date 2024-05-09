using HeroMSVC.Models;
using HeroMSVC.Models.SuperHero;
using HeroMSVC.Repo.Abstract;
using HeroMSVC.Repo.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;

namespace HeroMSVC.Repo.Concrete
{
    public class SuperHeroRepository : ISuperHeroRepository
    {
        private SuperHeroDataContext _context;
        private readonly ILogger<SuperHeroRepository> logger;

        public SuperHeroRepository(
            SuperHeroDataContext context,
            ILogger<SuperHeroRepository> logger
            )
        {
            _context = context;
            this.logger = logger;
        }

        public async Task<SuperHero?> AddSuperHero(SuperHero newSuperHero)
        {
            var hero = await _context.SuperHeroes.AddAsync(newSuperHero);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? hero.Entity : null;
        }

        public async Task<int?> DeleteSuperHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return null;
            }

            _context.SuperHeroes.Remove(hero);

            var result = await _context.SaveChangesAsync();

            return id;
        }

        public async Task<SuperHero?> GetSuperHeroById(int id)
        {
            return await _context.SuperHeroes.FindAsync(id);
        }

        public async Task<IEnumerable<SuperHero?>> GetSuperHeroesAsync(ISuperHeroFilterParams filters)
        {

            IQueryable<SuperHero> query = _context.SuperHeroes;

            if (filters != null)
            {
                foreach (var property in typeof(ISuperHeroFilterParams).GetProperties())
                {
                    var filterValue = (string)property.GetValue(filters);

                    if (!string.IsNullOrEmpty(filterValue))
                    {
                        var predicate = ExpressionHelper.GetFilterExpression<SuperHero>(filterValue, property.Name);
                        query = query.Where(predicate);
                    }
                }
            }



            return await query.ToListAsync();
        }

        public async Task<SuperHero?> UpdateSuperHero(int id, SuperHero updateSuperHero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);

            if (dbHero == null) { return null; }

            var result = UpdateHero(updateSuperHero, dbHero);

            _context.SuperHeroes.Update(result);

            await _context.SaveChangesAsync();

            return result;
        }

        private SuperHero UpdateHero(SuperHero hero, SuperHero? dbHero)
        {

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            return dbHero;
        }
    }
}
