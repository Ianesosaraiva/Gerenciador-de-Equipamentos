using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDeEquipamentos.Models
{
    public class Graficos
    {
        public int Aguardando_Atendente { get; set; }
        public int Aguardando_Atendimento { get; set; }
        public int Em_Atendimento { get; set; }
        public int Encerrado { get; set; }
        public int Cancelado { get; set; }
        public int Pausado { get; set; }
    }
}