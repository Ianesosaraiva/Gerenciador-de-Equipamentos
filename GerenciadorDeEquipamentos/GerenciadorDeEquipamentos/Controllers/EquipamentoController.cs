using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class EquipamentoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarEquipamentos(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
            }
            var equipamento = bd.Equipamentos.ToList();
            return View(equipamento);
        }
        //===============================================================================================


        [Authorize(Roles = ("Administrador, Funcionário"))]
        public ActionResult GerenciarEquipamentos()
        {
            try
            {
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { status = 1 });
            }
        }

        [Authorize]
        public ActionResult CriarEquipamento(int? equipamentoId, int? status)
        {

            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
            }
            try
            {
                ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 && x.Tipo == 3).ToList(), "StatusId", "Descricao");
                ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
                ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

                return View();
            }
            catch
            {
                return RedirectToAction("ListarEquipamentos", new { status = 1 });
            }

        }

        [HttpPost]
        public ActionResult CriarEquipamento(Equipamentos equipamento)
        {
            try
            {
                equipamento.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
                bd.Equipamentos.Add(equipamento);
                bd.SaveChanges();
                Session["EquipamentoId"] = equipamento.EquipamentoId;

                return RedirectToAction("CriarEquipamentoEspecificacoes", "Equipamento", new { tipoEquipamentoId = equipamento.TipoEquipamentoId });
            }
            catch
            {
                return RedirectToAction("CriarEquipamento", new { status = 1 });
            }
        }

        [HttpGet]
        public ActionResult CriarEquipamentoEspecificacoes(int? tipoEquipamentoId, int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
            }
            try
            {
                ViewBag.atributos = bd.Atributos.Where(x => x.TipoEquipamentoId == tipoEquipamentoId);
                var atributos = bd.Atributos.Where(x => x.TipoEquipamentoId == tipoEquipamentoId);
                ViewBag.especificacoes = bd.Especificacoes.ToList();

                return View();
            }
            catch
            {
                return RedirectToAction("CriarEquipamento", new { status = 1 });
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
                return RedirectToAction("ListarEquipamentos", "Equipamento");

            }
            catch
            {
                return RedirectToAction("CriarEquipamentoEspecificacoes", new { status = 1 });
            }
        }


        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarEquipamento(int EquipamentoId, int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
            }
            try
            {
                var equipamento = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == EquipamentoId);

                ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 && x.Tipo == 3).ToList(), "StatusId", "Descricao");
                ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
                ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

                return View(equipamento);

            }
            catch
            {
                return RedirectToAction("ListarEquipamentos", new { status = 1 });
            }
        }
        //===============================================================================================
        [HttpPost]
        public ActionResult EditarEquipamento(Equipamentos equipamento)
        {
            try
            {
                var equipamentoBD = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == equipamento.EquipamentoId);

                equipamentoBD.DataAquisicao = equipamento.DataAquisicao;
                equipamentoBD.DataGarantia = equipamento.DataGarantia;
                equipamentoBD.NumeroPatrimonial = equipamento.NumeroPatrimonial;
                equipamentoBD.ServiceTagSerial = equipamento.ServiceTagSerial;
                equipamentoBD.Observacao = equipamento.Observacao;
                equipamentoBD.DepartamentoId = equipamento.DepartamentoId;
                equipamentoBD.StatusId = equipamento.StatusId;

                bd.Entry(equipamentoBD).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarEquipamentos", "Equipamento");

            }
            catch
            {
                return RedirectToAction("EditarEquipamento", new { EquipamentoId = equipamento.EquipamentoId, status = 1 });
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
                return RedirectToAction("ListarEquipamentos", new { status = 1 });
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
                return RedirectToAction("ListarEquipamentos", new { status = 1 });
            }

        }

        //===============================================================================================
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
                return RedirectToAction("ListarEquipamentos", new { status = 1 });
            }

        }

        //===============================================================================================
        [Authorize]
        [HttpGet]
        public ActionResult DetalhesEquipamento(int EquipamentoId)
        {
            try
            {
                var detalhes = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == EquipamentoId);

                return View(detalhes);

            }
            catch
            {
                return RedirectToAction("ListarEquipamentos", new { status = 1 });
            }

        }

        [Authorize(Roles = ("Administrador, Funcionário"))]
        [HttpPost]
        public ActionResult DeletarEquipamento(int EquipamentoId)
        {
            try
            {
                Equipamentos equipamento = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == EquipamentoId);

                equipamento.StatusId = 2;
                bd.Entry(equipamento).State = EntityState.Modified;
                bd.SaveChanges();

                return RedirectToAction("ListarEquipamentos");
            }
            catch
            {
                return RedirectToAction("ListarEquipamentos", new { status = 1 });
            }
        }
        public ActionResult GetPie2ChartData()
        {
            List<vw_quantidade_equipamentos_por_departamento> data = new List<vw_quantidade_equipamentos_por_departamento>();


            data = bd.vw_quantidade_equipamentos_por_departamento.ToList();

            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]
            {
                "Departamento",
                "Quantidade"
            };

            int i = 0;

            foreach (var item in data)
            {
                i++;
                chartData[i] = new object[] { item.Departamento.ToString(), item.Quantidade };
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPieChartData()
        {
            List<vw_quantidade_equipamentos_por_tipo> data = new List<vw_quantidade_equipamentos_por_tipo>();


            data = bd.vw_quantidade_equipamentos_por_tipo.ToList();


            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]
            {
                "Tipo",
                "Quantidade"
            };



            int i = 0;

            foreach (var item in data)
            {
                i++;
                chartData[i] = new object[] { item.TipoDeEquipamento.ToString(), item.Quantidade };
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);

        }

    }
}