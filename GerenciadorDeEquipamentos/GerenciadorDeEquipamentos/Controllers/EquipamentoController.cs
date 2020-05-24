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
        public ActionResult ListarEquipamentos(int? status, int? tipo)
        {
            try
            {
                if (status == 1)
                {
                    ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
                }

                Session["tipo"] = tipo;
                if (tipo == 1)
                {
                    //Em manutenção
                    var equipamento = bd.Equipamentos.Where(x => x.StatusId == 9).ToList();
                    return View(equipamento);
                }
                else if (tipo == 2)
                {
                    //Ativos
                    var equipamento = bd.Equipamentos.Where(x => x.StatusId == 1).ToList();
                    return View(equipamento);
                }
                else if (tipo == 3)
                {
                    //Desativos
                    var equipamento = bd.Equipamentos.Where(x => x.StatusId == 2).ToList();
                    return View(equipamento);
                }
                else
                {
                    //Todos
                    var equipamento = bd.Equipamentos.ToList();
                    return View(equipamento);
                }
            }
            catch
            {
                return RedirectToAction("Login", "Login", new { status = 2 });
            }

        }
        //===============================================================================================

        [Authorize(Roles = ("Administrador, Funcionário"))]
        public ActionResult GerenciarEquipamentos()
        {
            try
            {
                ViewBag.total = bd.Equipamentos.Count();
                ViewBag.emManutencao = bd.Equipamentos.Where(x => x.StatusId == 9).Count();
                ViewBag.ativos = bd.Equipamentos.Where(x => x.StatusId == 1).Count();
                ViewBag.desativos = bd.Equipamentos.Where(x => x.StatusId == 2).Count();

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

            try
            {
                if (status == 1)
                {
                    ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
                }
                else if (status == 2)
                {
                    ViewBag.message = "Equipamento já cadastrado no sistema!";
                }
                ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 || x.Tipo == 3).ToList(), "StatusId", "Descricao");
                ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
                ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

                ViewBag.tipo = Session["tipo"];
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
                if (bd.Equipamentos.Any(x => x.ServiceTagSerial == equipamento.ServiceTagSerial
                || x.NumeroPatrimonial == equipamento.NumeroPatrimonial))
                {
                    return RedirectToAction("CriarEquipamento", new { status = 2 });
                }
                else
                {
                    equipamento.PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
                    bd.Equipamentos.Add(equipamento);
                    bd.SaveChanges();

                    return RedirectToAction("CriarEquipamentoEspecificacoes", "EquipamentoEspecificacao", new { tipoEquipamentoId = equipamento.TipoEquipamentoId, EquipamentoId = equipamento.EquipamentoId });
                }
            }
            catch
            {
                return RedirectToAction("CriarEquipamento", new { status = 1 });
            }
        }



        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarEquipamento(int EquipamentoId, int? status)
        {
            try
            {
                if (status == 1)
                {
                    ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor tente novamente.";
                }
                var equipamento = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == EquipamentoId);

                ViewBag.status = new SelectList(bd.Status.Where(x => x.Tipo == 1 || x.Tipo == 3).ToList(), "StatusId", "Descricao");
                ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
                ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

                ViewBag.tipo = Session["tipo"];

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

                int tipo = Convert.ToInt32(Session["tipo"]);
                return RedirectToAction("ListarEquipamentos", "Equipamento", new { tipo = tipo});

            }
            catch
            {
                return RedirectToAction("EditarEquipamento", new { EquipamentoId = equipamento.EquipamentoId, status = 1 });
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

                ViewBag.tipo = Session["tipo"];

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

                int tipo = Convert.ToInt32(Session["tipo"]);
                return RedirectToAction("ListarEquipamentos", new { tipo = tipo});
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