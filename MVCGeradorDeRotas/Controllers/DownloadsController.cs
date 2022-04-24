using Microsoft.AspNetCore.Mvc;

namespace MVCGeradorDeRotas.Controllers
{
    public class DownloadsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
