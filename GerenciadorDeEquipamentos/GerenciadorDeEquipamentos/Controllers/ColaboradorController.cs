using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class ColaboradorController : Controller
    {
        shield01Entities bd = new shield01Entities();
        
        [HttpGet]
        [Authorize]
        // GET: Pessoa
        public ActionResult ListarColaboradores(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação, por favor tente novamente";
            }

            var lista = bd.Pessoas.ToList();
            return View(lista);
        }

        // GET: Pessoa/Create
        public ActionResult CadastrarColaborador()
        {
            ViewBag.acesso = new SelectList(bd.Acessos.ToList(), "AcessoId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 0).ToList(), "StatusId", "Descricao");

            return View();
        }
        //===============================================================================================
        // POST: Pessoa/Create
        [HttpPost]
        public ActionResult CadastrarColaborador(DataColaborador colaborador)
        {
            try
            {
                colaborador.DataCadastro = DateTime.Now;
                Pessoas pessoa = colaborador;

                bd.Pessoas.Add(pessoa);
                bd.SaveChanges();
                return RedirectToAction("ListarPessoas");
            }
            catch
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("ListarColaboradores", "Colaborador", new { status = 1 });
                }
                else
                {
                    return RedirectToAction("Login", "Login", new { status = 2 });
                }
            }
        }
        //===============================================================================================
        // GET: Pessoa/Edit/5
        [HttpGet]
        public ActionResult EditarColaborador(int PessoaId)
        {
            Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);
            ViewBag.acesso = new SelectList(bd.Acessos.ToList(), "AcessoId", "Descricao");
            ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 0).ToList(), "StatusId", "Descricao");

            return View(pessoas);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        public ActionResult EditarColaborador(Pessoas pessoas )
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

                return RedirectToAction("ListarColaboradores");
            }
            catch
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("ListarColaboradores", "Colaborador", new { status = 1 });
                }
                else
                {
                    return RedirectToAction("Login", "Login", new { status = 2 });
                }
            }
        }
        //===============================================================================================

        // POST: Pessoa/Delete/5
        [HttpPost]
        public ActionResult DeletarColaborador(int PessoaId)
        {
            try
            {
                Pessoas pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == PessoaId);
                pessoas.StatusId = 2;
                bd.Entry(pessoas).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarColaboradores");
            }
            catch
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("ListarColaboradores", "Colaborador", new { status = 1 });
                }
                else
                {
                    return RedirectToAction("Login", "Login", new { status = 2 });
                }
            }
        }
        //===============================================================================================


        [Authorize]
        public ActionResult TrocarSenhaColaborador(int PessoaId)
        {
            try
            {
                var usuario = bd.Pessoas.FirstOrDefault(x => x.PessoaId == PessoaId);
                ViewBag.nome = usuario.NomeCompleto;
                return View(usuario);
            }
            catch
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("ListarColaboradores", "Colaborador", new { status = 1 });
                }
                else
                {
                    return RedirectToAction("Login", "Login", new { status = 2 });
                }
            }
        }

        [HttpPost]
        public ActionResult TrocarSenhaColaborador(DataColaborador colaborador)
        {
            try
            {
                Pessoas pessoa = colaborador;

                var pessoas = bd.Pessoas.FirstOrDefault(pes => pes.PessoaId == pessoa.PessoaId);

                //Log log = new Log
                //{
                //    DadoAnterior = pessoas.Senha,
                //    DadoModificado = pessoa.Senha,
                //    DataLog = DateTime.Now,
                //    NomeTabela = "Pessoas",
                //    PessoaId = pessoa.PessoaId
                //};

                //bd.Log.Add(log);

                pessoas.Senha = pessoa.Senha;

                bd.Entry(pessoas).State = EntityState.Modified;                

                bd.SaveChanges();

                return RedirectToAction("ListarColaboradores");
            }
            catch
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("ListarColaboradores", "Colaborador", new { status = 1 });
                }
                else
                {
                    return RedirectToAction("Login", "Login", new { status = 2 });
                }
            }
        }

        [HttpGet]
        public ActionResult DetalhesColaborador(int PessoaId)
        {
            try
            {
                var detalhes = bd.Pessoas.FirstOrDefault(x => x.PessoaId == PessoaId);

                return View(detalhes);
            }
            catch
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("ListarColaboradores", "Colaborador", new { status = 1 });
                }
                else
                {
                    return RedirectToAction("Login", "Login", new { status = 2 });
                }
            }
        }

    }
}
