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
    
    public partial class Especificacoes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especificacoes()
        {
            this.EspecificacaoEquipamento = new HashSet<EspecificacaoEquipamento>();
        }
    
        public int EspecificacaoId { get; set; }
        public string Nome { get; set; }
        public int AtributoId { get; set; }
        public int PessoaId { get; set; }
    
        public virtual Atributos Atributos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EspecificacaoEquipamento> EspecificacaoEquipamento { get; set; }
        public virtual Pessoas Pessoas { get; set; }
    }
}
