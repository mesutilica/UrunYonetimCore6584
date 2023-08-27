using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Data;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IService<Brand> _service;

        public BrandsController(IService<Brand> service)
        {
            _service = service;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            return await _service.GetAllAsync();
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _service.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _service.Update(brand);

            try
            {
                await _service.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem();
            }

            return NoContent();
        }

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            await _service.AddAsync(brand);
            await _service.SaveAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            
            var brand = await _service.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _service.Delete(brand);
            await _service.SaveAsync();

            return NoContent();
        }

    }
}
