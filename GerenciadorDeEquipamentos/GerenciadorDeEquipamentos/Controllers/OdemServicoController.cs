using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class OdemServicoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarOrdemServicos(int? ManutencaoId)
        {
            if (ManutencaoId != null)
            {
                var ordemservico = bd.Manutencao.Where(x => x.ManutencaoId == ManutencaoId);
                return View(ordemservico);
            }
            else
            {
                var ordemservico = bd.Manutencao.ToList();
                return View(ordemservico);
            }
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarOrdemServicos()
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarOrdemServicos(Manutencao manutencao, int ManutencaoId)
        {
            manutencao.ManutencaoId = Convert.ToInt32(HttpContext.User.Identity.Name);

            bd.Manutencao.Add(manutencao);
            bd.SaveChanges();
            return RedirectToAction("ListarOrdemServico", "OrdemServico");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarOrdemServico(int ManutencaoId)
        {
            var manutencao = bd.Manutencao.FirstOrDefault(x => x.ManutencaoId == ManutencaoId);

            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View(manutencao);
        }
        //===============================================================================================
        [HttpPost]
        public ActionResult EditarEquipamentos(Equipamentos equipamento)
        {
            var manutencaoBD = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == equipamento.EquipamentoId);

            //manutencaoBD.DataAquisicao = equipamento.DataAquisicao;
            //manutencaoBD.DataGarantia = equipamento.DataGarantia;
            //manutencaoBD.NumeroPatrimonial = equipamento.NumeroPatrimonial;
            //manutencaoBD.ServiceTagSerial = equipamento.ServiceTagSerial;
            //manutencaoBD.Observacao = equipamento.Observacao;
            //manutencaoBD.DepartamentoId = equipamento.DepartamentoId;
            //manutencaoBD.StatusId = equipamento.StatusId;

            bd.Entry(manutencaoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarEquipamentos", "Equipamento");
        }
        //===============================================================================================
    }
}