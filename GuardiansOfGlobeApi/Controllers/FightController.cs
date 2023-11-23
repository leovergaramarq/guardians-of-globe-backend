using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/fights")]
    [ApiController]
    public class FightController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public FightController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fight>>> FindMany()
        {
            try
            {
                return await _context.Fights.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fight>> FindById(decimal id)
        {
            try
            {
                Fight? fight = await _context.Fights.FirstOrDefaultAsync(
                    a => a.FightId == id
                );

                if (fight != null)
                    return fight;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Fight>> Create(Fight fightCreate)
        {
            try
            {
                await _context.Fights.AddAsync(fightCreate);
                await _context.SaveChangesAsync();
                fightCreate.FightId = await _context.Fights.MaxAsync(a => a.FightId);

                return fightCreate;
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
        public async Task<ActionResult<Fight>> Update(Fight fightUpdate)
        {
            if (fightUpdate == null || fightUpdate.FightId <= 0)
                return BadRequest("Missing data");

            try
            {
                Fight? fight = await _context.Fights.FirstOrDefaultAsync(a => a.FightId == fightUpdate.FightId);

                if (fight == null)
                    return NotFound();

                // fight.FightId = fightUpdate.FightId;
                fight.DateStart = fightUpdate.DateStart;
                fight.DateEnd = fightUpdate.DateEnd;
                fight.Location = fightUpdate.Location;
                fight.FightTitle = fightUpdate.FightTitle;
                fight.Description = fightUpdate.Description;

                _context.Fights.Update(fight);
                await _context.SaveChangesAsync();

                return fight;
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
            Fight? fight = await _context.Fights.FirstOrDefaultAsync(a => a.FightId == id);

            if (fight == null)
                return NotFound();

            try
            {
                _context.Fights.Remove(fight);
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
