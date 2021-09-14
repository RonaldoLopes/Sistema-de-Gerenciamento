using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SG.Domain.Entities;
using SG.Repository.Context;

namespace SG.WebApi.Controllers
{
    [Route("api/ch")]
    [ApiController]
    public class CadernoHorasController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor")]
        public async Task<ActionResult<List<CadernoHoras>>> Get([FromServices] SGContext context)
        {
            var c = await context.CadernoHoras
                .Include(p => p.Projetos)
                .Include(u => u.User)
                .AsNoTracking()
                .ToListAsync();
            return Ok(c);
        }

        [HttpGet]
        [Route("{id:int}")]//restrição de rota
        public async Task<ActionResult<CadernoHoras>> GetById([FromServices] SGContext context, int id)
        {
            try
            {
                var c = await context.CadernoHoras
                    .Include(p => p.Projetos)
                    .Include(u => u.User)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
                return Ok(c);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }
        }
        [HttpGet]
        [Route("{id:int}/{idUser:int}")]
        [Authorize(Roles = "RUser")]
        public async Task<ActionResult<List<CadernoHoras>>> GetByUserId([FromServices] SGContext context, int id)
        {
            var c = await context.CadernoHoras
                .Include(p => p.Projetos)
                .Include(u => u.User)
                .Where(c => c.UserId == id)
                .AsNoTracking()
                .ToListAsync();
            return Ok(c);
        }
        [HttpGet]
        [Route("{data}/{userId:int}/{projetoId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindLancamento([FromServices] SGContext context, DateTime data, int userId, int projetoId)
        {
            var dd = await context.CadernoHoras
                .AsNoTracking()
                .FirstOrDefaultAsync(dd => dd.UserId == userId && dd.Data == data && dd.ProjetosId == projetoId);


            return Ok(dd);

        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor, RUser")]
        public async Task<ActionResult<CadernoHoras>> Post([FromServices] SGContext context, [FromBody] CadernoHoras cadernoHoras)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.CadernoHoras.Add(cadernoHoras);
                    await context.SaveChangesAsync();
                    return Created($"/api/ch/{cadernoHoras.Id}", cadernoHoras);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados: " + ex.ToString());
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("dd")]
        [Authorize(Roles = "Admin, Gestor, RH, RUser")]
        public async Task<ActionResult<CadernoHoras>> PostDD([FromServices] SGContext context, [FromBody] DadosDia dados)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    CadernoHoras ch = new CadernoHoras();

                    var horasDia = (dados.SaidaAlmo - dados.EntraFabrica) + (dados.SaidaFabrica - dados.RetorAlmo);
                    var desloc = (dados.EntraFabrica - dados.SaidaHotel) + (dados.ChegaHotel - dados.SaidaFabrica);
                    var horasT = horasDia - desloc;

                    ch.Data = dados.Data;
                    ch.ProjetosId = dados.ProjetosId;
                    ch.UserId = dados.UserId;
                    ch.AtvDia = dados.AtvDia;
                    if (horasDia != null)
                    {
                        ch.HorasDia = (TimeSpan)(horasDia);
                        ch.Deslocamento = (TimeSpan)(desloc);
                        ch.HorasTrab = (TimeSpan)(horasT);
                    }
                    else if (dados.HorasInterno != null)
                    {
                        ch.HorasTrab = (TimeSpan)dados.HorasInterno;
                    }

                    context.CadernoHoras.Add(ch);
                    await context.SaveChangesAsync();
                    return Created($"/api/ch/{ch.Id}", ch);


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
        [Authorize(Roles = "Admin, RH, Gestor, RUser")]
        public async Task<ActionResult<CadernoHoras>> Put([FromServices] SGContext context,
            int id, [FromBody] CadernoHoras cadernoH)//CadernoHoras cadernoHoras
        {

            try
            {
                var c = await context.CadernoHoras
                    .AsNoTracking()
                  .FirstOrDefaultAsync(c => c.Id == id);

                if (c == null)
                {
                    return NotFound();
                }
               

                context.Update(cadernoH);

                await context.SaveChangesAsync();

                return Created($"/api/ch/{c.Id}", c);

                /**/



            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpPut("{id}/{data}")]
        [Route("")]
        [Authorize(Roles = "Admin, RH, Gestor, RUser")]
        public async Task<ActionResult<CadernoHoras>> Put([FromServices] SGContext context, 
            int id, DateTime data, [FromBody] DadosDia dados)//CadernoHoras cadernoHoras
        {

            try
            {
                CadernoHoras ch;

                var c = await context.CadernoHoras
                    .AsNoTracking()
                  .FirstOrDefaultAsync(c => c.ProjetosId == id && c.Data == data);

                if (c == null)
                {
                    return NotFound();
                } else
                {
                    ch = new CadernoHoras();

                    var horasDia = (dados.SaidaAlmo - dados.EntraFabrica) + (dados.SaidaFabrica - dados.RetorAlmo);
                    var desloc = (dados.EntraFabrica - dados.SaidaHotel) + (dados.ChegaHotel - dados.SaidaFabrica);
                    var horasT = horasDia - desloc;

                    ch.Id = c.Id;
                    ch.Data = dados.Data;
                    ch.UserId = c.UserId;
                    ch.ProjetosId = dados.ProjetosId;
                    ch.HorasDia = (TimeSpan)(horasDia);
                    ch.Deslocamento = (TimeSpan)(desloc);
                    ch.HorasTrab = (TimeSpan)(horasT);
                }

                context.Update(ch);

                await context.SaveChangesAsync();

                return Created($"/api/ch/{ch.Id}", ch);

                /*var c = await context.CadernoHoras
                    .AsNoTracking()
                  .FirstOrDefaultAsync(c => c.Id == id);*/



            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpDelete("{id}")]
        [Route("")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<ActionResult<CadernoHoras>> Delete([FromServices] SGContext context, int id)
        {
            try
            {
                var c = await context.CadernoHoras
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (c == null) return NotFound();

                context.Remove(c);

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
