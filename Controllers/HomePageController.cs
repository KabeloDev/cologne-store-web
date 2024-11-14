using Microsoft.AspNetCore.Mvc;

namespace CologneStore.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
