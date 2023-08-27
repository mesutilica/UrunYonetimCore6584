using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrunYonetimCore6584.Core.Entities;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/AppUsers";
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {
            var userList = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres);
            var account = userList.FirstOrDefault(u => u.IsActive && u.Email == email && u.Password == password);
            if (account is null)
            {
                TempData["Message"] = "<div class='alert alert-danger'>Giriş Başarısız!</div>";
            }
            else
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, account.Name),
                    new Claim(ClaimTypes.Role, account.IsAdmin ? "Admin" : "User"),
                    new Claim("UserId", account.Id.ToString())
                };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal userPrincipal = new(userIdentity);
                await HttpContext.SignInAsync(userPrincipal);
                return Redirect("/Admin");
            }
            return View();
        }

        [Route("/Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
