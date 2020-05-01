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
        public ActionResult ListarOrdemServico(int? tipo)
        {
            if (tipo == 1)
            {
                //Ordens de Serviço aguardando atendimento
                var ordensServicos = bd.OrdemServico.Where(x => x.StatusId == 3).ToList();
                return View(ordensServicos);
            }
            else if (tipo == 2)
            {
                //Ordens de Serviço não atribuidas
                var ordensServicos = bd.OrdemServico.Where(x => x.EquipeId == null).ToList();
                return View(ordensServicos);
            }
            else
            {
                //Todas as Ordens de Serviço
                var ordensServicos = bd.OrdemServico.ToList();
                return View(ordensServicos);
            }
        }
        [Authorize]
        public ActionResult GerenciarOS()
        {
            ViewBag.AguardandoAtendimento = bd.OrdemServico.Where(x => x.StatusId == 3).Count();
            ViewBag.NaoAtribuidos = bd.OrdemServico.Where(x => x.EquipeId == null).Count();
            ViewBag.Total = bd.OrdemServico.ToList().Count();

            ViewBag.osEquipe = bd.vw_equipe_ordemServico.ToList();
            ViewBag.osColaborador = bd.vw_colaborador_OS_tarefas.ToList();

            return View();
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarOrdemServico()
        {
            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1).ToList(), "StatusId", "Descricao");
            ViewBag.equipe = new SelectList(bd.Equipe.ToList(), "EquipeId", "Titulo");

            return View();
        }

        [HttpPost]
        public ActionResult CriarOrdemServico(OrdemServico ordemServico)
        {
            ordemServico.DataAbertura = DateTime.Now;

            bd.OrdemServico.Add(ordemServico);
            bd.SaveChanges();
            return RedirectToAction("ListarOrdemServico");
        }

        //===============================================================================================

        public ActionResult AbrirChamado()
        {
            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1).ToList(), "StatusId", "Descricao");
            ViewBag.equipe = new SelectList(bd.Equipe.ToList(), "EquipeId", "Titulo");

            return View();
        }
        [HttpPost]
        public ActionResult AbrirChamado(OrdemServico ordemServico)
        {
            try
            {
                ordemServico.DataAbertura = DateTime.Now;
                ordemServico.StatusId = 3;

                bd.OrdemServico.Add(ordemServico);
                bd.SaveChanges();
                return RedirectToAction("Sucesso", new { status = 1 });
            }
            catch
            {
                return RedirectToAction("Falha", new { status = 2 });
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditarOrdemServico(int OrdemServicoId)
        {
            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1).ToList(), "StatusId", "Descricao");
            ViewBag.equipe = new SelectList(bd.Equipe.ToList(), "EquipeId", "Titulo");
            var ordemServico = bd.OrdemServico.FirstOrDefault(x => x.OrdemServicoId == OrdemServicoId);

            return View(ordemServico);
        }

        [HttpPost]
        public ActionResult EditarOrdemServico(OrdemServico ordemServico)
        {
            var OrdemServicoBD = bd.OrdemServico.FirstOrDefault(x => x.OrdemServicoId == ordemServico.OrdemServicoId);

            OrdemServicoBD.Titulo = ordemServico.Titulo;
            OrdemServicoBD.ClienteEmail = ordemServico.ClienteEmail;
            OrdemServicoBD.ClienteNome = ordemServico.ClienteNome;
            OrdemServicoBD.Descricao = ordemServico.Descricao;
            OrdemServicoBD.StatusId = ordemServico.StatusId;
            OrdemServicoBD.PrioridadeId = ordemServico.PrioridadeId;
            OrdemServicoBD.EquipeId = ordemServico.EquipeId;
            OrdemServicoBD.TipoSolicitacaoId = ordemServico.TipoSolicitacaoId;
            OrdemServicoBD.DataEncerramento = ordemServico.DataEncerramento;

            bd.Entry(OrdemServicoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarOrdemServico");
        }

        public ActionResult Sucesso(int? status)
        {
            string message;
            if (status == 1)
            {
                message = "Seu chamado foi aberto, está aguardando atendimento. Acompanhe o chamado pelo email";
            }
            else
            {
                message = "Ocorreu um erro";
            }

            ViewBag.message = message;
            return View();
        }

        //===============================================================================================

    }
}