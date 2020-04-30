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
        public ActionResult CriarTipoSolicitacao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarTipoSolicitacao(TipoSolicitacao tipoSolicitacao)
        {
            tipoSolicitacao.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
            bd.TipoSolicitacao.Add(tipoSolicitacao);
            bd.SaveChanges();
            return RedirectToAction("ListarTipoSolicitacoes");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarTipoSolicitacao(int TipoSolicitacaoId)
        {
            var tipoSolicitacao = bd.TipoSolicitacao.FirstOrDefault(x => x.TipoSolicitacaoId == TipoSolicitacaoId);

            return View(tipoSolicitacao);
        }

        [HttpPost]
        public ActionResult EditarTipoSolicitacao(TipoSolicitacao tipoSolicitacao)
        {
            var tipoSolicitacaoBD = bd.TipoSolicitacao.FirstOrDefault(x => x.TipoSolicitacaoId == tipoSolicitacao.TipoSolicitacaoId);

            tipoSolicitacaoBD.Titulo = tipoSolicitacao.Titulo;
            tipoSolicitacaoBD.Descricao = tipoSolicitacao.Descricao;

            bd.Entry(tipoSolicitacaoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarTipoSolicitacoes");
        }

        //===============================================================================================

    }
}