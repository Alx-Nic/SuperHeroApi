namespace HeroMSVC.Models.SuperHero
{
    public interface ISuperHeroFilterParams
    {
        string Name { get; set; }
        string Place { get; set; }
        string Power { get; set; }
    }


    public class SuperHeroFilterParamsV1 : ISuperHeroFilterParams
    {
        public string Name { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string Power { get; set; } = string.Empty;
    }
}
