using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SG.Domain.Entities;
using SG.Repository.Context;

namespace SG.WebApi.Controllers
{
    [Route("api/dd")]
    [ApiController]
    public class DadosDiaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor")]
        public async Task<ActionResult<List<DadosDia>>> Get([FromServices] SGContext context)
        {
            var dd = await context.DadosDias
                .Include(p => p.Projetos)
                .Include(u => u.User)
                .AsNoTracking()
                .ToListAsync();
            return Ok(dd);
        }
        [HttpGet]
        [Route("{data}/{userId:int}/{projetoId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindLancamento([FromServices] SGContext context, DateTime data, int userId, int projetoId)
        {
            var dd = await context.DadosDias
                .AsNoTracking()
                .FirstOrDefaultAsync(dd => dd.UserId == userId && dd.Data == data && dd.ProjetosId == projetoId);


            return Ok(dd);

        }
        [HttpGet]
        [Route("{id:int}/{idUser:int}")]
        [Authorize(Roles = "RUser")]
        public async Task<ActionResult<List<DadosDia>>> GetByUserId([FromServices] SGContext context, int id)
        {
            var dd = await context.DadosDias
                .Include(p => p.Projetos)
                .Include(u => u.User)
                .Where(dd => dd.UserId == id)
                .AsNoTracking()
                .ToListAsync();
            return Ok(dd);
        }

        [HttpGet]
        [Route("{id:int}")]//restrição de rota
        public async Task<ActionResult<DadosDia>> GetById([FromServices] SGContext context, int id)
        {
            try
            {
                var dd = await context.DadosDias
                    .Include(p => p.Projetos)
                    .Include(u => u.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(dd => dd.Id == id);
                return Ok(dd);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor, RUser")]
        public async Task<ActionResult<DadosDia>> Post([FromServices] SGContext context, [FromBody] DadosDia dadosDia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.DadosDias.Add(dadosDia);
                    await context.SaveChangesAsync();
                    return Created($"/api/dd/{dadosDia.Id}", dadosDia);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados: " + ex.ToString());
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Gestor, RUser")]
        public async Task<ActionResult<DadosDia>> Put([FromServices] SGContext context, int id, [FromBody] DadosDia dadosDia)
        {

            try
            {
                var dd = await context.DadosDias
                    .AsNoTracking()
                  .FirstOrDefaultAsync(dd => dd.Id == id);

                if (dd == null) return NotFound();

                context.Update(dadosDia);

                await context.SaveChangesAsync();

                return Created($"/api/dd/{dadosDia.Id}", dadosDia);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Gestor, RUser")]
        public async Task<ActionResult<DadosDia>> Delete([FromServices] SGContext context, int id)
        {
            try
            {
                var dd = await context.DadosDias
                    .AsNoTracking()
                    .FirstOrDefaultAsync(dd => dd.Id == id);

                if (dd == null) return NotFound();

                context.Remove(dd);

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
