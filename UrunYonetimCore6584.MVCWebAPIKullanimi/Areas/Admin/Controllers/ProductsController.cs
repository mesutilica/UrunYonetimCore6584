using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.MVCWebAPIKullanimi.Utils;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/Products/";
        private readonly string _apiMarkaAdres = "https://localhost:7246/api/Brands/";
        private readonly string _apiKategoriAdres = "https://localhost:7246/api/Categories/";
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres);
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            var markalar = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiMarkaAdres);
            var kategoriler = await _httpClient.GetFromJsonAsync<List<Category>>(_apiKategoriAdres);
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product collection, IFormFile? Image, IFormFile? Image2, IFormFile? Image3)
        {
            try
            {
                if (Image is not null)
                    collection.Image = await FileHelper.FileLoaderAsync(Image, "Products/");
                if (Image2 is not null)
                    collection.Image2 = await FileHelper.FileLoaderAsync(Image2, "Products/");
                if (Image3 is not null)
                    collection.Image3 = await FileHelper.FileLoaderAsync(Image3, "Products/");
                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            var markalar = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiMarkaAdres);
            var kategoriler = await _httpClient.GetFromJsonAsync<List<Category>>(_apiKategoriAdres);
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            return View();
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var markalar = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiMarkaAdres);
            var kategoriler = await _httpClient.GetFromJsonAsync<List<Category>>(_apiKategoriAdres);
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + id);
            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Product collection, IFormFile? Image, IFormFile? Image2, IFormFile? Image3)
        {
            try
            {
                if (Image is not null)
                    collection.Image = await FileHelper.FileLoaderAsync(Image, "Products/");
                if (Image2 is not null)
                    collection.Image2 = await FileHelper.FileLoaderAsync(Image2, "Products/");
                if (Image3 is not null)
                    collection.Image3 = await FileHelper.FileLoaderAsync(Image3, "Products/");
                var response = await _httpClient.PutAsJsonAsync(_apiAdres + id, collection);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            var markalar = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiMarkaAdres);
            var kategoriler = await _httpClient.GetFromJsonAsync<List<Category>>(_apiKategoriAdres);
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
            ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
            return View();
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Product collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
