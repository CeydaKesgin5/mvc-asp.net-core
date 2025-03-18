using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]//oturum açan ve Admin rolüne sahip olan kullanıcılar erişim sağlayabilir.

    public class DashboardController :Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
