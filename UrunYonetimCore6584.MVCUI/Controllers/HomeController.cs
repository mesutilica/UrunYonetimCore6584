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
        private readonly IService<Contact> _serviceContact;
        public HomeController(ILogger<HomeController> logger, IProductService service, IService<Brand> serviceBrand, IService<Contact> serviceContact)
        {
            _logger = logger;
            _service = service;
            _serviceBrand = serviceBrand;
            _serviceContact = serviceContact;
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

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _serviceContact.AddAsync(contact);
                    await _serviceContact.SaveAsync();
                    // await MailHelper.SendMailAsync(contact);
                    TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Mesajınız Gönderilmiştir.!</strong>  Teşekkür Ederiz..
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                    return RedirectToAction("ContactUs");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(contact);
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