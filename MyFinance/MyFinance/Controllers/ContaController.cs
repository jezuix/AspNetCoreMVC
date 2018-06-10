using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {
        IHttpContextAccessor _httpContextAcessor;
        public ContaController(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;
        }
        public IActionResult Index()
        {
            ContaModel objConta = new ContaModel(_httpContextAcessor);
            ViewBag.ListaConta = objConta.ListaConta();

            return View();
        }

        [HttpGet]
        public IActionResult CriarConta(int? id)
        {
            var conta = new ContaModel();
            if (id != null)
            {
                conta._httpContextAccessor = _httpContextAcessor;
                return View(conta.CarregaRegistro(id ?? 0));
            }
            return View(conta);
        }

        [HttpPost]
        public IActionResult CriarConta(ContaModel conta)
        {
            if (ModelState.IsValid)
            {
                conta._httpContextAccessor = _httpContextAcessor;
                if (conta.Id == 0)
                    conta.Insert();
                else
                    conta.Update();
                return RedirectToAction("Index","Conta");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var conta = new ContaModel(_httpContextAcessor);
            conta.Excluir();
            return RedirectToAction("CriarConta","Conta");
        }
    }
}