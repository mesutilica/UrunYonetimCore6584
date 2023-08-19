using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.MVCUI.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly IService<Category> _service;

        public Categories(IService<Category> service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _service.GetAllAsync(c => c.IsActive));
        }
    }
}
