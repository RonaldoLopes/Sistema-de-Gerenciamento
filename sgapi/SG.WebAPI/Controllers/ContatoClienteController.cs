using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SG.Domain.Entities;
using SG.Repository.Context;
using SG.WebApi.Dto;

namespace SG.WebApi.Controllers
{
    [Route("api/ct")]
    [ApiController]
    [Authorize(Roles = "Admin, Gestor")]
    public class ContatoClienteController : ControllerBase
    {

        private readonly IMapper _mapper;

        public ContatoClienteController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ContatoCliente>>> Get([FromServices] SGContext context)
        {
            try
            {
                var ct = await context.ContatoClientes
                    .AsNoTracking()
                    .Include(c => c.Clientes)
                    .ToListAsync();

                //var results = _mapper.Map<IEnumerable<ContatoClienteDto>>(ct);

                return Ok(ct);
            }
            catch (System.Exception ex)
            {

                 return this.StatusCode(StatusCodes.Status500InternalServerError, $"erro {ex}");
            }
        }

        [HttpGet]
        [Route("{id:int}")]//restrição de rota
        public async Task<ActionResult<ContatoCliente>> GetById([FromServices] SGContext context, int id)
        {
            try
            {
                var ct = await context.ContatoClientes
                    .Include(c => c.Clientes)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
                return Ok(ct);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha: " + ex.ToString());
            }
        }
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ContatoCliente>> Post([FromServices] SGContext context, [FromBody] ContatoCliente ctCli)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.ContatoClientes.Add(ctCli);
                    await context.SaveChangesAsync();
                    return Created($"/api/ct/{ctCli.Id}", ctCli);
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
        public async Task<ActionResult<ContatoCliente>> Put([FromServices] SGContext context, int id, [FromBody] ContatoCliente conCli)
        {

            try
            {
                var ct = await context.ContatoClientes
                    .AsNoTracking()
                  .FirstOrDefaultAsync(ct => ct.Id == id);

                if (ct == null) return NotFound();

                context.Update(conCli);

                await context.SaveChangesAsync();

                return Created($"/api/ct/{ct.Id}", ct);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }
        [HttpDelete("{id}")]
        [Route("")]
        public async Task<ActionResult<ContatoCliente>> Delete([FromServices] SGContext context, int id)
        {
            try
            {
                var ct = await context.ContatoClientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (ct == null) return NotFound();

                context.Remove(ct);

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
