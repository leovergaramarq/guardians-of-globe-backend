using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/sponsorships")]
    [ApiController]
    public class SponsorshipController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public SponsorshipController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sponsorship>>> FindMany()
        {
            try
            {
                return await _context.Sponsorships.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sponsorship>> FindById(decimal id)
        {
            try
            {
                Sponsorship? sponsorship = await _context.Sponsorships.FirstOrDefaultAsync(
                    a => a.SponsorshipId == id
                );

                if (sponsorship != null)
                    return sponsorship;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Sponsorship>> Create(Sponsorship sponsorshipCreate)
        {
            try
            {
                await _context.Sponsorships.AddAsync(sponsorshipCreate);
                await _context.SaveChangesAsync();
                sponsorshipCreate.SponsorshipId = await _context.Sponsorships.MaxAsync(a => a.SponsorshipId);

                return sponsorshipCreate;
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
        public async Task<ActionResult<Sponsorship>> Update(Sponsorship sponsorshipUpdate)
        {
            if (sponsorshipUpdate == null || sponsorshipUpdate.SponsorshipId <= 0)
                return BadRequest("Missing data");

            try
            {
                Sponsorship? sponsorship = await _context.Sponsorships.FirstOrDefaultAsync(a => a.SponsorshipId == sponsorshipUpdate.SponsorshipId);

                if (sponsorship == null)
                    return NotFound();

                // sponsorship.SponsorshipId = sponsorshipUpdate.SponsorshipId;
                sponsorship.SponsorId = sponsorshipUpdate.SponsorId;
                sponsorship.AlterEgoId = sponsorshipUpdate.AlterEgoId;

                _context.Sponsorships.Update(sponsorship);
                await _context.SaveChangesAsync();

                return sponsorship;
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
            Sponsorship? sponsorship = await _context.Sponsorships.FirstOrDefaultAsync(a => a.SponsorshipId == id);

            if (sponsorship == null)
                return NotFound();

            try
            {
                _context.Sponsorships.Remove(sponsorship);
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
