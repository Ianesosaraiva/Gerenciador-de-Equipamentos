using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorDeEquipamentos.Models
{
    public class DadosIndex
    {
        public string NomeUsuario { get; set; }
        public int qtdUsuariosAtivos { get; set; }
        public int qtdEquipamentosAtivos { get; set; }
        public int qtdSoftwaresAtivos { get; set; }
        public int qtdOSAbertos { get; set; }
    }
}