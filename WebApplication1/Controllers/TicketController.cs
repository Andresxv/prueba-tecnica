using ApiPruebaBackend.Context;
using ApiPruebaBackend.Gestion;
using ApiPruebaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiPruebaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketContext _dbContext;
        public TicketController(TicketContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/Ticket
        [HttpGet("Listar tickets")]
        public async Task<IActionResult> GetAllTickets([FromQuery] Pagination filter)
        {
            Pagination validFilter = new Pagination(filter.PageNumber, filter.PageSize);
            List<Tickets> pagedData = await _dbContext.Tickets.Where(s => s.Estatus == true)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            int totalCount = await _dbContext.Tickets.CountAsync();
            return Ok(new PageResponse<List<Tickets>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalCount));
        }

        // GET api/Ticket/5
        [HttpGet("Buscar ticket por Id")]
        public async Task<IActionResult> GetTicket(int id)
        {
            Tickets ticket = new Tickets();

            try
            {
                ticket = await _dbContext.Tickets.FindAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok(ticket);
        }

        // POST api/Ticket
        [HttpPost("Agregar ticket")]
        public async Task<IActionResult> PostTickets([FromBody] Tickets nuevoTicket)
        {

            try
            {
                nuevoTicket.FechaCreacion = DateTime.Now;
                
                _dbContext.Add(nuevoTicket);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return CreatedAtAction("GetTicket", new { id = nuevoTicket.Id }, nuevoTicket);
        }

        // PUT api/Ticket/5
        [HttpPut("Editar ticket")]
        public async Task<IActionResult> PutTicket(int id, [FromBody] Tickets editable)
        {
            Tickets ticket = new Tickets();
            try
            {
                ticket = await _dbContext.Tickets.FindAsync(id);
                if (ticket == null)
                {
                    editable.FechaActualizacion = DateTime.Now;
                    _dbContext.Entry(editable).State = EntityState.Modified;
                    _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }

        // DELETE api/Ticket/5
        [HttpDelete("Anular ticket")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            Tickets ticket = new Tickets();
            try
            {
                ticket = await _dbContext.Tickets.FindAsync(id);
                if (ticket != null)
                {
                    ticket.FechaActualizacion = DateTime.Now;
                    ticket.Estatus = false;
                    _dbContext.Entry(ticket).State = EntityState.Modified;
                    _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
        //RECOVER api/Ticket/5
        [HttpPost("Recuperar ticket")]
        public async Task<IActionResult> RecoverTicket(int id)
        {
            Tickets ticket = new Tickets();
            try
            {
                ticket = await _dbContext.Tickets.FindAsync(id);
                if (ticket != null)
                {
                    ticket.FechaActualizacion = DateTime.Now;
                    ticket.Estatus = true;
                    _dbContext.Entry(ticket).State = EntityState.Modified;
                    _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return NoContent();
        }
    }
}
