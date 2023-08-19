using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.MVCUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var kategori = await _service.GetCategoryByIncludeAsync(c => c.IsActive && c.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }
    }
}
