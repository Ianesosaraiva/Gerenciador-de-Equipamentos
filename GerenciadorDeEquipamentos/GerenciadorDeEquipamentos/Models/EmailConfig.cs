//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GerenciadorDeEquipamentos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailConfig
    {
        public int ConfigId { get; set; }
        public string NomeServidor { get; set; }
        public int NumeroPorta { get; set; }
        public string EnderecoEmail { get; set; }
        public string EmailUsuario { get; set; }
        public sbyte FlgSSL { get; set; }
        public string EmailSenha { get; set; }
    }
}
