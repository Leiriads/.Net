using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
