using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


// Não é bom que haja mais do quê 6 Action em cada controller.
namespace GerenciadorDeEquipamentos.Controllers
{
    public class LoginController : Controller
    {
        shield01Entities bd = new shield01Entities();
        // GET: Login
        [HttpGet]
        public ActionResult Login(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Login inválido";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(DataLogin pessoas)
        {
            if (bd.Pessoas.Any(x => x.Email == pessoas.Email && x.Senha == pessoas.Senha))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login", new { status = 1 });
            }
        }

    }
}