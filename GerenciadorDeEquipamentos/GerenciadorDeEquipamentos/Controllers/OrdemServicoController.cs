using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class OrdemServicoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarOdemServicos()
        {
            var odemServicos = bd.OrdemServico.ToList();
            return View(odemServicos);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarOrdemServico()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarOrdemServicos(OrdemServico odemServico)
        {
            bd.OrdemServico.Add(odemServico);
            bd.SaveChanges();
            return RedirectToAction("ListarodemServicos");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarOrdemServico(int TipoSolicitacaoId)
        {
            var tipoSolicitacao = bd.TipoSolicitacao.FirstOrDefault(x => x.TipoSolicitacaoId == TipoSolicitacaoId);

            return View(tipoSolicitacao);
        }

        [HttpPost]
        public ActionResult EditarOrdemServico(OrdemServico ordemServico)
        {
            var OrdemServicoBD = bd.OrdemServico.FirstOrDefault(x => x.TipoSolicitacaoId == ordemServico.OrdemServicoId);

            OrdemServicoBD.ClienteEmail = ordemServico.ClienteEmail;
            OrdemServicoBD.ClienteNome = ordemServico.ClienteNome;
            OrdemServicoBD.Descricao = ordemServico.Descricao;
            OrdemServicoBD.DataAbertura = ordemServico.DataAbertura;
            OrdemServicoBD.DataEncerramento = ordemServico.DataEncerramento;

            bd.Entry(OrdemServicoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarDepartamentos");
        }

        //===============================================================================================

    }
}