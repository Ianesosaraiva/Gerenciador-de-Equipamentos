using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


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
            }else if(status == 2)
            {
                ViewBag.message = "Acesso Negado";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(DataLogin pessoas)
        {
            var user = bd.Pessoas.FirstOrDefault(x => x.Email == pessoas.Email && x.Senha == pessoas.Senha);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.PessoaId.ToString(), true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Login", new { status = 1 });
            }
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}