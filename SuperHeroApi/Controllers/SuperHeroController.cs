using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.Modles;
using SuperHeroApi.Repo.Abstract;

namespace SuperHeroApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRepository _repo;

        public SuperHeroController(
            ISuperHeroRepository superHeroRepository,
            FooService foo, 
            ILogger<SuperHeroController> logger)

        {
            this._repo = superHeroRepository;
            logger.LogInformation($"Controller has got:{foo.myNumber}");


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
            try
            {

                var result = await this._repo.GetSuperHeroById(id);

                return result != null ? Ok(result) : NotFound("Hero not found");

            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            try
            {

                var result = await this._repo.GetSuperHeroesAsync();

                return result.Any() ? Ok(result) : NoContent();
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<SuperHero>> CreateHero(SuperHero heroDto)
        //{
        //    try
        //    {
        //        var hero = await this._context.SuperHeroes.AddAsync(heroDto);

        //        var result = await this._context.SaveChangesAsync();

        //        return (result > 0) ? Ok(hero.Entity) : BadRequest("Hero not created");



        //    }catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteHero(int id)
        //{
        //    var hero = await this._context.SuperHeroes.FindAsync(id);

        //    if(hero == null)
        //    {
        //        return NotFound("Hero not found");
        //    }

        //    this._context.SuperHeroes.Remove(hero);

        //    var result = await this._context.SaveChangesAsync();

        //    return StatusCode(204);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<SuperHero>> UpdateHero(int id, SuperHero hero)
        //{
        //    var dbHero = await this._context.SuperHeroes.FindAsync(id);

        //    if (dbHero == null) { return NotFound("Hero not found"); }

        //    var result = UpdateHero(hero, dbHero);

        //    this._context.SuperHeroes.Update(result);

        //    await this._context.SaveChangesAsync();

        //    return Ok(result);
        //}


    }
}
