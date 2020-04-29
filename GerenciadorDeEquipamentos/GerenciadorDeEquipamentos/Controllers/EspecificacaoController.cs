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
        public ActionResult ListarEspecificacoes()
        {
            var especificao = bd.Especificacoes.ToList();
            return View(especificao);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarEspecificacoes()
        {
            ViewBag.atributos = new SelectList(bd.Atributos.ToList(), "AtributoId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarEspecificacoes(Especificacoes especificao)
        {
            especificao.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);

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

            ViewBag.atributos = new SelectList(bd.Atributos.ToList(), "AtributoId", "Nome");

            return View(especificao);
        }

        [HttpPost]
        public ActionResult EditarEspecificacoes(Especificacoes especificao)
        {
            var especificacaoBD = bd.Especificacoes.FirstOrDefault(x => x.EspecificacaoId == especificao.EspecificacaoId);

            especificacaoBD.AtributoId = especificao.AtributoId;
            especificacaoBD.Nome = especificao.Nome;

            bd.Entry(especificacaoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarEspecificacoes", "Especificacao");
        }

        //===============================================================================================

    }
}