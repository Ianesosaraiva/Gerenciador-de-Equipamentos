using GerenciadorDeEquipamentos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GerenciadorDeEquipamentos.Controllers
{
    public class HomeController : Controller
    {
        shield01Entities bd = new shield01Entities();

        [Authorize]
        // GET: Home
        public ActionResult Index()
        {
            int PessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);
            DadosIndex dados = new DadosIndex
            {
                NomeUsuario = bd.Pessoas.FirstOrDefault(x => x.PessoaId == PessoaId).Nome_Completo.Split(' ')[0],
                qtdUsuariosAtivos = bd.Pessoas.Where(x => x.StatusId == 1).Count(),
                qtdEquipamentosAtivos = bd.Equipamentos.Where(x => x.StatusId == 1).Count(),
                qtdSoftwaresAtivos = bd.Softwares.Where(x => x.StatusId == 1).Count(),
                qtdOSAbertos = bd.OrdemServico.Where(x => x.DataEncerramento == null).Count()
            };

            return View(dados);
        }

    }
}
