using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/alterEgoFights")]
    [ApiController]
    public class AlterEgoFightController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public AlterEgoFightController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlterEgoFight>>> FindMany()
        {
            try
            {
                return await _context.AlterEgoFights.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlterEgoFight>> FindById(decimal id)
        {
            try
            {
                AlterEgoFight? alterEgoFight = await _context.AlterEgoFights.FirstOrDefaultAsync(
                    a => a.AlterEgoFightId == id
                );

                if (alterEgoFight != null)
                    return alterEgoFight;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlterEgoFight>> Create(AlterEgoFight alterEgoFightCreate)
        {
            try
            {
                await _context.AlterEgoFights.AddAsync(alterEgoFightCreate);
                await _context.SaveChangesAsync();
                alterEgoFightCreate.AlterEgoFightId = await _context.AlterEgoFights.MaxAsync(a => a.AlterEgoFightId);

                return alterEgoFightCreate;
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
        public async Task<ActionResult<AlterEgoFight>> Update(AlterEgoFight alterEgoFightUpdate)
        {
            if (alterEgoFightUpdate == null || alterEgoFightUpdate.AlterEgoFightId <= 0)
                return BadRequest("Missing data");

            try
            {
                AlterEgoFight? alterEgoFight = await _context.AlterEgoFights.FirstOrDefaultAsync(a => a.AlterEgoFightId == alterEgoFightUpdate.AlterEgoFightId);

                if (alterEgoFight == null)
                    return NotFound();

                //alterEgoFight.AlterEgoFightId = alterEgoFightUpdate.AlterEgoFightId;
                alterEgoFight.AlterEgoId = alterEgoFightUpdate.AlterEgoId;
                alterEgoFight.FightId = alterEgoFightUpdate.FightId;
                alterEgoFight.Victory = alterEgoFightUpdate.Victory;
                alterEgoFight.Side = alterEgoFightUpdate.Side;

                _context.AlterEgoFights.Update(alterEgoFight);
                await _context.SaveChangesAsync();

                return alterEgoFight;
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
            AlterEgoFight? alterEgoFight = await _context.AlterEgoFights.FirstOrDefaultAsync(a => a.AlterEgoFightId == id);

            if (alterEgoFight == null)
                return NotFound();

            try
            {
                _context.AlterEgoFights.Remove(alterEgoFight);
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
