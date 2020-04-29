using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class EspecificacaoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarEspecificacoes(int? EspecificacaoId)
        {
            if (EspecificacaoId != null)
            {
                var especificao = bd.Especificacoes.Where(x => x.EspecificacaoId == EspecificacaoId);
                return View(especificao);
            }
            else
            {
                var especificao = bd.Especificacoes.ToList();
                return View(especificao);
            }
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarEspecificacoes()
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarEspecificacoes(Especificacoes especificao, int EspecificacaoId)
        {
            especificao.EspecificacaoId = Convert.ToInt32(HttpContext.User.Identity.Name);

            bd.Especificacoes.Add(especificao);
            bd.SaveChanges();
            return RedirectToAction("ListarEspecificacoes", "Especificacao");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarEspecificacoes(int EspecificacaoId)
        {
            var especificao = bd.Especificacoes.FirstOrDefault(x => x.EspecificacaoId == EspecificacaoId);

            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View(especificao);
        }

        [HttpPost]
        public ActionResult EditarEspecificacoes(Especificacoes especificao)
        {
            var especificacaoBD = bd.Especificacoes.FirstOrDefault(x => x.EspecificacaoId == especificao.EspecificacaoId);

            //especificacaoBD.Atributos = especificacoes.Atributos;
            //especificacaoBD.DataGarantia = departamento.DataGarantia;
            //especificacaoBD.NumeroPatrimonial = departamento.NumeroPatrimonial;
            //especificacaoBD.ServiceTagSerial = departamento.ServiceTagSerial;
            //especificacaoBD.Observacao = departamento.Observacao;
            //especificacaoBD.DepartamentoId = departamento.DepartamentoId;
            //especificacaoBD.StatusId = departamento.StatusId;

            bd.Entry(especificacaoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarEspecificacoes", "Especificacao");
        }

        //===============================================================================================

    }
}