using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDeEquipamentos.Models
{
    public class Desempenho
    {
        public string Nome { get; set; }
        public string Responsavel { get; set; }
        public int TarefasAbertas { get; set; }
        public int TarefasFinalizadas { get; set; }
        public int OSAbertos { get; set; }
        public int OSFinalizados { get; set; }
        public int Tempo { get; set; }
    }
}