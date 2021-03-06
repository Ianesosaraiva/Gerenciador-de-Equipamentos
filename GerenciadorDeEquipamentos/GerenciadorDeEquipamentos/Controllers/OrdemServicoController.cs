﻿using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class OrdemServicoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();

        [Authorize(Roles = ("Administrador, Funcionário"))]
        public ActionResult ListarOrdemServico(int? tipo, int? status)
        {
            try
            {
                if (status == 1)
                {
                    ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
                }

                Session["tipo"] = tipo;
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
            catch
            {
                return RedirectToAction("ListarOrdemServico", new { status = 1 });
            }

        }


        [Authorize(Roles = ("Administrador, Funcionário"))]
        public ActionResult GerenciarOS()
        {
            ViewBag.dadosIndex = bd.vw_dados_index_os.FirstOrDefault();
            ViewBag.NaoAtribuidos = bd.OrdemServico.Where(x => x.EquipeId == null).Count();

            ViewBag.osEquipe = bd.vw_equipe_ordemServico.ToList();
            ViewBag.osColaborador = bd.vw_colaborador_tarefas.ToList();

            int PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);

            //if (HttpContext.User.IsInRole("Funcionário")){

            //    var EquipeId = bd.Pessoas.FirstOrDefault(x => x.PessoaId == PessoaId).EquipeId;
            //    var colaboradores = bd.Pessoas.Where(x => x.EquipeId == EquipeId).ToList();

            //    List<vw_colaborador_tarefas> dadosColaboradores = new List<vw_colaborador_tarefas>();

            //    foreach(var item in colaboradores)
            //    {
            //        dadosColaboradores.Add(bd.vw_colaborador_tarefas.FirstOrDefault(x => x.Colaborador == item.NomeCompleto));
            //    }

            //    ViewBag.osColaborador = dadosColaboradores;
            //}

            return View();
        }

        public ActionResult GetLineChartData()
        {
            List<vw_tarefas_encerradas_por_mes> data = new List<vw_tarefas_encerradas_por_mes>();


            data = bd.vw_tarefas_encerradas_por_mes.ToList();

            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]
            {
                "Mês",
                "Quantidade"
            };

            int i = 0;

            foreach (var item in data)
            {
                i++;
                chartData[i] = new object[] { item.Mes.ToString(), item.Quantidade };
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPieChartData()
        {
            List<vw_os_status_dados> data = new List<vw_os_status_dados>();


            data = bd.vw_os_status_dados.ToList();


            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]
            {
                "Status",
                "Quantidade"
            };



            int i = 0;

            foreach (var item in data)
            {
                i++;
                chartData[i] = new object[] { item.Status.ToString(), item.Quantidade };
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);

        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarOrdemServico()
        {
            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 2).ToList(), "StatusId", "Descricao");
            ViewBag.equipe = new SelectList(bd.Equipe.ToList(), "EquipeId", "Nome");

            ViewBag.tipo = Session["tipo"];
            return View();
        }

        [HttpPost]
        public ActionResult CriarOrdemServico(OrdemServico ordemServico)
        {
            ordemServico.DataAbertura = DateTime.Now;

            EnviarEmail(ordemServico);

            bd.OrdemServico.Add(ordemServico);
            bd.SaveChanges();

            int tipo = Convert.ToInt32(Session["tipo"]);
            return RedirectToAction("ListarOrdemServico", new { tipo = tipo });
        }

        //===============================================================================================

        public ActionResult AbrirChamado()
        {
            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 2).ToList(), "StatusId", "Descricao");
            ViewBag.equipe = new SelectList(bd.Equipe.ToList(), "EquipeId", "Nome");

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

                EnviarEmail(ordemServico);

                return RedirectToAction("Sucesso", new { status = 1 });
            }
            catch
            {
                return RedirectToAction("Sucesso", new { status = 2 });
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditarOrdemServico(int OrdemServicoId, int? message)
        {
            if (message == 1)
            {
                ViewBag.messagem = "Não é possível finalizar OS com tarefa associada aberta";
            }

            ViewBag.tipoSolicitacao = new SelectList(bd.TipoSolicitacao.ToList(), "TipoSolicitacaoId", "Titulo");
            ViewBag.prioridade = new SelectList(bd.Prioridade.ToList(), "PrioridadeId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 2).ToList(), "StatusId", "Descricao");
            ViewBag.equipe = new SelectList(bd.Equipe.ToList(), "EquipeId", "Nome");
            var ordemServico = bd.OrdemServico.FirstOrDefault(x => x.OrdemServicoId == OrdemServicoId);

            ViewBag.tipo = Session["tipo"];
            return View(ordemServico);
        }

        [HttpPost]
        public ActionResult EditarOrdemServico(OrdemServico ordemServico)
        {
            var OrdemServicoBD = bd.OrdemServico.FirstOrDefault(x => x.OrdemServicoId == ordemServico.OrdemServicoId);

            if (bd.Tarefa.Any(x => x.OrdemServicoId == OrdemServicoBD.OrdemServicoId && x.StatusId != 6))
            {
                return RedirectToAction("EditarOrdemServico", new { OrdemServicoId = ordemServico.OrdemServicoId, message = 1 });
            }
            else
            {

                if (ordemServico.StatusId != 6 && OrdemServicoBD.StatusId == 6)
                {
                    OrdemServicoBD.DataEncerramento = null;
                }

                if (ordemServico.StatusId != OrdemServicoBD.StatusId)
                {
                    EnviarEmail(ordemServico);
                }

                OrdemServicoBD.Titulo = ordemServico.Titulo;
                OrdemServicoBD.ClienteEmail = ordemServico.ClienteEmail;
                OrdemServicoBD.ClienteNome = ordemServico.ClienteNome;
                OrdemServicoBD.Descricao = ordemServico.Descricao;
                OrdemServicoBD.StatusId = ordemServico.StatusId;
                OrdemServicoBD.PrioridadeId = ordemServico.PrioridadeId;
                OrdemServicoBD.EquipeId = ordemServico.EquipeId;
                OrdemServicoBD.TipoSolicitacaoId = ordemServico.TipoSolicitacaoId;
                OrdemServicoBD.DataEncerramento = ordemServico.DataEncerramento;


                if (ordemServico.StatusId == 6)
                {
                    OrdemServicoBD.DataEncerramento = DateTime.Now;
                }


                bd.Entry(OrdemServicoBD).State = EntityState.Modified;
                bd.SaveChanges();

                int tipo = Convert.ToInt32(Session["tipo"]);
                return RedirectToAction("ListarOrdemServico", new { tipo = tipo });
            }
        }

        public bool EnviarEmail(OrdemServico ordemServico)
        {
            try
            {
                string[] nome = ordemServico.ClienteNome.Split(' ');
                string status = bd.Status.FirstOrDefault(x => x.StatusId == ordemServico.StatusId).Descricao;
                string titulo = ordemServico.Titulo;
                string mensagem = System.IO.File.ReadAllText(Server.MapPath("~/Content/EmailOrdemServico.html"));
                mensagem = mensagem.Replace("%%USUARIO%%", nome[0])
                                    .Replace("%%STATUS%%", status)
                                    .Replace("%%TITULO%%", titulo);

                Funcoes.Funcoes.EnviaEmail(1, ordemServico.ClienteEmail, mensagem, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ActionResult Resultado(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Seu chamado foi aberto! " +
                    "Está aguardando atendimento. Acompanhe o chamado por e-mail.";
            }
            else if (status == 2)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
            }
            return View();
        }

        //===============================================================================================

    }
}