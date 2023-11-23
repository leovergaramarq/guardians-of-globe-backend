using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public PersonController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> FindMany()
        {
            try
            {
                return await _context.People.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> FindById(decimal id)
        {
            try
            {
                Person? person = await _context.People.FirstOrDefaultAsync(
                    a => a.PersonId == id
                );

                if (person != null)
                    return person;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Create(Person personCreate)
        {
            try
            {
                await _context.People.AddAsync(personCreate);
                await _context.SaveChangesAsync();
                personCreate.PersonId = await _context.People.MaxAsync(a => a.PersonId);

                return personCreate;
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
        public async Task<ActionResult<Person>> Update(Person personUpdate)
        {
            if (personUpdate == null || personUpdate.PersonId <= 0)
                return BadRequest("Missing data");

            try
            {
                Person? person = await _context.People.FirstOrDefaultAsync(a => a.PersonId == personUpdate.PersonId);

                if (person == null)
                    return NotFound();

                // person.PersonId = personUpdate.PersonId;
                person.PersonName = personUpdate.PersonName;
                person.Birthdate = personUpdate.Birthdate;
                person.Sex = personUpdate.Sex;
                person.Occupation = personUpdate.Occupation;
                person.Address = personUpdate.Address;

                _context.People.Update(person);
                await _context.SaveChangesAsync();

                return person;
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
            Person? person = await _context.People.FirstOrDefaultAsync(a => a.PersonId == id);

            if (person == null)
                return NotFound();

            try
            {
                _context.People.Remove(person);
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
