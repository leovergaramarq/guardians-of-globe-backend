using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/sponsors")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public SponsorController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sponsor>>> FindMany()
        {
            try
            {
                var sponsors = await _context.Sponsors
                    .Include(s => s.SponsorSource)
                    .Include(s => s.Sponsorships)
                    //.ThenInclude(s => s.AlterEgo)
                    .ToListAsync();
                
                return sponsors;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sponsor>> FindById(decimal id)
        {
            try
            {
                Sponsor? sponsor = await _context.Sponsors.FirstOrDefaultAsync(
                    a => a.SponsorId == id
                );

                if (sponsor != null)
                    return sponsor;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Sponsor>> Create(Sponsor sponsorCreate)
        {
            try
            {
                await _context.Sponsors.AddAsync(sponsorCreate);
                await _context.SaveChangesAsync();
                sponsorCreate.SponsorId = await _context.Sponsors.MaxAsync(a => a.SponsorId);

                return sponsorCreate;
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
        public async Task<ActionResult<Sponsor>> Update(Sponsor sponsorUpdate)
        {
            if (sponsorUpdate == null || sponsorUpdate.SponsorId <= 0)
                return BadRequest("Missing data");

            try
            {
                Sponsor? sponsor = await _context.Sponsors.FirstOrDefaultAsync(a => a.SponsorId == sponsorUpdate.SponsorId);

                if (sponsor == null)
                    return NotFound();

                // sponsor.SponsorId = sponsorUpdate.SponsorId;
                sponsor.SponsorSourceId = sponsorUpdate.SponsorSourceId;
                sponsor.SponsorName = sponsorUpdate.SponsorName;

                _context.Sponsors.Update(sponsor);
                await _context.SaveChangesAsync();

                return sponsor;
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
            Sponsor? sponsor = await _context.Sponsors.FirstOrDefaultAsync(a => a.SponsorId == id);

            if (sponsor == null)
                return NotFound();

            try
            {
                _context.Sponsors.Remove(sponsor);
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
