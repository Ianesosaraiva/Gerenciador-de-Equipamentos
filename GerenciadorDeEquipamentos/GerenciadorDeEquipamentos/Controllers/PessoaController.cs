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

        // GET: Pessoa/Create
        public ActionResult CadastrarPessoas()
        {
            ViewBag.acesso = new SelectList(bd.Acessos.ToList(), "AcessoId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 0).ToList(), "StatusId", "Descricao");

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
                return RedirectToAction("ListarPessoas");
            }
        }
        //===============================================================================================
        // GET: Pessoa/Edit/5
        [HttpGet]
        public ActionResult EditarPessoas(int PessoaId)
        {
            Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);
            ViewBag.acesso = new SelectList(bd.Acessos.ToList(), "AcessoId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 0).ToList(), "StatusId", "Descricao");

            return View(pessoas);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult EditarPessoas(Pessoas pessoas )
        {
            try
            {
                var pessoa = bd.Pessoas.FirstOrDefault(x => x.PessoaId == pessoas.PessoaId);

                pessoa.RG = pessoas.RG;
                pessoa.StatusId = pessoas.StatusId;
                pessoa.NomeCompleto = pessoas.NomeCompleto;
                pessoa.DataNascimento = pessoas.DataNascimento;
                pessoa.Contato = pessoas.Contato;
                pessoa.AcessoId = pessoas.AcessoId;
                pessoa.CPF = pessoas.CPF;
                pessoa.Email = pessoas.Email;

                bd.Entry(pessoa).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarPessoas");
            }
            catch
            {
                return RedirectToAction("ListarPessoas");
            }
        }
        //===============================================================================================

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult DeletarPessoas(int PessoaId)
        {
            try
            {
                Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);
                pessoas.StatusId = 2;
                bd.Entry(pessoas).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarPessoas");
            }
            catch
            {
                return RedirectToAction("ListarPessoas");
            }
        }
        //===============================================================================================
    }
}
