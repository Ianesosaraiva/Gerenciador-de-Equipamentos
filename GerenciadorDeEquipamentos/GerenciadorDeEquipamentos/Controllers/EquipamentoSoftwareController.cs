using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class EquipamentoSoftwareController : Controller
    {
        shield01Entities bd = new shield01Entities();
        // GET: EquipamentoSoftware
        [Authorize]
        [HttpGet]
        public ActionResult ListarEquipamentoSoftwares(int EquipamentoId)
        {
            try
            {
                var softwaresEquipamento = bd.SoftwareEquipemento.Where(x => x.EquipamentoId == EquipamentoId).ToList();

                return View(softwaresEquipamento);
            }
            catch
            {
                return RedirectToAction("ListarEquipamentos", "Equipamento", new { status = 1 });
            }
        }

        [HttpGet]
        public ActionResult CriarEquipamentoSoftwares(int EquipamentoId, int? status)
        {
            try
            {
                Session["EquipamentoId"] = EquipamentoId;
                ViewBag.EquipamentoId = EquipamentoId;

                if (status == 1)
                {
                    ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
                }

                ViewBag.softwares = bd.Softwares.Where(x => x.StatusId != 2).ToList();

                return View();
            }
            catch
            {
                return RedirectToAction("CriarEquipamento", "Equipamento", new { status = 1 });
            }
        }

        [HttpPost]
        public ActionResult CriarEquipamentoSoftwares(string[] mybox)
        {
            try
            {
                int equipamentoId = Convert.ToInt32(Session["EquipamentoId"]);

                var softwares = bd.Softwares.Where(x => x.StatusId != 2 && mybox.Contains(x.Nome)).ToList();

                SoftwareEquipemento softwareEquipemento = new SoftwareEquipemento();

                foreach (var item in softwares)
                {
                    softwareEquipemento.SoftwareId = item.SoftwareId;
                    softwareEquipemento.EquipamentoId = equipamentoId;
                    softwareEquipemento.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
                    softwareEquipemento.Data = DateTime.Now;

                    bd.SoftwareEquipemento.Add(softwareEquipemento);
                    bd.SaveChanges();
                }

                return RedirectToAction("ListarEquipamentos", "Equipamento");

            }
            catch
            {
                return RedirectToAction("CriarEquipamentoSoftwares", new { status = 1 });
            }

        }
    }
}