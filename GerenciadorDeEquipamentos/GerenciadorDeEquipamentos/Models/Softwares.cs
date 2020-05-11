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
    
    public partial class Softwares
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Softwares()
        {
            this.Licencas = new HashSet<Licencas>();
            this.SoftwareEquipemento = new HashSet<SoftwareEquipemento>();
        }
    
        public int SoftwareId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public System.DateTime DataAquisicao { get; set; }
        public int StatusId { get; set; }
        public int PessoaId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Licencas> Licencas { get; set; }
        public virtual Pessoas Pessoas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoftwareEquipemento> SoftwareEquipemento { get; set; }
        public virtual Status Status { get; set; }
    }
}
