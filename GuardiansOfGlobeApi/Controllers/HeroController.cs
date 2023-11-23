using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/heroes")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public HeroController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlterEgo>>> FindMany()
        {
            try
            {

                Task<List<AlterEgo>> heroes = (
                    from alterEgo in _context.AlterEgos
                    where alterEgo.IsHero == true
                    select alterEgo
                ).ToListAsync();

                return await heroes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlterEgo>> FindById(decimal id)
        {
            try
            {
                AlterEgo? hero = await _context.AlterEgos.FirstOrDefaultAsync(
                    a => a.IsHero == true && a.AlterEgoId == id
                );

                if (hero != null)
                    return hero;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlterEgo>> Create(AlterEgo heroCreate)
        {
            if (heroCreate.IsHero == false) return BadRequest("Field 'isHero' must be true for heroes");

            heroCreate.IsHero = true;

            try
            {
                await _context.AlterEgos.AddAsync(heroCreate);
                await _context.SaveChangesAsync();
                heroCreate.AlterEgoId = await _context.AlterEgos.MaxAsync(a => a.AlterEgoId);

                return heroCreate;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPut]
        public async Task<ActionResult<AlterEgo>> Update(AlterEgo heroUpdate)
        {
            if (heroUpdate == null || heroUpdate.AlterEgoId <= 0)
                return BadRequest("Missing data");
            
            if (heroUpdate.IsHero == false) return BadRequest("Field 'isHero' must be true for heroes");

            try
            {
                AlterEgo? alterEgo = await _context.AlterEgos.FirstOrDefaultAsync(a => a.AlterEgoId == heroUpdate.AlterEgoId);

                if (alterEgo == null)
                    return NotFound();

                //alterEgo.AlterEgoId = heroUpdate.AlterEgoId;
                alterEgo.PersonId = heroUpdate.PersonId;
                alterEgo.Origin = heroUpdate.Origin;
                alterEgo.IsHero = heroUpdate.IsHero;
                alterEgo.Alias = heroUpdate.Alias;

                _context.AlterEgos.Update(alterEgo);
                await _context.SaveChangesAsync();

                return alterEgo;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(decimal id)
        {
            AlterEgo? alterEgo = await _context.AlterEgos.FirstOrDefaultAsync(a => a.AlterEgoId == id);

            if (alterEgo == null)
                return NotFound();

            try
            {
                _context.AlterEgos.Remove(alterEgo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }
    }
}
