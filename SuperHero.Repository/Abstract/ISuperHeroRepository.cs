using HeroMSVC.Models.SuperHero;

namespace HeroMSVC.Repo.Abstract
{
    public interface ISuperHeroRepository
    {
        Task<IEnumerable<SuperHero>> GetSuperHeroesAsync(ISuperHeroFilterParams? filters);
        Task<SuperHero> GetSuperHeroById(int id);
        Task<int?> DeleteSuperHero(int id);
        Task<SuperHero> AddSuperHero(SuperHero newSuperHero);
        Task<SuperHero> UpdateSuperHero(int id, SuperHero updateSuperHero);
    }
}
