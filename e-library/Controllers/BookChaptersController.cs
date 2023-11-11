using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_library.Models;

namespace e_library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookChaptersController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public BookChaptersController(ElearningDbContext context)
        {
            _context = context;
        }

        // GET: api/BookChapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookChapter>>> GetBookChapters()
        {
          if (_context.BookChapters == null)
          {
              return NotFound();
          }
            return await _context.BookChapters.ToListAsync();
        }

        // GET: api/BookChapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookChapter>> GetBookChapter(int id)
        {
          if (_context.BookChapters == null)
          {
              return NotFound();
          }
            var bookChapter = await _context.BookChapters.FindAsync(id);

            if (bookChapter == null)
            {
                return NotFound();
            }

            return bookChapter;
        }

        // PUT: api/BookChapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookChapter(int id, BookChapter bookChapter)
        {
            if (id != bookChapter.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookChapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookChapterExists(id))
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

        // POST: api/BookChapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookChapter>> PostBookChapter(BookChapter bookChapter)
        {
          if (_context.BookChapters == null)
          {
              return Problem("Entity set 'ElearningDbContext.BookChapters'  is null.");
          }
            _context.BookChapters.Add(bookChapter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookChapter", new { id = bookChapter.Id }, bookChapter);
        }

        // DELETE: api/BookChapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookChapter(int id)
        {
            if (_context.BookChapters == null)
            {
                return NotFound();
            }
            var bookChapter = await _context.BookChapters.FindAsync(id);
            if (bookChapter == null)
            {
                return NotFound();
            }

            _context.BookChapters.Remove(bookChapter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookChapterExists(int id)
        {
            return (_context.BookChapters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
