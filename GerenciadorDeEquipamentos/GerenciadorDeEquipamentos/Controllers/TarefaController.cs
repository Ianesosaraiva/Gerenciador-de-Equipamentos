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
        public ActionResult Tarefas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarTarefas(Tarefa tarefa)
        {
            bd.Tarefa.Add(tarefa);
            bd.SaveChanges();
            return RedirectToAction("ListarTarefas");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarTarefas(int TarefaId)
        {
            var tarefa = bd.Tarefa.FirstOrDefault(x => x.TarefaId == TarefaId);

            return View(tarefa);
        }

        [HttpPost]
        public ActionResult EditarTarefa(Tarefa tarefa)
        {
            var tarefaBD = bd.Tarefa.FirstOrDefault(x => x.TarefaId == tarefa.TarefaId);

            tarefaBD.DataAbertura = tarefa.DataAbertura;
            tarefaBD.DataEncerramento = tarefa.DataEncerramento;
            tarefaBD.Descricao = tarefa.Descricao;

            bd.Entry(tarefaBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarTarefas");
        }

        //===============================================================================================

    }
}