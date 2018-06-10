using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {
        IHttpContextAccessor _httpContextAcessor;
        public PlanoContaController(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;
        }
        public IActionResult Index()
        {
            PlanoContaModel objPlanoConta = new PlanoContaModel(_httpContextAcessor);
            ViewBag.ListaConta = objPlanoConta.ListaPlanoConta();

            return View();
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            var planoConta = new PlanoContaModel();
            if (id != null)
            {
                planoConta._httpContextAccessor = _httpContextAcessor;
                return View(planoConta.CarregaRegistro(id ?? 0));
            }
            return View(planoConta);
        }

        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel planoConta)
        {
            if (ModelState.IsValid)
            {
                planoConta._httpContextAccessor = _httpContextAcessor;
                if (planoConta.Id == 0)
                    planoConta.Insert();
                else
                    planoConta.Update();
                return RedirectToAction("Index", "PlanoConta");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var conta = new ContaModel(_httpContextAcessor);
            conta.Excluir();
            return RedirectToAction("CriarConta", "Conta");
        }
    }
}