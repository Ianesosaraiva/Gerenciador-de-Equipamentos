using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDeEquipamentos.Models
{
    using System;
    using System.Collections.Generic;

    public class Desempenho
    {
        public string Colaborador { get; set; }
        public string Equipe { get; set; }
        public string Responsavel { get; set; }
        public int TarefasAbertas { get; set; }
        public int TarefasEncerradas { get; set; }
        public int OSAbertos { get; set; }
        public int OSEncerrados { get; set; }
        public int OSColaboradorAbertos { get; set; }
        public int OSColaboradorEncerrados { get; set; }
        public int Tempo { get; set; }
    }
}