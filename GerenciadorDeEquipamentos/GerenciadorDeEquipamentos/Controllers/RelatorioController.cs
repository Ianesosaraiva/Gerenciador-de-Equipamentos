using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class RelatorioController : Controller
    {
        shield01Entities bd = new shield01Entities();
        // GET: Relatorio
        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult RelatoriosIndex()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult RelatorioEquipamentos()
        {
            List<SelectListItem> agrupar = new List<SelectListItem>();
            agrupar.Add(new SelectListItem { Text = "TipoEquipamentoId", Value = "Tipo de Equipamento" });
            agrupar.Add(new SelectListItem { Text = "StatusId", Value = "Status" });
            agrupar.Add(new SelectListItem { Text = "DepartamentoId", Value = "Departamento" });

            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 || x.Tipo == 3).ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");
            ViewBag.garantia = new SelectList(bd.Equipamentos.Select(x => x.DataGarantia).Distinct().ToList(), "DataGarantia", "DataGarantia");
            ViewBag.aquisicao = new SelectList(bd.Equipamentos.Select(x => x.DataAquisicao).Distinct().ToList(), "DataAquisicao", "DataAquisicao");
            ViewBag.agrupar = agrupar;

            var relatorioEquipamentos = Session["relatorioEquipamentos"];
            if (relatorioEquipamentos != null)
            {
                ViewBag.relatorioEquipamentos = relatorioEquipamentos;
            }
            else
            {
                ViewBag.relatorioEquipamentos = new List<Equipamentos>(); 
            }
            return View();
        }

        public ActionResult _RelatorioEquipamentos(List<Equipamentos> equipamento)
        {
            equipamento = new List<Equipamentos>();
            return View(equipamento);
        }

        [HttpPost]
        public ActionResult _RelatorioEquipamentos(RelatorioEquipamentos relatorio)
        {
            var relatorioEquipamentos = bd.Equipamentos.Where(x => x.TipoEquipamentoId == relatorio.TipoEquipamentoId &&
            x.DepartamentoId == relatorio.DepartamentoId && x.StatusId == relatorio.StatusId).ToList();

            Session["relatorioEquipamentos"] = relatorioEquipamentos;

            return RedirectToAction("RelatorioEquipamentos");
        }


        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult RelatorioOrdemServico()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Funcionário")]
        public ActionResult RelatorioColaboradores()
        {
            return View();
        }

    }
}