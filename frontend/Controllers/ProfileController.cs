using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PersonalHealthcareExpense.Web.Services;
using PersonalHealthcareExpense.Web.ViewModels;

namespace PersonalHealthcareExpense.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApiService _api;
        private readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

        public ProfileController(ApiService api) => _api = api;

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");

            var response = await _api.GetAsync("api/Users/profile");
            if (!response.IsSuccessStatusCode) return View(new ProfileViewModel());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ProfileViewModel>(json, _json);
            return View(data ?? new ProfileViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProfileViewModel model)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Account");

            var content = new StringContent(
                JsonSerializer.Serialize(new
                {
                    model.FullName,
                    model.PhoneNumber,
                    model.DateOfBirth,
                    model.Gender
                }),
                System.Text.Encoding.UTF8, "application/json");

            var response = await _api.PutAsync("api/Users/profile", content);
            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("UserName", model.FullName);
                TempData["Success"] = "Profile updated successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to update profile.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
