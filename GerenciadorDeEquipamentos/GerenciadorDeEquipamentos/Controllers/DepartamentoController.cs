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
        public ActionResult ListarDepartamentos()
        {
            var departamento = bd.Departamentos.ToList();
            return View(departamento);
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarDepartamentos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarDepartamentos(Departamentos departamento)
        {
            bd.Departamentos.Add(departamento);
            bd.SaveChanges();
            return RedirectToAction("ListarDepartamento");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarDepartamentos(int DepartamentoId)
        {
            var departamento = bd.Departamentos.FirstOrDefault(x => x.DepartamentoId == DepartamentoId);

            return View(departamento);
        }

        [HttpPost]
        public ActionResult EditarDepartamentos(Departamentos departamento)
        {
            var departamentoBD = bd.Departamentos.FirstOrDefault(x => x.DepartamentoId == departamento.DepartamentoId);

            departamentoBD.Nome = departamento.Nome;
            departamentoBD.Descricao = departamento.Descricao;

            bd.Entry(departamentoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarDepartamentos");
        }

        //===============================================================================================

    }
}
