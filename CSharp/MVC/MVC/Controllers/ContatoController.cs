using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
