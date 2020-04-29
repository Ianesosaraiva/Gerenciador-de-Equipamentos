using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class AtributoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarAtributos()
        {
            var atributos = bd.Atributos.ToList();
            return View(atributos);
        }
        //===============================================================================================

        [Authorize]
        public ActionResult CriarAtributos()
        {
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult CriarAtributos(Atributos atributo)
        {
            bd.Atributos.Add(atributo);
            bd.SaveChanges();
            return RedirectToAction("ListarAtributos", "Atributo");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarAtributos(int AtributoId)
        {
            var atributo = bd.Atributos.FirstOrDefault(x => x.AtributoId == AtributoId);

            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");

            return View(atributo);
        }

        [HttpPost]
        public ActionResult EditarAtributos(Atributos atributo)
        {
            var atributoBD = bd.Atributos.FirstOrDefault(x => x.AtributoId == atributo.AtributoId);

            atributoBD.Descricao = atributo.Descricao;
            atributoBD.Nome = atributo.Nome;
            atributoBD.TipoEquipamentoId = atributo.TipoEquipamentoId;

            bd.Entry(atributoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarAtributos", "Atributo");
        }

        //===============================================================================================

        public ActionResult DetalhesAtributos(Atributos atributo)
        {
            return View(atributo);
        }
    }
}