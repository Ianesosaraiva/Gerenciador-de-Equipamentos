using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class SoftwareController : Controller
    {
        shield01Entities bd = new shield01Entities();
        public ActionResult ListarSoftwares()
        {
            var softwares = bd.Softwares.ToList();
            return View(softwares);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CadastrarSoftwares()
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarSoftwares(Softwares softwares)
        {
            softwares.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);

            bd.Softwares.Add(softwares);
            bd.SaveChanges();
            return RedirectToAction("ListarSoftwares");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarSoftwares(int SoftwareId)
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            var softwares = bd.Softwares.FirstOrDefault(x => x.SoftwareId == SoftwareId);

            return View(softwares);
        }

        [HttpPost]
        public ActionResult EditarSoftwares(Softwares softwares)
        {
            var softwaresBD = bd.Softwares.FirstOrDefault(x => x.SoftwareId == softwares.SoftwareId);

            softwaresBD.Nome = softwares.Nome;
            softwaresBD.Descricao = softwares.Descricao;
            softwaresBD.Observacao = softwares.Observacao;
            softwaresBD.StatusId = softwares.StatusId;

            bd.Entry(softwaresBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarSoftwares");
        }

    }
}