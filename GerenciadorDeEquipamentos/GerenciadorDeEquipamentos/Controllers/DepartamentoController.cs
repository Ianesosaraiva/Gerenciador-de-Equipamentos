using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarEquipamentos(int? Departamento)
        {
            if (Departamento != null)
            {
                var departamento = bd.Departamentos.Where(x => x.DepartamentoId == Departamento);
                return View(departamento);
            }
            else
            {
                var departamento = bd.Departamentos.ToList();
                return View(departamento);
            }
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarDepartamento()
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarDepartamento(Departamentos departamento, int DepartamentoId)
        {
            departamento.DepartamentoId = Convert.ToInt32(HttpContext.User.Identity.Name);

            bd.Departamentos.Add(departamento);
            bd.SaveChanges();
            return RedirectToAction("ListarDepartamento", "Departamento");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarDepartamento(int DepartamentoId)
        {
            var departamento = bd.Departamentos.FirstOrDefault(x => x.DepartamentoId == DepartamentoId);

            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View(departamento);
        }

        [HttpPost]
        public ActionResult EditarDepartamento(Departamentos departamento)
        {
            var departamentoBD = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == departamento.DepartamentoId);

            //equipamentoBD.DataAquisicao = departamento.DataAquisicao;
            //equipamentoBD.DataGarantia = departamento.DataGarantia;
            //equipamentoBD.NumeroPatrimonial = departamento.NumeroPatrimonial;
            //equipamentoBD.ServiceTagSerial = departamento.ServiceTagSerial;
            //equipamentoBD.Observacao = departamento.Observacao;
            //equipamentoBD.DepartamentoId = departamento.DepartamentoId;
            //equipamentoBD.StatusId = departamento.StatusId;

            bd.Entry(departamentoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarDepartamentos", "Departamento");
        }

        //===============================================================================================

    }
}
