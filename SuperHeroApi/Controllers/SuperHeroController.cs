using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [HttpPost]
        public async Task<ActionResult<SuperHero>> createhero(SuperHero herodto)
        {
            try
            {

                var result = await this._repo.AddSuperHero(herodto);

                return (result != null) ? Ok(result) : BadRequest("hero not created");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var result = await this._repo.DeleteSuperHero(id);

            if (result == null)
            {
                return NotFound("Hero not found");
            }

            return StatusCode(204);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SuperHero>> UpdateHero(int id, SuperHero hero)
        {
            if(id != hero.Id)
            {
                return BadRequest();
            }

            var result = await this._repo.UpdateSuperHero(id, hero);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


    }
}
