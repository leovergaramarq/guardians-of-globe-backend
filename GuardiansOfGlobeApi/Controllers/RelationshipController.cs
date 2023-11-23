using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/relationships")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public RelationshipController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Relationship>>> FindMany()
        {
            try
            {
                return await _context.Relationships.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Relationship>> FindById(decimal id)
        {
            try
            {
                Relationship? relationship = await _context.Relationships.FirstOrDefaultAsync(
                    a => a.RelationshipId == id
                );

                if (relationship != null)
                    return relationship;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Relationship>> Create(Relationship relationshipCreate)
        {
            try
            {
                await _context.Relationships.AddAsync(relationshipCreate);
                await _context.SaveChangesAsync();
                relationshipCreate.RelationshipId = await _context.Relationships.MaxAsync(a => a.RelationshipId);

                return relationshipCreate;
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
        public async Task<ActionResult<Relationship>> Update(Relationship relationshipUpdate)
        {
            if (relationshipUpdate == null || relationshipUpdate.RelationshipId <= 0)
                return BadRequest("Missing data");

            try
            {
                Relationship? relationship = await _context.Relationships.FirstOrDefaultAsync(a => a.RelationshipId == relationshipUpdate.RelationshipId);

                if (relationship == null)
                    return NotFound();

                // relationship.RelationshipId = relationshipUpdate.RelationshipId;
                relationship.Person1Id = relationshipUpdate.Person1Id;
                relationship.Person2Id = relationshipUpdate.Person2Id;
                relationship.RelationshipType = relationshipUpdate.RelationshipType;

                _context.Relationships.Update(relationship);
                await _context.SaveChangesAsync();

                return relationship;
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
            Relationship? relationship = await _context.Relationships.FirstOrDefaultAsync(a => a.RelationshipId == id);

            if (relationship == null)
                return NotFound();

            try
            {
                _context.Relationships.Remove(relationship);
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
