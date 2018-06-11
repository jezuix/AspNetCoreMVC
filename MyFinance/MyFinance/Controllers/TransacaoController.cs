using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor _httpContextAcessor;
        public TransacaoController(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;
        }
        public IActionResult Index()
        {
            TransacaoModel objTransacao = new TransacaoModel(_httpContextAcessor);
            ViewBag.ListaConta = objTransacao.ListaTransacao();

            return View();
        }

        [HttpGet]
        public IActionResult Registrar(int? id)
        {
            var transacao = new TransacaoModel();
            ViewBag.ListaContas = new ContaModel(_httpContextAcessor).ListaConta();
            ViewBag.ListaPlanoContas = new PlanoContaModel(_httpContextAcessor).ListaPlanoConta();

            if (id != null)
            {
                transacao._httpContextAccessor = _httpContextAcessor;
                return View(transacao.CarregaRegistro(id ?? 0));
            }
            return View(transacao);
        }

        [HttpPost]
        public IActionResult Registrar(TransacaoModel transacao)
        {
            if (ModelState.IsValid)
            {
                transacao._httpContextAccessor = _httpContextAcessor;
                if (transacao.Id == 0)
                    transacao.Insert();
                else
                    transacao.Update();
                return RedirectToAction("Index", "Transacao");
            }
            ViewBag.ListaContas = new ContaModel(_httpContextAcessor).ListaConta();
            ViewBag.ListaPlanoContas = new PlanoContaModel(_httpContextAcessor).ListaPlanoConta();
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var transacao = new TransacaoModel(_httpContextAcessor);
            transacao.Excluir(id);
            return RedirectToAction("Index", "Transacao");
        }

        [HttpGet]
        public IActionResult Extrato()
        {
            TransacaoModel transacao = new TransacaoModel()
            {
                Tipo = null
            };

            transacao._httpContextAccessor = _httpContextAcessor;
            ViewBag.ListaTransacao = transacao.ListaTransacao();
            ViewBag.ListaContas = new ContaModel(_httpContextAcessor).ListaConta();
            return View(transacao);
        }

        [HttpPost]
        public IActionResult Extrato(TransacaoModel transacao)
        {
            transacao._httpContextAccessor = _httpContextAcessor;
            ViewBag.ListaTransacao = transacao.ListaTransacao();
            ViewBag.ListaContas = new ContaModel(_httpContextAcessor).ListaConta();
            return View();
        }

        public IActionResult Dashboard()
        {
            var lista = new DashboardModel().RetornarDadosGraficoPie();

            var valores = string.Empty;
            var labels = string.Empty;
            var cores = string.Empty;
            foreach (var item in lista)
            {
                valores += item.Total.ToString() + ",";
                labels += "'" + item.PlanoConta.ToString() + "',";
                cores += "'" + String.Format("#{0:X6}", new Random().Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores.Substring(0, valores.Length - 1);
            ViewBag.Labels = labels.Substring(0, labels.Length - 1);
            ViewBag.Cores = cores.Substring(0, cores.Length - 1);
            return View();
        }
    }
}