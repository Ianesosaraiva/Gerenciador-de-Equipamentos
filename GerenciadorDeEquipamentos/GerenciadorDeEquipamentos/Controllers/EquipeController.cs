using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class EquipeController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize(Roles = "Administrador")]
        public ActionResult ListarEquipes()
        {
            var equipes = bd.Equipe.ToList();
            return View(equipes);
        }
        //===============================================================================================
        
        [Authorize(Roles = "Administrador")]
        public ActionResult CriarEquipe()
        {
            ViewBag.responsavel = new SelectList(bd.Pessoas.Where(x => x.StatusId != 2 && x.AcessoId == 2).ToList(), "PessoaId", "NomeCompleto");
            return View();
        }

        [HttpPost]
        public ActionResult CriarEquipe(Equipe equipe)
        {
            bd.Equipe.Add(equipe);
            bd.SaveChanges();
            return RedirectToAction("ListarEquipes");
        }

        //===============================================================================================

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult EditarEquipe(int equipeId)
        {
            ViewBag.responsavel = new SelectList(bd.Pessoas.Where(x => x.StatusId != 2 && x.AcessoId == 2).ToList(), "PessoaId", "NomeCompleto");
            var equipe = bd.Equipe.FirstOrDefault(x => x.EquipeId == equipeId);

            return View(equipe);
        }

        [HttpPost]
        public ActionResult EditarEquipe(Equipe equipe)
        {
            var equipeBD = bd.Equipe.FirstOrDefault(x => x.EquipeId == equipe.EquipeId);

            equipeBD.Nome = equipe.Nome;
            equipeBD.ResponsavelId = equipe.ResponsavelId;
            equipeBD.Descricao = equipe.Descricao;

            bd.Entry(equipeBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarEquipes");
        }

        //===============================================================================================
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public ActionResult DetalhesEquipe(int EquipeId)
        {
            var detalhes = bd.Equipe.FirstOrDefault(x => x.EquipeId == EquipeId);

            return View(detalhes);
        }
    }
}