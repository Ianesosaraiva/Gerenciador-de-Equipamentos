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
        public ActionResult ListarDepartamentos(int? DepartamentoId)
        {
            if (DepartamentoId != null)
            {
                var departamento = bd.Departamentos.Where(x => x.DepartamentoId == DepartamentoId);
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
        public ActionResult CriarDepartamentos()
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarDepartamentos(Departamentos departamento, int DepartamentoId)
        {
            departamento.DepartamentoId = Convert.ToInt32(HttpContext.User.Identity.Name);

            bd.Departamentos.Add(departamento);
            bd.SaveChanges();
            return RedirectToAction("ListarDepartamento", "Departamento");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarDepartamentos(int DepartamentoId)
        {
            var departamento = bd.Departamentos.FirstOrDefault(x => x.DepartamentoId == DepartamentoId);

            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View(departamento);
        }

        [HttpPost]
        public ActionResult EditarDepartamentos(Departamentos departamento)
        {
            var departamentoBD = bd.Equipamentos.FirstOrDefault(x => x.EquipamentoId == departamento.DepartamentoId);

            //departamentoBD.DataAquisicao = departamento.DataAquisicao;
            //departamentoBD.DataGarantia = departamento.DataGarantia;
            //departamentoBD.NumeroPatrimonial = departamento.NumeroPatrimonial;
            //departamentoBD.ServiceTagSerial = departamento.ServiceTagSerial;
            //departamentoBD.Observacao = departamento.Observacao;
            //departamentoBD.DepartamentoId = departamento.DepartamentoId;
            //departamentoBD.StatusId = departamento.StatusId;

            bd.Entry(departamentoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarDepartamentos", "Departamento");
        }

        //===============================================================================================

    }
}
