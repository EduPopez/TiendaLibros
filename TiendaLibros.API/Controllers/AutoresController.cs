using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Data;
using TiendaLibros.API.DTO;
using TiendaLibros.API.Models;
using TiendaLibros.API.Static;

namespace TiendaLibros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly TiendaLibrosContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AutoresController> _logger;
        

        public AutoresController(TiendaLibrosContext context, IMapper mapper, ILogger<AutoresController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> GetAutors()
        {

            try
            {
                if (_context.Autors == null)
                {
                    return NotFound();
                }
               


                    var autores = _mapper.Map<IEnumerable<AutorDto>>(await _context.Autors.ToListAsync());
                    return Ok(autores);
                
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en la peticion get de {nameof(GetAutors)}");
                return StatusCode(500, Mensajes.MensajeError500);
            }
          
        }

        // GET: api/Autores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutor(Guid id)
        {
          if (_context.Autors == null)
          {
              return NotFound();
          }
            var autor = _mapper.Map<AutorDto>(await _context.Autors.FindAsync(id));

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        // PUT: api/Autores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor(Guid id, AutorUpdateDto autorDto)
        {
            //if (id != autorDto.Id)
            //{
            //    return BadRequest();
            //}

            var autor = await _context.Autors.FindAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            _mapper.Map(autorDto, autor);
            autor.Id = id;
            _context.Entry(autor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Autores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AutorCreateDto>> PostAutor(AutorCreateDto autorDto)
        {
          if (_context.Autors == null)
          {
              return Problem("Entity set 'TiendaLibrosContext.Autors'  is null.");
          }

          var autor = _mapper.Map<Autor>(autorDto);
            
          _context.Autors.Add(autor);
          try
          {
              await _context.SaveChangesAsync();
          }
          catch (DbUpdateException)
          {
              if (AutorExists(autor.Id))
              {
                  return Conflict();
              }
              else
              {
                  throw;
              }
          }

            //return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autor);
            return Ok(autorDto);
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(Guid id)
        {
            if (_context.Autors == null)
            {
                return NotFound();
            }
            var autor = await _context.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Autors.Remove(autor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutorExists(Guid id)
        {
            return (_context.Autors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
