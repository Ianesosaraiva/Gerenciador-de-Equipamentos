using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciadorDeEquipamentos.Models
{
    public class DataPessoa
    {
        [Required(ErrorMessage = "Campo obrigátorio.")]
        [StringLength(8)]
        public string Nome_Completo { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Telefone { get; set; }
    }
}