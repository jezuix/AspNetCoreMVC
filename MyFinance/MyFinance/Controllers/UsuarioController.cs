using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login(int? id)
        {
            if (id != null && id == 0)
                HttpContext.Session.Clear();

            return View();
        }

        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {
            bool login = usuario.ValidarLogin();
            if (login)
            {
                HttpContext.Session.SetString("IdUsuarioLogado", usuario.Id.ToString());
                HttpContext.Session.SetString("NomeUsuarioLogado", usuario.Nome);
                return RedirectToAction("Menu", "Home");
            }

            TempData["MensagemLoginInvalido"] = "Login Inválido";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.RegistrarUsuario();
                return RedirectToAction("Sucesso");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Sucesso()
        {
            return View();
        }
    }
}