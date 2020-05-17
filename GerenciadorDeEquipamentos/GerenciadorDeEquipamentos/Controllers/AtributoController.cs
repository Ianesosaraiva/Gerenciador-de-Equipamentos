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
        [Authorize(Roles = ("Administrador, Funcionário"))]
        public ActionResult ListarAtributos()
        {
            var atributos = bd.Atributos.ToList();
            return View(atributos);
        }
        //===============================================================================================

        [Authorize(Roles = ("Administrador, Funcionário"))]
        public ActionResult CriarAtributo()
        {
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult CriarAtributo(Atributos atributo)
        {
            bd.Atributos.Add(atributo);
            bd.SaveChanges();
            return RedirectToAction("ListarAtributos", "Atributo");
        }

        //===============================================================================================

        [Authorize(Roles = ("Administrador, Funcionário"))]
        [HttpGet]
        public ActionResult EditarAtributo(int AtributoId)
        {
            var atributo = bd.Atributos.FirstOrDefault(x => x.AtributoId == AtributoId);

            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");

            return View(atributo);
        }

        [HttpPost]
        public ActionResult EditarAtributo(Atributos atributo)
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

        [HttpGet]
        [Authorize(Roles = ("Administrador, Funcionário"))]
        public ActionResult DetalhesAtributo(int AtributoId)
        {
            var detalhes = bd.Atributos.FirstOrDefault(x => x.AtributoId == AtributoId);

            return View(detalhes);
        }
    }
}