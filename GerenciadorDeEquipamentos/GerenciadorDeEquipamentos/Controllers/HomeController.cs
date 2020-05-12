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
                NomeUsuario = bd.Pessoas.FirstOrDefault(x => x.PessoaId == PessoaId).NomeCompleto.Split(' ')[0],
                qtdUsuariosAtivos = bd.Pessoas.Where(x => x.StatusId == 1).Count(),
                qtdEquipamentosAtivos = bd.Equipamentos.Where(x => x.StatusId == 1).Count(),
                qtdSoftwaresAtivos = bd.Softwares.Where(x => x.StatusId == 1).Count(),
                qtdOSAbertos = bd.OrdemServico.Where(x => x.DataEncerramento == null).Count()
            };

            return View(dados);
        }

        public ActionResult GetLineChartData()
        {
            List<vw_os_encerrados_por_mes> data = new List<vw_os_encerrados_por_mes>();


            data = bd.vw_os_encerrados_por_mes.ToList();

            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]
            {
                "Mês",
                "Quantidade"
            };

            int i = 0;

            foreach (var item in data)
            {
                i++;
                chartData[i] = new object[] { item.Mes.ToString(), item.Quantidade };
            }

            return Json(chartData, JsonRequestBehavior.AllowGet);

        }

    }
}
