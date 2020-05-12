using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class ManutencaoController : Controller
    {
        // GET: Manutencao
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarManutencoesEquipamento(int EquipamentoId, int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação, por favor tente novamente";
            }
            else if (status == 2)
            {
                ViewBag.message = "Não foi possivel colocar esse equipamento em manutenção pois " +
                    "consta que o mesmo já está em manutenção";
            }
            var equipamento = bd.Manutencao.Where(x => x.EquipamentoId == EquipamentoId).ToList();
            ViewBag.EquipamentoId = EquipamentoId;
            return View(equipamento);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AdicionarEquipamentoManutencao(int EquipamentoId)
        {
            Session["EquipamentoId"] = EquipamentoId;
            return View();
        }

        [HttpPost]
        public ActionResult AdicionarEquipamentoManutencao(Manutencao manutencao)
        {
            try
            {
                var equipamento = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == manutencao.EquipamentoId);

                if (equipamento.StatusId == 9)
                {
                    return RedirectToAction("ListarManutencoesEquipamento", new { EquipamentoId = manutencao.EquipamentoId, status = 2 });
                }
                else
                {
                    manutencao.EquipamentoId = Convert.ToInt32(Session["EquipamentoId"]);
                    manutencao.DataEntrada = DateTime.Now;

                    equipamento.StatusId = 9;

                    bd.Entry(equipamento).State = EntityState.Modified;

                    bd.Manutencao.Add(manutencao);
                    bd.SaveChanges();

                    return RedirectToAction("ListarManutencoesEquipamento", new { EquipamentoId = manutencao.EquipamentoId });
                }
            }
            catch
            {
                return RedirectToAction("ListarManutencoesEquipamento", new { EquipamentoId = manutencao.EquipamentoId, status = 1 });
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditarManutencaoEquipamento(int ManutencaoId)
        {
            var manutencaoBD = bd.Manutencao.FirstOrDefault(x => x.ManutencaoId == ManutencaoId);
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 || x.Tipo == 3).ToList(), "StatusId", "Descricao");

            return View(manutencaoBD);
        }

        [HttpPost]
        public ActionResult EditarManutencaoEquipamento(Manutencao manutencao)
        {
            try
            {
                var manutencaoBD = bd.Manutencao.FirstOrDefault(x => x.ManutencaoId == manutencao.ManutencaoId);

                if (manutencao.Equipamentos.StatusId != manutencaoBD.Equipamentos.StatusId)
                {
                    var equipamento = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == manutencaoBD.EquipamentoId);
                    equipamento.StatusId = manutencao.Equipamentos.StatusId;
                    bd.Entry(equipamento).State = EntityState.Modified;
                }
                manutencaoBD.DataEntrada = manutencao.DataEntrada;
                manutencaoBD.DataSaida = manutencao.DataSaida;
                manutencaoBD.ComoEntrou = manutencao.ComoEntrou;
                manutencaoBD.ComoSaiu = manutencao.ComoSaiu;
                manutencaoBD.Observacao = manutencao.Observacao;

                bd.Entry(manutencaoBD).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarManutencoesEquipamento", new { EquipamentoId = manutencaoBD.EquipamentoId });
            }
            catch
            {
                return RedirectToAction("ListarManutencoesEquipamento", new { EquipamentoId = manutencao.EquipamentoId, status = 1 });
            }

        }
    }
}