using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.MVCUI.ViewComponents
{
    public class Slides : ViewComponent
    {
        private readonly IService<Slide> _service;

        public Slides(IService<Slide> service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _service.GetAllAsync());
        }
    }
}
