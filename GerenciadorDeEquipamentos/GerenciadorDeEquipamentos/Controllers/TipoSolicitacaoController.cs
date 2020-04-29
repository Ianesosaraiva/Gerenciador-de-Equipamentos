using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class TipoSolicitacaoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarTipoSolicitacoes()
        {
            var tipoSolicitacoes = bd.TipoSolicitacao.ToList();
            return View(tipoSolicitacoes);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarTipoSolicitacoes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarTipoSolicitacoes(TipoSolicitacao tipoSolicitacao)
        {
            bd.TipoSolicitacao.Add(tipoSolicitacao);
            bd.SaveChanges();
            return RedirectToAction("ListarTipoSolicitacao");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarTipoSolicitacoes(int TipoSolicitacaoId)
        {
            var tipoSolicitacao = bd.TipoSolicitacao.FirstOrDefault(x => x.TipoSolicitacaoId == TipoSolicitacaoId);

            return View(tipoSolicitacao);
        }

        [HttpPost]
        public ActionResult EditarTipoSolicitacoes(TipoSolicitacao tipoSolicitacao)
        {
            var tipoSolicitacaoBD = bd.TipoSolicitacao.FirstOrDefault(x => x.TipoSolicitacaoId == tipoSolicitacao.tipoSolicitacao);

            tipoSolicitacaoBD.Titulo = tipoSolicitacao.Titulo;
            tipoSolicitacaoBD.Descricao = tipoSolicitacao.Descricao;

            bd.Entry(tipoSolicitacaoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarDepartamentos");
        }

        //===============================================================================================

    }
}