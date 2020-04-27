using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
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
        //===============================================================================================
        [Authorize]
        public ActionResult CriarEquipamentos()
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");
          
            return View();
        }

        [HttpPost]
        public ActionResult CriarEquipamentos(Equipamentos equipamento, int TipoEquipamentoId)
        {
            equipamento.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);

            bd.Equipamentos.Add(equipamento);
            bd.SaveChanges();
            return RedirectToAction("ListarEquipamentos", "Equipamento");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarEquipamentos(int EquipamentoId)
        {
            var equipamento = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == EquipamentoId);

            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View(equipamento);
        }

        [HttpPost]
        public ActionResult EditarEquipamentos(Equipamentos equipamento)
        {
            var equipamentoBD = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == equipamento.EquipamentoId);

            equipamentoBD.DataAquisicao = equipamento.DataAquisicao;
            equipamentoBD.DataGarantia = equipamento.DataGarantia;
            equipamentoBD.NumeroPatrimonial = equipamento.NumeroPatrimonial;
            equipamentoBD.ServiceTagSerial = equipamento.ServiceTagSerial;
            equipamentoBD.Observacao = equipamento.Observacao;
            equipamentoBD.DepartamentoId = equipamento.DepartamentoId;
            equipamentoBD.StatusId = equipamento.StatusId;

            bd.Entry(equipamentoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarEquipamentos", "Equipamento");
        }
        //===============================================================================================
        [Authorize]
        public ActionResult ListarEquipamentoEspecificacao(int EquipamentoId)
        {
            var especificacaoEquipamento = bd.EspecificacaoEquipamento.Where(x => x.EquipamentoId == EquipamentoId);
            return View(especificacaoEquipamento);
        }

        //===============================================================================================
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

        //===============================================================================================
    }
}