using GuardiansOfGlobe.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuardiansOfGlobeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlterEgoController : ControllerBase
    {
        private ModelContext _context;
        
        public AlterEgoController(ModelContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<List<AlterEgo>> Listar()
        {
            return await _context.AlterEgos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlterEgo>> BuscarPorId(decimal id)
        {
            var retorno = await _context.AlterEgos.FirstOrDefaultAsync(x => x.AlterEgoId == id);

            if (retorno != null)
                return retorno;
            else
                return NotFound();
        }

        //[HttpPost]
        //public async Task<ActionResult<AlterEgo>> Guardar(AlterEgo c)
        //{
        //    try
        //    {
        //        await _context.AlterEgos.AddAsync(c);
        //        await _context.SaveChangesAsync();
        //        c.AlterEgoId = await _context.AlterEgos.MaxAsync(u => u.AlterEgoId);

        //        return c;
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return StatusCode(500, "Se encontró un error");
        //    }
        //}

        //[HttpPut]
        //public async Task<ActionResult<AlterEgo>> Actualizar(AlterEgo c)
        //{
        //    if (c == null || c.AlterEgoId == 0)
        //        return BadRequest("Faltan datos");

        //    AlterEgo cat = await _context.AlterEgos.FirstOrDefaultAsync(x => x.AlterEgoId == c.AlterEgoId);

        //    if (cat == null)
        //        return NotFound();

        //    try
        //    {
        //        cat.Nombre = c.Nombre;
        //        _context.AlterEgos.Update(cat);
        //        await _context.SaveChangesAsync();

        //        return cat;
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return StatusCode(500, "Se encontró un error");
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> Eliminar(decimal id)
        //{
        //    AlterEgo cat = await _context.AlterEgos.FirstOrDefaultAsync(x => x.AlterEgoId == id);

        //    if (cat == null)
        //        return NotFound();

        //    try
        //    {
        //        _context.AlterEgos.Remove(cat);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return StatusCode(500, "Se encontró un error");
        //    }
        //}
    }
}
