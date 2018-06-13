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
        IHttpContextAccessor _httpContextAccessor;
        public TransacaoController(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAccessor = httpContextAcessor;
        }
        public IActionResult Index()
        {
            TransacaoModel objTransacao = new TransacaoModel(_httpContextAccessor);
            ViewBag.ListaConta = objTransacao.ListaTransacao();

            return View();
        }

        [HttpGet]
        public IActionResult Registrar(int? id)
        {
            var transacao = new TransacaoModel();
            ViewBag.ListaContas = new ContaModel(_httpContextAccessor).ListaConta();
            ViewBag.ListaPlanoContas = new PlanoContaModel(_httpContextAccessor).ListaPlanoConta();

            if (id != null)
            {
                transacao._httpContextAccessor = _httpContextAccessor;
                return View(transacao.CarregaRegistro(id ?? 0));
            }
            return View(transacao);
        }

        [HttpPost]
        public IActionResult Registrar(TransacaoModel transacao)
        {
            if (ModelState.IsValid)
            {
                transacao._httpContextAccessor = _httpContextAccessor;
                if (transacao.Id == 0)
                    transacao.Insert();
                else
                    transacao.Update();
                return RedirectToAction("Index", "Transacao");
            }
            ViewBag.ListaContas = new ContaModel(_httpContextAccessor).ListaConta();
            ViewBag.ListaPlanoContas = new PlanoContaModel(_httpContextAccessor).ListaPlanoConta();
            return View();
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            var transacao = new TransacaoModel(_httpContextAccessor);
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

            transacao._httpContextAccessor = _httpContextAccessor;
            ViewBag.ListaTransacao = transacao.ListaTransacao();
            ViewBag.ListaContas = new ContaModel(_httpContextAccessor).ListaConta();
            return View(transacao);
        }

        [HttpPost]
        public IActionResult Extrato(TransacaoModel transacao)
        {
            transacao._httpContextAccessor = _httpContextAccessor;
            ViewBag.ListaTransacao = transacao.ListaTransacao();
            ViewBag.ListaContas = new ContaModel(_httpContextAccessor).ListaConta();
            return View();
        }

        public IActionResult Dashboard()
        {
            string id_usuario_id = _httpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var lista = new DashboardModel().RetornarDadosGraficoPie(id_usuario_id);

            var valores = string.Empty;
            var labels = string.Empty;
            var cores = string.Empty;
            foreach (var item in lista)
            {
                valores += item.Total.ToString() + ",";
                labels += "'" + item.PlanoConta.ToString() + "',";
                cores += "'" + String.Format("#{0:X6}", new Random().Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;
            return View();
        }
    }
}