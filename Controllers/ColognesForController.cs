using Microsoft.AspNetCore.Mvc;

namespace CologneStore.Controllers
{
	public class ColognesForController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
