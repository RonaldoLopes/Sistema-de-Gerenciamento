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
    [Route("api/pe")]
    [ApiController]
    public class PontoExternoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<ActionResult<List<PontoExterno>>> Get([FromServices] SGContext context)
        {
            var dd = await context.PontoExternos
                .Include(p => p.Projetos)
                .Include(u => u.User)
                .AsNoTracking()
                .ToListAsync();
            return Ok(dd);
        }

        [HttpGet]
        [Route("{id:int}")]//restrição de rota
        [Authorize(Roles = "Admin, RH, RUser")]
        public async Task<ActionResult<PontoExterno>> GetById([FromServices] SGContext context, int id)
        {
            try
            {
                var pe = await context.PontoExternos
                    .Include(p => p.Projetos)
                    .Include(u => u.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(pe => pe.Id == id);
                return Ok(pe);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }
        }
        [HttpGet]
        [Route("{data}/{userId:int}/{projetoId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindPonto([FromServices] SGContext context, DateTime data, int userId, int projetoId)
        {
            var dd = await context.PontoExternos
                .AsNoTracking()
                .FirstOrDefaultAsync(dd => dd.UserId == userId && dd.Data == data && dd.ProjetosId == projetoId);


            return Ok(dd);

        }
        [HttpGet]
        [Route("{id:int}/{idUser:int}")]//restrição de rota
        [Authorize(Roles = "Gestor, RUser")]
        public async Task<ActionResult<PontoExterno>> GetByUserId([FromServices] SGContext context, int id)
        {
            try
            {
                var pe = await context.PontoExternos
                .Include(p => p.Projetos)
                .Include(u => u.User)
                .Where(pe => pe.UserId == id)
                .AsNoTracking()
                .ToListAsync();
                return Ok(pe);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor, RH, RUser")]
        public async Task<ActionResult<PontoExterno>> Post([FromServices] SGContext context, [FromBody] PontoExterno pontoExterno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.PontoExternos.Add(pontoExterno);
                    await context.SaveChangesAsync();
                    return Created($"/api/pe/{pontoExterno.Id}", pontoExterno);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados: " + ex.ToString());
            }
            return BadRequest();
        }

        [HttpPut("{id}/{data}/{pId}")]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor, RH")]
        public async Task<ActionResult<PontoExterno>> Put([FromServices] SGContext context, int id,
            DateTime data, int pId, [FromBody] PontoExterno pontoExterno)
        {

            try
            {
                PontoExterno pee;
                var pe = await context.PontoExternos
                    .AsNoTracking()
                  .FirstOrDefaultAsync(pe => pe.UserId == pontoExterno.UserId && pe.Data == data && pe.ProjetosId == pId);

                if (pe == null)
                {
                    return NotFound("Não encontrado");
                } else
                {
                    pee = new PontoExterno();
                    pee.Id = pe.Id;
                    pee.Data = pontoExterno.Data;
                    pee.EntraFabrica = pontoExterno.EntraFabrica;
                    pee.SaidaAlmo = pontoExterno.SaidaAlmo;
                    pee.RetorAlmo = pontoExterno.RetorAlmo;
                    pee.SaidaFabrica = pontoExterno.SaidaFabrica;
                    pee.AtvDia = pontoExterno.AtvDia;
                    pee.ProjetosId = pontoExterno.ProjetosId;
                    pee.UserId = pontoExterno.UserId;
                }

                // context.Update(pontoExterno);
                context.Update(pee);

                await context.SaveChangesAsync();

                return Created($"/api/pe/{pontoExterno.Id}", pontoExterno);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpDelete("{id}")]
        [Route("")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<ActionResult<PontoExterno>> Delete([FromServices] SGContext context, int id)
        {
            try
            {
                var pe = await context.PontoExternos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(pe => pe.Id == id);

                if (pe == null) return NotFound();

                context.Remove(pe);

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
