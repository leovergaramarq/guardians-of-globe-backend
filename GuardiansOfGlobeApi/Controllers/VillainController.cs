using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/villains")]
    [ApiController]
    public class VillainController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public VillainController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlterEgo>>> FindMany()
        {
            try
            {

                Task<List<AlterEgo>> villains = (
                    from alterEgo in _context.AlterEgos
                    where alterEgo.IsHero == false
                    select alterEgo
                ).ToListAsync();

                return await villains;
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
                AlterEgo? villain = await _context.AlterEgos.FirstOrDefaultAsync(
                    a => a.IsHero == false && a.AlterEgoId == id
                );

                if (villain != null)
                    return villain;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlterEgo>> Create(AlterEgo villainCreate)
        {
            if (villainCreate.IsHero == true) return BadRequest("Field 'IsHero' must be false for villains");

            villainCreate.IsHero = false;

            try
            {
                await _context.AlterEgos.AddAsync(villainCreate);
                await _context.SaveChangesAsync();
                villainCreate.AlterEgoId = await _context.AlterEgos.MaxAsync(a => a.AlterEgoId);

                return villainCreate;
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
        public async Task<ActionResult<AlterEgo>> Update(AlterEgo villainUpdate)
        {
            if (villainUpdate == null || villainUpdate.AlterEgoId <= 0)
                return BadRequest("Missing data");
            
            if (villainUpdate.IsHero == true) return BadRequest("Field 'IsHero' must be false for villains");

            villainUpdate.IsHero = false;

            try
            {
                AlterEgo? alterEgo = await _context.AlterEgos.FirstOrDefaultAsync(a => a.AlterEgoId == villainUpdate.AlterEgoId);

                if (alterEgo == null)
                    return NotFound();

                //alterEgo.AlterEgoId = villainUpdate.AlterEgoId;
                alterEgo.PersonId = villainUpdate.PersonId;
                alterEgo.Origin = villainUpdate.Origin;
                alterEgo.IsHero = villainUpdate.IsHero;
                alterEgo.Alias = villainUpdate.Alias;

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
