using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class LicencaController : Controller
    {
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarLicencas(int SoftwareId)
        {
            Session["SoftwareId"] = SoftwareId;
            var licencas = bd.Licencas.Where(x => x.SoftwareId == SoftwareId).ToList();
            return View(licencas);
        }

        [Authorize]
        public ActionResult CadastrarLicenca()
        {
            ViewBag.SoftwareId = Session["SoftwareId"];
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarLicenca(Licencas licenca)
        {
            licenca.SoftwareId = Convert.ToInt32(Session["SoftwareId"]);
            bd.Licencas.Add(licenca);
            bd.SaveChanges();
            return RedirectToAction("ListarLicencas", new { SoftwareId = Convert.ToInt32(Session["SoftwareId"]) });
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarLicenca(int LicencaId)
        {
            ViewBag.SoftwareId = Session["SoftwareId"];
            var licenca = bd.Licencas.FirstOrDefault(x => x.LicencaId == LicencaId);

            return View(licenca);
        }

        [HttpPost]
        public ActionResult EditarLicenca(Licencas licenca)
        {
            var licencaBD = bd.Licencas.FirstOrDefault(x => x.LicencaId == licenca.LicencaId);

            licencaBD.Preco = licenca.Preco;
            licencaBD.Quantidade = licenca.Quantidade;
            licencaBD.SoftwareId = licenca.SoftwareId;

            bd.Entry(licencaBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarLicencas", new { SoftwareId = Convert.ToInt32(Session["SoftwareId"]) });
        }

        [Authorize]
        [HttpDelete]
        public ActionResult DeletarLicenca(int LicencaId)
        {
            var licenca = bd.Licencas.FirstOrDefault(x => x.LicencaId == LicencaId);
            bd.Entry(licenca).State = EntityState.Deleted;
            return RedirectToAction("ListarLicencas", new { SoftwareId = Convert.ToInt32(Session["SoftwareId"]) });
        }
    }
}