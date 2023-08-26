using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Data;

namespace UrunYonetimCore6584.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SlidesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Slides
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slide>>> GetSlides()
        {
            if (_context.Slides == null)
            {
                return NotFound();
            }
            return await _context.Slides.ToListAsync();
        }

        // GET: api/Slides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slide>> GetSlide(int id)
        {
            if (_context.Slides == null)
            {
                return NotFound();
            }
            var slide = await _context.Slides.FindAsync(id);

            if (slide == null)
            {
                return NotFound();
            }

            return slide;
        }

        // PUT: api/Slides/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlide(int id, Slide slide)
        {
            if (id != slide.Id)
            {
                return BadRequest();
            }

            _context.Entry(slide).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlideExists(id))
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

        // POST: api/Slides
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Slide>> PostSlide(Slide slide)
        {
            if (_context.Slides == null)
            {
                return Problem("Entity set 'DatabaseContext.Slides'  is null.");
            }
            _context.Slides.Add(slide);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSlide", new { id = slide.Id }, slide);
        }

        // DELETE: api/Slides/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlide(int id)
        {
            if (_context.Slides == null)
            {
                return NotFound();
            }
            var slide = await _context.Slides.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }

            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SlideExists(int id)
        {
            return (_context.Slides?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
