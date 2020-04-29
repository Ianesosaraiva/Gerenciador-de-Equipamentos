using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class AtributosController : Controller
    {
        // GET: Equipamento
        shield01Entities bd = new shield01Entities();
        [Authorize]
        public ActionResult ListarAtributos(int? AtributoId)
        {
            if (AtributoId != null)
            {
                var atributo = bd.Atributos.Where(x => x.AtributoId == AtributoId);
                return View(atributo);
            }
            else
            {
                var atributo = bd.Atributos.ToList();
                return View(atributo);
            }
        }
        //===============================================================================================
        [Authorize]
        public ActionResult CriarAtributos()
        {
            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarAtributos(Atributos atributo, int AtributoId)
        {
            atributo.AtributoId = Convert.ToInt32(HttpContext.User.Identity.Name);

            bd.Atributos.Add(atributo);
            bd.SaveChanges();
            return RedirectToAction("ListarAtributo", "Atributo");
        }

        //===============================================================================================

        [Authorize]
        [HttpGet]
        public ActionResult EditarAtributos(int AtributoId)
        {
            var atributo = bd.Atributos.FirstOrDefault(x => x.AtributoId == AtributoId);

            ViewBag.status = new SelectList(bd.Status.ToList(), "StatusId", "Descricao");
            ViewBag.tipoEquipamento = new SelectList(bd.TipoEquipamento.ToList(), "TipoEquipamentoId", "Nome");
            ViewBag.departamento = new SelectList(bd.Departamentos.ToList(), "DepartamentoId", "Nome");

            return View(atributo);
        }

        [HttpPost]
        public ActionResult EditarAtributos(Atributos atributo)
        {
            var atributoBD = bd.Atributos.FirstOrDefault(x => x.AtributoId == atributo.AtributoId);

            //atributoBD.DataAquisicao = departamento.DataAquisicao;
            //atributoBD.DataGarantia = departamento.DataGarantia;
            //atributoBD.NumeroPatrimonial = departamento.NumeroPatrimonial;
            //atributoBD.ServiceTagSerial = departamento.ServiceTagSerial;
            //atributoBD.Observacao = departamento.Observacao;
            //atributoBD.DepartamentoId = departamento.DepartamentoId;
            //atributoBD.StatusId = departamento.StatusId;

            bd.Entry(atributoBD).State = EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("ListarAtributo", "Atributo");
        }

        //===============================================================================================

    }
}