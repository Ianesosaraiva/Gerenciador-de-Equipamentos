using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class HomeController : Controller
    {
        shield01Entities bd = new shield01Entities();
        [Authorize]
        // GET: Home
        public ActionResult Index()
        {
            int p = Convert.ToInt32(HttpContext.User.Identity.Name);

            ViewBag.usuario = bd.Pessoas.FirstOrDefault(x => x.PessoaId == p).Nome_Completo.Split(' ')[0];
            ViewBag.qtdUsuarios = bd.Pessoas.Where(x=>x.StatusId == 1).Count();
            ViewBag.qtdEquipamentos = bd.Equipamentos.Where(x=>x.StatusId == 1).Count();

            return View();
        }

    }
}
