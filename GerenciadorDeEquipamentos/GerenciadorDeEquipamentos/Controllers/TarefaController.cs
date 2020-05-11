using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class TarefaController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarTarefas()
        {
            var tarefas = bd.Tarefa.ToList();
            return View(tarefas);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarTarefa()
        {
            ViewBag.ordemServico = new SelectList(bd.OrdemServico.ToList(), "OrdemServicoId", "Titulo");
            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x=>x.Tipo == 1).ToList(), "StatusId", "Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult CriarTarefa(Tarefa tarefa)
        {
            tarefa.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
            tarefa.DataAbertura = DateTime.Now;

            bd.Tarefa.Add(tarefa);
            bd.SaveChanges();
            return RedirectToAction("ListarTarefas");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarTarefa(int TarefaId)
        {
            ViewBag.ordemServico = new SelectList(bd.OrdemServico.ToList(), "OrdemServicoId", "Titulo");
            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1).ToList(), "StatusId", "Descricao");

            var tarefa = bd.Tarefa.FirstOrDefault(x => x.TarefaId == TarefaId);

            return View(tarefa);
        }

        [HttpPost]
        public ActionResult EditarTarefa(Tarefa tarefa)
        {
            var tarefaBD = bd.Tarefa.FirstOrDefault(x => x.TarefaId == tarefa.TarefaId);

            if(tarefa.StatusId != 6 && tarefaBD.StatusId == 6)
            {
                tarefaBD.DataEncerramento = null;
            }

            tarefaBD.TipoSolicitacaoId = tarefa.TipoSolicitacaoId;
            tarefaBD.OrdemServicoId = tarefa.OrdemServicoId;
            tarefaBD.PrioridadeId = tarefa.PrioridadeId;
            tarefaBD.Descricao = tarefa.Descricao;
            tarefaBD.StatusId = tarefa.StatusId;

            if(tarefa.StatusId == 6)
            {
                tarefaBD.DataEncerramento = DateTime.Now;
            }

            bd.Entry(tarefaBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarTarefas");
        }

        //===============================================================================================

    }
}