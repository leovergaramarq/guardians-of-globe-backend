using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/alterEgoAbilities")]
    [ApiController]
    public class AlterEgoAbilityController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public AlterEgoAbilityController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlterEgoAbility>>> FindMany()
        {
            try
            {
                return await _context.AlterEgoAbilities.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlterEgoAbility>> FindById(decimal id)
        {
            try
            {
                AlterEgoAbility? alterEgoAbility = await _context.AlterEgoAbilities.FirstOrDefaultAsync(
                    a => a.AlterEgoAbilityId == id
                );

                if (alterEgoAbility != null)
                    return alterEgoAbility;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlterEgoAbility>> Create(AlterEgoAbility alterEgoAbilityCreate)
        {
            try
            {
                await _context.AlterEgoAbilities.AddAsync(alterEgoAbilityCreate);
                await _context.SaveChangesAsync();
                alterEgoAbilityCreate.AlterEgoAbilityId = await _context.AlterEgoAbilities.MaxAsync(a => a.AlterEgoAbilityId);

                return alterEgoAbilityCreate;
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
        public async Task<ActionResult<AlterEgoAbility>> Update(AlterEgoAbility alterEgoAbilityUpdate)
        {
            if (alterEgoAbilityUpdate == null || alterEgoAbilityUpdate.AlterEgoAbilityId <= 0)
                return BadRequest("Missing data");

            try
            {
                AlterEgoAbility? alterEgoAbility = await _context.AlterEgoAbilities.FirstOrDefaultAsync(a => a.AlterEgoAbilityId == alterEgoAbilityUpdate.AlterEgoAbilityId);

                if (alterEgoAbility == null)
                    return NotFound();

                // alterEgoAbility.AlterEgoAbilityId = alterEgoAbilityUpdate.AlterEgoAbilityId;
                alterEgoAbility.AlterEgoId = alterEgoAbilityUpdate.AlterEgoId;
                alterEgoAbility.AbilityName = alterEgoAbilityUpdate.AbilityName;

                _context.AlterEgoAbilities.Update(alterEgoAbility);
                await _context.SaveChangesAsync();

                return alterEgoAbility;
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
            AlterEgoAbility? alterEgoAbility = await _context.AlterEgoAbilities.FirstOrDefaultAsync(a => a.AlterEgoAbilityId == id);

            if (alterEgoAbility == null)
                return NotFound();

            try
            {
                _context.AlterEgoAbilities.Remove(alterEgoAbility);
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
