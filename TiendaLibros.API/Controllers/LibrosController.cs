using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Data;
using TiendaLibros.API.DTO.Libro;
using TiendaLibros.API.Models;

namespace TiendaLibros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly TiendaLibrosContext _context;
        private readonly ILogger<LibrosController> _logger;
        private readonly IMapper _mapper;

        public LibrosController(TiendaLibrosContext context, IMapper mapper, ILogger<LibrosController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDto>>> GetLibros()
        {
          if (_context.Libros == null)
          {
              return NotFound();
          }

          var libros = await _context.Libros
                .Include(q => q.Autor)
                .ProjectTo<LibroDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            //var retorno = _mapper.Map<IEnumerable<LibroDto>>(libros);
            return Ok(libros);
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDto>> GetLibro(Guid id)
        {
          if (_context.Libros == null)
          {
              return NotFound();
          }
            var libro = await _context.Libros.Include(q => q.Autor).Where(q => q.Id == id).SingleOrDefaultAsync();
            var retornar = _mapper.Map<LibroDto>(libro);

            if (libro == null)
            {
                return NotFound();
            }



            return Ok(retornar);
        }

        // PUT: api/Libros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(Guid id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
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

        // POST: api/Libros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LibroDto>> PostLibro(LibroCreateDto libroDto)
        {
          if (_context.Libros == null)
          {
              return Problem("Entity set 'TiendaLibrosContext.Libros'  is null.");
          }


          // En este caso lo que esta con <> a lo que quiero llegar y lo que esta dentro del parentisis representa el origen

            var libro = _mapper.Map<Libro>(libroDto);
            _context.Libros.Add(libro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LibroExists(libro.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // Cuando se usa CreatedAtAction, los parámetros que se deben pasar son
            // 1: Nombre del Metodo
            // 2: Parametros, en este caso es el new
            // 3: Objeto a devolver.
            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libroDto);

            //return CreatedAtAction("GetLibro", new { id = libro.Id }, libro);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(Guid id)
        {
            if (_context.Libros == null)
            {
                return NotFound();
            }
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(Guid id)
        {
            return (_context.Libros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
