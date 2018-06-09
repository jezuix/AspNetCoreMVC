using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [HttpGet]
        public IActionResult Index(HomeModel formulario)
        {
            CarregarListaDados();
            return View();
        }

        public void CarregarListaDados()
        {
            HomeModel objHomeModel = new HomeModel();

            ViewBag.Lista = objHomeModel.GetAll();
        }
    }
}