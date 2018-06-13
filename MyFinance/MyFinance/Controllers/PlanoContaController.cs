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
        IHttpContextAccessor _httpContextAccessor;
        public PlanoContaController(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAccessor = httpContextAcessor;
        }
        public IActionResult Index()
        {
            PlanoContaModel objPlanoConta = new PlanoContaModel(_httpContextAccessor);
            ViewBag.ListaConta = objPlanoConta.ListaPlanoConta();

            return View();
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            var planoConta = new PlanoContaModel();
            if (id != null)
            {
                planoConta._httpContextAccessor = _httpContextAccessor;
                return View(planoConta.CarregaRegistro(id ?? 0));
            }
            return View(planoConta);
        }

        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel planoConta)
        {
            if (ModelState.IsValid)
            {
                planoConta._httpContextAccessor = _httpContextAccessor;
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
            var conta = new PlanoContaModel(_httpContextAccessor);
            conta.Excluir(id);
            return RedirectToAction("CriarConta", "Conta");
        }
    }
}