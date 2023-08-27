using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Data;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
        private readonly IService<Slide> _context;

        public SlidesController(IService<Slide> context)
        {
            _context = context;
        }

        // GET: api/Slides
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slide>>> GetSlides()
        {
            return await _context.GetAllAsync();
        }

        // GET: api/Slides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slide>> GetSlide(int id)
        {
            var slide = await _context.FindAsync(id);

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

            _context.Update(slide);

            try
            {
                await _context.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
            }

            return NoContent();
        }

        // POST: api/Slides
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Slide>> PostSlide(Slide slide)
        {
            await _context.AddAsync(slide);
            await _context.SaveAsync();

            return CreatedAtAction("GetSlide", new { id = slide.Id }, slide);
        }

        // DELETE: api/Slides/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlide(int id)
        {
            var slide = await _context.FindAsync(id);
            if (slide == null)
            {
                return NotFound();
            }

            _context.Delete(slide);
            await _context.SaveAsync();

            return NoContent();
        }
    }
}
