using Microsoft.AspNetCore.Mvc;

namespace MVCGeradorDeRotas.Controllers
{
	public class EquipesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
