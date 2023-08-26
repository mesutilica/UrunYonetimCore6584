using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.MVCUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync(p => p.IsActive));
        }

        public async Task<IActionResult> DetailAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var product = await _service.GetProductByIncludeAsync(id.Value);
            if (product is null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> SearchAsync(string q)
        {
            if (!string.IsNullOrWhiteSpace(q))
            {
                var model = await _service.GetAllProductsByIncludeAsync(p => p.Name.Contains(q));
                return View(model);
            }
            return BadRequest();
        }
    }
}
