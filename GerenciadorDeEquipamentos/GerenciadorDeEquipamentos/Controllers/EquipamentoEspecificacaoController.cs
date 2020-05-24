using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class EquipamentoEspecificacaoController : Controller
    {
        shield01Entities bd = new shield01Entities();

        [Authorize]
        public ActionResult ListarEquipamentoEspecificacao(int EquipamentoId)
        {
            try
            {
                var especificacaoEquipamento = bd.EspecificacaoEquipamento.Where(x => x.EquipamentoId == EquipamentoId).ToList();

                return View(especificacaoEquipamento);
            }
            catch
            {
                return RedirectToAction("ListarEquipamentos", "Equipamento", new { status = 1 });
            }

        }

        [HttpGet]
        public ActionResult CriarEquipamentoEspecificacoes(int? tipoEquipamentoId, int? status, int EquipamentoId)
        {
            Session["EquipamentoId"] = EquipamentoId;
            ViewBag.EquipamentoId = EquipamentoId;

            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
            }
            try
            {
                ViewBag.atributos = bd.Atributos.Where(x => x.TipoEquipamentoId == tipoEquipamentoId);
                ViewBag.especificacoes = bd.Especificacoes.ToList();

                return View();
            }
            catch
            {
                return RedirectToAction("CriarEquipamento", "Equipamento", new { status = 1 });
            }
        }

        [HttpPost]
        public ActionResult CriarEquipamentoEspecificacoes(int[] especificacacoesId)
        {
            try
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

                return RedirectToAction("CriarEquipamentoSoftwares", "EquipamentoSoftware", new { EquipamentoId = equipamentoId});

            }
            catch
            {
                return RedirectToAction("CriarEquipamentoEspecificacoes", new { status = 1 });
            }
        }

        [HttpGet]
        public ActionResult EditarEquipamentoEspecificacoes(int tipoEquipamentoId, int equipamentoId, int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
            }
            try
            {
                ViewBag.atributos = bd.Atributos.Where(x => x.TipoEquipamentoId == tipoEquipamentoId);
                ViewBag.especificacoes = bd.Especificacoes.ToList();

                var equipamentoEspecificacoes = bd.EspecificacaoEquipamento.FirstOrDefault(x => x.EquipamentoId == equipamentoId);

                return View(equipamentoEspecificacoes);
            }
            catch
            {
                return RedirectToAction("ListarEquipamentos", "Equipamento", new { status = 1 });
            }
        }

        [HttpPost]
        public ActionResult EditarEquipamentoEspecificacoes(int[] especificacacoesId)
        {
            try
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
            catch
            {
                return RedirectToAction("ListarEquipamentos", "Equipamento", new { status = 1 });
            }

        }

    }
}