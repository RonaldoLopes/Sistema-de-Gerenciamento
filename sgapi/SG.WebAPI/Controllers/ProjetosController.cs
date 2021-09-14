using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SG.Domain.Entities;
using SG.Repository.Context;
using SG.WebApi.Dto;

namespace SG.WebApi.Controllers
{
    [Route("api/projetos")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor, RUser")]
        public async Task<ActionResult<List<Projetos>>> Get([FromServices] SGContext context)
        {
            var p = await context.Projetos
                .Include(c => c.Cliente)
                .AsNoTracking()
                .ToListAsync();
            return Ok(p);
        }


        [HttpGet]
        [Route("dashUR")]
        // [Authorize(Roles = "Admin, Gestor, RUser")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Projetos>>> GetDashUR([FromServices] SGContext context)
        {
            try
            {
                var codProj = "";
                var random = new Random();
                int index;

                var p1 = await context
                        .Projetos
                        .AsNoTracking()
                        .Where(p => p.Concluido == false)
                        .ToListAsync();

                index = random.Next(p1.Count);

                codProj = p1[index].CodProjeto;

                var p = await context
                      .Projetos
                      .AsNoTracking()
                      .Where(p => p.CodProjeto == codProj)

                      .Select(u => new
                                ProjetosPieDto
                      {
                          //Concluidos = Convert.ToInt32(u.Concluido)
                          name = u.CodProjeto,
                          RPrevistos = new int[] { u.RecursosPrev },
                          RUtilizados = new int[] { u.RecursosUtil },
                          MPrevistos = new int[] { u.MobilizaPrev },
                          MUtilizados = new int[] { u.MobilizaUtili },
                          HPrevistasD = new int[] { u.HorasPrevDesen },
                          HUtilizadasD = new int[] { u.HorasPrevDesen },
                          HPrevistasI = new int[] { u.HorasPrevImplement },
                          HUtilizadasI = new int [] { u.HorasUtilImplement   }
                      })
                      .ToListAsync();

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }

        }

        [HttpGet]
        [Route("dashPIE")]
        //[Authorize(Roles = "Admin, Gestor, RUser")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Projetos>>> GetDash([FromServices] SGContext context)
        {
            try
            {
                var year = DateTime.Now.ToString("yyyy");
                var dataInicial = year + "-01-01";
                var dataFinal = year + "-12-31";

                var dtIni = DateTime.Parse(dataInicial);
                var dtFim = DateTime.Parse(dataFinal);

                string sql = "";
                sql += " SELECT distinct Concluido , count(Concluido) Total from projetos" +
                    " WHERE DataInicio BETWEEN " + "'" + dataInicial + "' AND " + "'" + dataFinal + "' group by concluido;";


                /* var p = await context.Projetos
                          .FromSqlRaw(sql)
                          .AsNoTracking()
                          .ToListAsync();
                 return Ok(p);*/


                /*var p = await context
                      .Projetos
                      .Where(p => p.DataInicio >= dtIni && p.DataInicio <= dtFim )
                      .Select(u => u.Concluido).Distinct()
                      .ToListAsync();*/

                var p = await context
                      .Projetos
                      .AsNoTracking()
                      .Where(p => p.DataInicio >= dtIni && p.DataInicio <= dtFim)

                      .Select(u => new
                                ProjetosPieDto
                      {
                          //Concluidos = Convert.ToInt32(u.Concluido)
                          name = (bool)u.Concluido ? "Concluídos" : "Aberto"
                      })
                      .ToListAsync();

                var newList =  p.GroupBy(p => p.name)
                                .Select(t => new ProjetosPieDto()
                                {
                                    name = t.Key,
                                    y = t.Select(p => p.name).Count()
                                })
                                .ToList();

                

                return Ok(newList);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }

        }

        [HttpGet]
        [Route("c")]
        [Authorize(Roles = "Admin, Gestor, RUser")]
        public async Task<ActionResult<List<Projetos>>> GetC([FromServices] SGContext context)
        {
            var p = await context.Projetos
                .Include(c => c.Cliente)
                .Where(p => p.Concluido.Equals(false))
                .AsNoTracking()
                .ToListAsync();
            return Ok(p);
        }

        [HttpGet]
        [Route("{id:int}")]//restrição de rota
        [Authorize(Roles = "Admin, Gestor, RUser")]
        public async Task<ActionResult<Projetos>> GetById([FromServices] SGContext context, int id)
        {
            try
            {
                var p = await context.Projetos
                    .Include(c => c.Cliente)
                    .Where(p => p.Concluido.Equals(false))
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);
                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor")]
        public async Task<ActionResult<Projetos>> Post([FromServices] SGContext context, [FromBody] Projetos projetos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Projetos.Add(projetos);
                    await context.SaveChangesAsync();
                    return Created($"/api/projetos/{projetos.Id}", projetos);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no banco de dados: " + ex.ToString());
            }
            return BadRequest();
        }
        [Route("")]
        [HttpPut("al/con/{id}")]        
        [Authorize(Roles = "Admin, Gestor")]
        public async Task<ActionResult<Projetos>> Put([FromServices] SGContext context, int id, [FromBody] Projetos projetos)
        {

            try
            {
                Projetos pj = new Projetos();

                var p = await context.Projetos
                    .AsNoTracking()
                  .FirstOrDefaultAsync(p => p.Id == id);

                if (p == null) return NotFound();

                pj.Id = projetos.Id;
                pj.CodProjeto = projetos.CodProjeto;
                pj.Descricao = projetos.Descricao;
                pj.RecursosPrev = projetos.RecursosPrev;
                pj.RecursosUtil = projetos.RecursosUtil;
                pj.MobilizaPrev = projetos.MobilizaPrev;
                pj.MobilizaUtili = projetos.MobilizaUtili;
                pj.DataInicio = projetos.DataInicio;
                pj.HorasPrevDesen = projetos.HorasPrevDesen;
                pj.HorasUtilDesenv = projetos.HorasUtilDesenv;
                pj.HorasPrevImplement = projetos.HorasPrevImplement;
                pj.HorasUtilImplement = projetos.HorasUtilImplement;
                pj.Concluido = false;
                pj.DataConclusao = projetos.DataConclusao;
                pj.ClienteId = projetos.ClienteId;

                context.Update(pj);

                await context.SaveChangesAsync();

                return Created($"/api/projetos/{projetos.Id}", projetos);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }
        [HttpPut("{id}")]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor")]
        public async Task<ActionResult<Projetos>> PutAberto([FromServices] SGContext context, int id, [FromBody] Projetos projetos)
        {

            try
            {
                var p = await context.Projetos
                    .AsNoTracking()
                  .FirstOrDefaultAsync(p => p.Id == id);

                if (p == null) return NotFound();

                context.Update(projetos);

                await context.SaveChangesAsync();

                return Created($"/api/projetos/{projetos.Id}", projetos);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }
        [HttpDelete("{id}")]
        [Route("")]
        [Authorize(Roles = "Admin, Gestor")]
        public async Task<ActionResult<Projetos>> Delete([FromServices] SGContext context, int id)
        {
            try
            {
                var p = await context.Projetos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (p == null) return NotFound();

                context.Remove(p);

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
