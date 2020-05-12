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
        public ActionResult ListarEquipamentos()
        {
            var equipamento = bd.Equipamentos.ToList();
            return View(equipamento);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarEquipamento(int? equipamentoId)
        {
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 && x.Tipo == 3).ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarEquipamento(Equipamentos equipamento)
        {
            equipamento.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
            bd.Equipamentos.Add(equipamento);
            bd.SaveChanges();
            Session["EquipamentoId"] = equipamento.EquipamentoId;

            return RedirectToAction("CriarEquipamentoEspecificacoes", "Equipamento", new { tipoEquipamentoId = equipamento.TipoEquipamentoId });
        }

        [HttpGet]
        public ActionResult CriarEquipamentoEspecificacoes(int? tipoEquipamentoId)
        {
            ViewBag.atributos = bd.Atributos.Where(x => x.TipoEquipamentoId == tipoEquipamentoId);
            var atributos = bd.Atributos.Where(x => x.TipoEquipamentoId == tipoEquipamentoId);
            ViewBag.especificacoes = bd.Especificacoes.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CriarEquipamentoEspecificacoes(int[] especificacacoesId)
        {
            int equipamentoId = Convert.ToInt32(Session["EquipamentoId"]);
            EspecificacaoEquipamento especificacaoEquipamento = new EspecificacaoEquipamento();

            foreach (var item in especificacacoesId)
            {
                especificacaoEquipamento.EspecificacaoId = item;
                especificacaoEquipamento.EquipamentoId = equipamentoId;

                bd.EspecificacaoEquipamento.Add(especificacaoEquipamento);
                bd.SaveChanges();
            }
            return RedirectToAction("ListarEquipamentos", "Equipamento");
        }


        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarEquipamento(int EquipamentoId)
        {
            var equipamento = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == EquipamentoId);

            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 && x.Tipo == 3).ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View(equipamento);
        }
        //===============================================================================================
        [HttpPost]
        public ActionResult EditarEquipamento(Equipamentos equipamento)
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

        [HttpGet]
        public ActionResult EditarEquipamentoEspecificacoes(int tipoEquipamentoId, int equipamentoId)
        {
            //ViewBag.atributos = bd.EspecificacaoEquipamento.
            //    Where(x => x.EquipamentoId == EquipamentoId).Select(x=>x.Especificacoes.Atributos).ToList();

            //ViewBag.especificacoes = bd.EspecificacaoEquipamento.
            //    Where(x => x.EquipamentoId == EquipamentoId).Select(x=>x.Especificacoes).ToList();



            ViewBag.atributos = bd.Atributos.Where(x => x.TipoEquipamentoId == tipoEquipamentoId);
            ViewBag.especificacoes = bd.Especificacoes.ToList();

            var equipamentoEspecificacoes = bd.EspecificacaoEquipamento.FirstOrDefault(x => x.EquipamentoId == equipamentoId);

            return View(equipamentoEspecificacoes);
        }

        [HttpPost]
        public ActionResult EditarEquipamentoEspecificacoes(int[] especificacacoesId)
        {
            int equipamentoId = Convert.ToInt32(Session["EquipamentoId"]);
            EspecificacaoEquipamento especificacaoEquipamento = new EspecificacaoEquipamento();

            foreach (var item in especificacacoesId)
            {
                especificacaoEquipamento.EspecificacaoId = item;
                especificacaoEquipamento.EquipamentoId = equipamentoId;

                bd.EspecificacaoEquipamento.Add(especificacaoEquipamento);
            }
            bd.SaveChanges();
            return RedirectToAction("ListarEquipamentos", "Equipamento");
        }

        //===============================================================================================
        [Authorize]
        public ActionResult ListarEquipamentoEspecificacao(int EquipamentoId)
        {
            var especificacaoEquipamento = bd.EspecificacaoEquipamento.Where(x => x.EquipamentoId == EquipamentoId).ToList();

            return View(especificacaoEquipamento);
        }

        //===============================================================================================

        [HttpGet]
        public ActionResult DetalhesEquipamento(int EquipamentoId)
        {
            var detalhes = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == EquipamentoId);

            return View(detalhes);
        }
    }
}