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
        public ActionResult ListarPessoa()
        {
            var listar = bd.Pessoas.ToList();
            return View(listar);
        }

        // POST: Pessoa/List/5
        [HttpPost]
        public ActionResult ListarPessoa(int? PessoaId)
        {
            if (PessoaId != null)
            {
                //var pessoas = bd.Pessoas.Where(x => x.PessoaId == PessoaId);
                Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);

                return View(pessoas);
            }
            else
            {
                var pessoas = bd.Pessoas.ToList();
                return View(pessoas);
            }
        }

        // GET: Pessoa/Create
        public ActionResult CadastrarPessoa()
        {
            ViewBag.Pessoa = new SelectList(bd.Pessoas.ToList(), "PessoaId", "Detalhes");

            return View();
        }
        //===============================================================================================
        // POST: Pessoa/Create
        [HttpPost]
        public ActionResult CadastrarPessoa(Pessoas pessoas)
        {
            try
            {
                bd.Pessoas.Add(pessoas);
                bd.SaveChanges();
                return RedirectToAction("ListarPessoa");
            }
            catch
            {
                return RedirectToAction("CadastrarPessoas", "Home");
            }
        }
        //===============================================================================================
        // GET: Pessoa/Edit/5
        public ActionResult EditarPessoa(int PessoaId)
        {
            Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);
            ViewBag.Pessoas = new SelectList(bd.Pessoas.ToList(), "EditarPessoas", "Detalhes");

            return View(pessoas);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult EditarPessoa(Pessoas pessoas )
        {
            try
            {
                bd.Entry(pessoas).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarPessoas");
            }
            catch
            {
                return RedirectToAction("EditarPessoas", "Home");
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
