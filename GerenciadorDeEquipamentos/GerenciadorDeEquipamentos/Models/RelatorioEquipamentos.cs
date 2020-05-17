using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciadorDeEquipamentos.Models
{
    public class RelatorioEquipamentos
    {
        public int TipoEquipamentoId { get; set; }
        public int DepartamentoId { get; set; }
        public int StatusId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DataGarantia { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime DataAquisicao { get; set; }

        public string Agrupar { get; set; }
    }
}