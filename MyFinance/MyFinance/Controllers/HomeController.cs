using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class HomeController : Controller
    {
        IHttpContextAccessor _httpContextAcessor;
        public HomeController(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;
        }

        public IActionResult Index()
        {
            string id_usuario_id = _httpContextAcessor.HttpContext.Session.GetString("IdUsuarioLogado");

            if (!string.IsNullOrEmpty(id_usuario_id))
                return RedirectToAction("Menu", "Home");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Ajuda()
        {
            return View();
        }
    }
}
