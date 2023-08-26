using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.MVCUI.Controllers
{
    public class BrandController : Controller
    {
        private readonly IService<Brand> _service;
        private readonly IService<Product> _serviceProduct;

        public BrandController(IService<Brand> service, IService<Product> serviceProduct)
        {
            _service = service;
            _serviceProduct = serviceProduct;
        }

        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = await _service.FindAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            model.Products = await _serviceProduct.GetAllAsync(p => p.BrandId == model.Id && p.IsActive);
            return View(model);
        }
    }
}
