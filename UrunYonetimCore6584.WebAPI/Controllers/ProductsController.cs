using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Data;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _context;

        public ProductsController(IProductService context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.GetAllProductsByIncludeAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Update(product);

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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Delete(product);
            await _context.SaveAsync();

            return NoContent();
        }

    }
}
