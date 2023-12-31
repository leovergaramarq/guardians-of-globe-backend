﻿using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/alterEgos")]
    [ApiController]
    public class AlterEgoController : ControllerBase
    {
        private readonly ModelContext _context;
        
        public AlterEgoController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlterEgo>>> FindMany()
        {
            try
            {
                return await _context.AlterEgos.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlterEgo>> FindById(decimal id)
        {
            try
            {
                AlterEgo? alterEgo = await _context.AlterEgos.FirstOrDefaultAsync(
                    a => a.AlterEgoId == id
                );

                if (alterEgo != null)
                    return alterEgo;
                else
                    return NotFound();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AlterEgo>> Create(AlterEgo alterEgoCreate)
        {
            try
            {
                await _context.AlterEgos.AddAsync(alterEgoCreate);
                await _context.SaveChangesAsync();
                alterEgoCreate.AlterEgoId = await _context.AlterEgos.MaxAsync(a => a.AlterEgoId);

                return alterEgoCreate;
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
        public async Task<ActionResult<AlterEgo>> Update(AlterEgo alterEgoUpdate)
        {
            if (alterEgoUpdate == null || alterEgoUpdate.AlterEgoId <= 0)
                return BadRequest("Missing data");

            try
            {
                AlterEgo? alterEgo = await _context.AlterEgos.FirstOrDefaultAsync(a => a.AlterEgoId == alterEgoUpdate.AlterEgoId);

                if (alterEgo == null)
                    return NotFound();

                //alterEgo.AlterEgoId = alterEgoUpdate.AlterEgoId;
                alterEgo.PersonId = alterEgoUpdate.PersonId;
                alterEgo.Origin = alterEgoUpdate.Origin;
                alterEgo.IsHero = alterEgoUpdate.IsHero;
                alterEgo.Alias = alterEgoUpdate.Alias;

                _context.AlterEgos.Update(alterEgo);
                await _context.SaveChangesAsync();

                return alterEgo;
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
            AlterEgo? alterEgo = await _context.AlterEgos.FirstOrDefaultAsync(a => a.AlterEgoId == id);

            if (alterEgo == null)
                return NotFound();

            try
            {
                _context.AlterEgos.Remove(alterEgo);
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
