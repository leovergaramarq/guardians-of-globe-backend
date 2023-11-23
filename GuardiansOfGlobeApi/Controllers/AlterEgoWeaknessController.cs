using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/alterEgoWeaknesses")]
    [ApiController]
    public class AlterEgoWeaknessController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public AlterEgoWeaknessController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlterEgoWeakness>>> FindMany()
        {
            try
            {
                return await _context.AlterEgoWeaknesses.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlterEgoWeakness>> FindById(decimal id)
        {
            try
            {
                AlterEgoWeakness? alterEgoWeakness = await _context.AlterEgoWeaknesses.FirstOrDefaultAsync(
                    a => a.AlterEgoWeaknessId == id
                );

                if (alterEgoWeakness != null)
                    return alterEgoWeakness;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlterEgoWeakness>> Create(AlterEgoWeakness alterEgoWeaknessCreate)
        {
            try
            {
                await _context.AlterEgoWeaknesses.AddAsync(alterEgoWeaknessCreate);
                await _context.SaveChangesAsync();
                alterEgoWeaknessCreate.AlterEgoWeaknessId = await _context.AlterEgoWeaknesses.MaxAsync(a => a.AlterEgoWeaknessId);

                return alterEgoWeaknessCreate;
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
        public async Task<ActionResult<AlterEgoWeakness>> Update(AlterEgoWeakness alterEgoWeaknessUpdate)
        {
            if (alterEgoWeaknessUpdate == null || alterEgoWeaknessUpdate.AlterEgoWeaknessId <= 0)
                return BadRequest("Missing data");

            try
            {
                AlterEgoWeakness? alterEgoWeakness = await _context.AlterEgoWeaknesses.FirstOrDefaultAsync(a => a.AlterEgoWeaknessId == alterEgoWeaknessUpdate.AlterEgoWeaknessId);

                if (alterEgoWeakness == null)
                    return NotFound();

                // alterEgoWeakness.AlterEgoWeaknessId = alterEgoWeaknessUpdate.AlterEgoWeaknessId;
                alterEgoWeakness.AlterEgoId = alterEgoWeaknessUpdate.AlterEgoId;
                alterEgoWeakness.WeaknessName = alterEgoWeaknessUpdate.WeaknessName;

                _context.AlterEgoWeaknesses.Update(alterEgoWeakness);
                await _context.SaveChangesAsync();

                return alterEgoWeakness;
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
            AlterEgoWeakness? alterEgoWeakness = await _context.AlterEgoWeaknesses.FirstOrDefaultAsync(a => a.AlterEgoWeaknessId == id);

            if (alterEgoWeakness == null)
                return NotFound();

            try
            {
                _context.AlterEgoWeaknesses.Remove(alterEgoWeakness);
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
