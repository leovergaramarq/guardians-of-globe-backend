using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/sponsorSources")]
    [ApiController]
    public class SponsorSourceController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public SponsorSourceController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SponsorSource>>> FindMany()
        {
            try
            {
                return await _context.SponsorSources.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SponsorSource>> FindById(decimal id)
        {
            try
            {
                SponsorSource? sponsorSource = await _context.SponsorSources.FirstOrDefaultAsync(
                    a => a.SponsorSourceId == id
                );

                if (sponsorSource != null)
                    return sponsorSource;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<SponsorSource>> Create(SponsorSource sponsorSourceCreate)
        {
            try
            {
                await _context.SponsorSources.AddAsync(sponsorSourceCreate);
                await _context.SaveChangesAsync();
                sponsorSourceCreate.SponsorSourceId = await _context.SponsorSources.MaxAsync(a => a.SponsorSourceId);

                return sponsorSourceCreate;
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
        public async Task<ActionResult<SponsorSource>> Update(SponsorSource sponsorSourceUpdate)
        {
            if (sponsorSourceUpdate == null || sponsorSourceUpdate.SponsorSourceId <= 0)
                return BadRequest("Missing data");

            try
            {
                SponsorSource? sponsorSource = await _context.SponsorSources.FirstOrDefaultAsync(a => a.SponsorSourceId == sponsorSourceUpdate.SponsorSourceId);

                if (sponsorSource == null)
                    return NotFound();

                // sponsorSource.SponsorSourceId = sponsorSourceUpdate.SponsorSourceId;
                sponsorSource.SponsorSourceName = sponsorSourceUpdate.SponsorSourceName;
                sponsorSource.Reliability = sponsorSourceUpdate.Reliability;

                _context.SponsorSources.Update(sponsorSource);
                await _context.SaveChangesAsync();

                return sponsorSource;
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
            SponsorSource? sponsorSource = await _context.SponsorSources.FirstOrDefaultAsync(a => a.SponsorSourceId == id);

            if (sponsorSource == null)
                return NotFound();

            try
            {
                _context.SponsorSources.Remove(sponsorSource);
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
