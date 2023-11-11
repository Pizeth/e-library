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
    public class ChapterDetailsController : ControllerBase
    {
        private readonly ElearningDbContext _context;

        public ChapterDetailsController(ElearningDbContext context)
        {
            _context = context;
        }

        // GET: api/ChapterDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterDetail>>> GetChapterDetails()
        {
          if (_context.ChapterDetails == null)
          {
              return NotFound();
          }
            return await _context.ChapterDetails.ToListAsync();
        }

        // GET: api/ChapterDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDetail>> GetChapterDetail(int id)
        {
          if (_context.ChapterDetails == null)
          {
              return NotFound();
          }
            var chapterDetail = await _context.ChapterDetails.FindAsync(id);

            if (chapterDetail == null)
            {
                return NotFound();
            }

            return chapterDetail;
        }

        // PUT: api/ChapterDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapterDetail(int id, ChapterDetail chapterDetail)
        {
            if (id != chapterDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(chapterDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterDetailExists(id))
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

        // POST: api/ChapterDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChapterDetail>> PostChapterDetail(ChapterDetail chapterDetail)
        {
          if (_context.ChapterDetails == null)
          {
              return Problem("Entity set 'ElearningDbContext.ChapterDetails'  is null.");
          }
            _context.ChapterDetails.Add(chapterDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChapterDetail", new { id = chapterDetail.Id }, chapterDetail);
        }

        // DELETE: api/ChapterDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapterDetail(int id)
        {
            if (_context.ChapterDetails == null)
            {
                return NotFound();
            }
            var chapterDetail = await _context.ChapterDetails.FindAsync(id);
            if (chapterDetail == null)
            {
                return NotFound();
            }

            _context.ChapterDetails.Remove(chapterDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChapterDetailExists(int id)
        {
            return (_context.ChapterDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
