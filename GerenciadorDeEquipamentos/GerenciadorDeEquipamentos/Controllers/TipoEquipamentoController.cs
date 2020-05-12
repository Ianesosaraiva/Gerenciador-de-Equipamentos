using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class TipoEquipamentoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarTipoEquipamentos()
        {
            var tipoEquipamentos = bd.TipoEquipamento.ToList();
            return View(tipoEquipamentos);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarTipoEquipamento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarTipoEquipamento(TipoEquipamento tipoEquipamento)
        {
            bd.TipoEquipamento.Add(tipoEquipamento);
            bd.SaveChanges();
            return RedirectToAction("ListarTipoEquipamentos");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarTipoEquipamento(int TipoEquipamentoId)
        {
            var tipoEquipamento = bd.TipoEquipamento.FirstOrDefault(x => x.TipoEquipamentoId == TipoEquipamentoId);

            return View(tipoEquipamento);
        }

        [HttpPost]
        public ActionResult EditarTipoEquipamento(TipoEquipamento tipoEquipamento)
        {
            var tipoEquipamentoBD = bd.TipoEquipamento.FirstOrDefault(x => x.TipoEquipamentoId == tipoEquipamento.TipoEquipamentoId);

            tipoEquipamentoBD.Nome = tipoEquipamento.Nome;

            bd.Entry(tipoEquipamentoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarTipoEquipamentos");
        }

        //===============================================================================================

    }
}