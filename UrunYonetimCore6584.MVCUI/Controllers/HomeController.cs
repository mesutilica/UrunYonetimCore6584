using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.MVCUI.Models;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _service;
        private readonly IService<Brand> _serviceBrand;
        public HomeController(ILogger<HomeController> logger, IProductService service, IService<Brand> serviceBrand)
        {
            _logger = logger;
            _service = service;
            _serviceBrand = serviceBrand;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel()
            {
                Brands = await _serviceBrand.GetAllAsync(b => b.IsActive),
                Products = await _service.GetAllAsync(p => p.IsActive && p.IsHome)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}