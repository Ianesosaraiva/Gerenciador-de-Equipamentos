using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class PessoaController : Controller
    {
        shield01Entities bd = new shield01Entities();
        
        [HttpGet]
        [Authorize]
        // GET: Pessoa
        public ActionResult ListarPessoas()
        {
            var listar = bd.Pessoas.ToList();
            return View(listar);
        }

        [HttpPost]
        public ActionResult ListarPessoas(int? PessoaId)
        {
            if (PessoaId != null)
            {
                var pessoas = bd.Pessoas.Where(x => x.PessoaId == PessoaId);
                return View(pessoas);
            }
            else
            {
                var pessoas = bd.Pessoas.ToList();
                return View(pessoas);
            }
        }

        // GET: Pessoa/Create
        public ActionResult CadastrarPessoas()
        {
            ViewBag.acesso = new SelectList(bd.Acessos.ToList(), "AcessoId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");

            return View();
        }
        //===============================================================================================
        // POST: Pessoa/Create
        [HttpPost]
        public ActionResult CadastrarPessoas(Pessoas pessoas)
        {
            try
            {
                pessoas.DataCadastro = DateTime.Now;
                bd.Pessoas.Add(pessoas);
                bd.SaveChanges();
                return RedirectToAction("ListarPessoas");
            }
            catch
            {
                return RedirectToAction("ListarPessoas", "Home");
            }
        }
        //===============================================================================================
        // GET: Pessoa/Edit/5
        [HttpGet]
        public ActionResult EditarPessoas(int PessoaId)
        {
            Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);
            ViewBag.acesso = new SelectList(bd.Acessos.ToList(), "AcessoId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");

            return View(pessoas);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult EditarPessoas(Pessoas pessoas )
        {
            try
            {

                bd.Entry(pessoas).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarPessoas");
            }
            catch
            {
                return RedirectToAction("EditarPessoas");
            }
        }
        //===============================================================================================

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult DeletarPessoa(int PessoaId)
        {
            try
            {
                Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);
                bd.Entry(pessoas).State = EntityState.Deleted;
                bd.SaveChanges();

                return RedirectToAction("ListarPessoas");
            }
            catch
            {
                return RedirectToAction("EditarPessoas", "Home");
            }
        }
        //===============================================================================================
    }
}
