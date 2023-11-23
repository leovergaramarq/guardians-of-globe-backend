using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/scheduleEvents")]
    [ApiController]
    public class ScheduleEventController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public ScheduleEventController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduleEvent>>> FindMany()
        {
            try
            {
                return await _context.ScheduleEvents.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleEvent>> FindById(decimal id)
        {
            try
            {
                ScheduleEvent? scheduleEvent = await _context.ScheduleEvents.FirstOrDefaultAsync(
                    a => a.ScheduleEventId == id
                );

                if (scheduleEvent != null)
                    return scheduleEvent;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleEvent>> Create(ScheduleEvent scheduleEventCreate)
        {
            try
            {
                await _context.ScheduleEvents.AddAsync(scheduleEventCreate);
                await _context.SaveChangesAsync();
                scheduleEventCreate.ScheduleEventId = await _context.ScheduleEvents.MaxAsync(a => a.ScheduleEventId);

                return scheduleEventCreate;
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
        public async Task<ActionResult<ScheduleEvent>> Update(ScheduleEvent scheduleEventUpdate)
        {
            if (scheduleEventUpdate == null || scheduleEventUpdate.ScheduleEventId <= 0)
                return BadRequest("Missing data");

            try
            {
                ScheduleEvent? scheduleEvent = await _context.ScheduleEvents.FirstOrDefaultAsync(a => a.ScheduleEventId == scheduleEventUpdate.ScheduleEventId);

                if (scheduleEvent == null)
                    return NotFound();

                // scheduleEvent.ScheduleEventId = scheduleEventUpdate.ScheduleEventId;
                scheduleEvent.PersonId = scheduleEventUpdate.PersonId;
                scheduleEvent.EventName = scheduleEventUpdate.EventName;
                scheduleEvent.DateStart = scheduleEventUpdate.DateStart;
                scheduleEvent.DateEnd = scheduleEventUpdate.DateEnd;

                _context.ScheduleEvents.Update(scheduleEvent);
                await _context.SaveChangesAsync();

                return scheduleEvent;
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
            ScheduleEvent? scheduleEvent = await _context.ScheduleEvents.FirstOrDefaultAsync(a => a.ScheduleEventId == id);

            if (scheduleEvent == null)
                return NotFound();

            try
            {
                _context.ScheduleEvents.Remove(scheduleEvent);
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
