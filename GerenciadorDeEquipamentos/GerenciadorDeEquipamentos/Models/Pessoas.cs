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
    
    public partial class Pessoas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pessoas()
        {
            this.Equipamentos = new HashSet<Equipamentos>();
            this.Equipe = new HashSet<Equipe>();
            this.Especificacoes = new HashSet<Especificacoes>();
            this.SoftwareEquipemento = new HashSet<SoftwareEquipemento>();
            this.Softwares = new HashSet<Softwares>();
            this.Tarefa = new HashSet<Tarefa>();
            this.TipoSolicitacao = new HashSet<TipoSolicitacao>();
            this.Transferencia = new HashSet<Transferencia>();
        }
    
        public int PessoaId { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public System.DateTime DataNascimento { get; set; }
        public string Contato { get; set; }
        public string Senha { get; set; }
        public System.DateTime DataCadastro { get; set; }
        public Nullable<System.DateTime> UltimoAcesso { get; set; }
        public int AcessoId { get; set; }
        public int StatusId { get; set; }
        public int EquipeId { get; set; }
    
        public virtual Acessos Acessos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipamentos> Equipamentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipe> Equipe { get; set; }
        public virtual Equipe Equipe1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Especificacoes> Especificacoes { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoftwareEquipemento> SoftwareEquipemento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Softwares> Softwares { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tarefa> Tarefa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TipoSolicitacao> TipoSolicitacao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transferencia> Transferencia { get; set; }
    }
}
