using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SG.Domain.Entities;
using SG.Repository.Context;

namespace SG.WebApi.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    [Authorize(Roles = "Admin, Gestor")]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        
        public async Task<ActionResult<List<Cliente>>> Get([FromServices] SGContext context)
        {
            var clie = await context.Clientes
                .AsNoTracking()
                .ToListAsync();
            return Ok(clie);
        }
        [HttpGet]
        [Route("{id:int}")]//restrição de rota
        public async Task<ActionResult<Cliente>> GetById([FromServices] SGContext context, int id)
        {
            try
            {
                var clie = await context.Clientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
                return Ok(clie);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Cliente>> Post([FromServices] SGContext context, [FromBody] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Clientes.Add(cliente);
                    await context.SaveChangesAsync();
                    return Created($"/api/clientes/{cliente.Id}", cliente);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados: " + ex.ToString());
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        [Route("")]
        public async Task<ActionResult<Cliente>> Put([FromServices] SGContext context, int id, [FromBody] Cliente cliente)
        {

            try
            {
                var clie = await context.Clientes
                    .AsNoTracking()
                  .FirstOrDefaultAsync(c => c.Id == id);

                if (clie == null) return NotFound();

                context.Update(cliente);

                await context.SaveChangesAsync();

                return Created($"/api/clientes/{cliente.Id}", cliente);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }
        [HttpDelete("{id}")]
        [Route("")]
        public async Task<ActionResult<Cliente>> Delete([FromServices] SGContext context, int id)
        {
            try
            {
                var clie = await context.Clientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (clie == null) return NotFound();

                context.Remove(clie);

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
