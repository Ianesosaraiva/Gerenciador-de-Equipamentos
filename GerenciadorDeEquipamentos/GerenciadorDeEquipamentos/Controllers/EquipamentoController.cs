using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASEC.Controllers
{
    public class EquipamentoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarEquipamentos(int? TipoEquipamentoId)
        {
            if (TipoEquipamentoId != null)
            {
                var equipamentos = bd.Equipamentos.Where(x => x.TipoEquipamentoId == TipoEquipamentoId);
                return View(equipamentos);
            }
            else
            {
                var equipamentos = bd.Equipamentos.ToList();
                return View(equipamentos);
            }
        }

        [Authorize]
        public ActionResult CriarEquipamentos(int TipoEquipamentoId)
        {
            ViewBag.tipoEquipamentoId = TipoEquipamentoId;
            return View();
        }

        [HttpPost]
        public ActionResult CriarEquipamentos(Equipamentos equipamento, int TipoEquipamentoId)
        {
            equipamento.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
            equipamento.StatusId = 1;

            bd.Equipamentos.Add(equipamento);
            bd.SaveChanges();
            return RedirectToAction("ListarEquipamentos", "Equipamentos");
        }

        [Authorize]
        public ActionResult ListarEquipamentoEspecificacao(int EquipamentoId)
        {
            var especificacaoEquipamento = bd.EspecificacaoEquipamento.Where(x => x.EquipamentoId == EquipamentoId);
            return View(especificacaoEquipamento);
        }

        [Authorize]
        public ActionResult AdicionarEquipamentoEspecs(int EquipamentoId, int AtributoId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarEquipamentoEspecs(EspecificacaoEquipamento especificacaoEquipamento)
        {


            return View();
        }
    }
}