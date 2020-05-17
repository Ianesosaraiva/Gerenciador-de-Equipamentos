using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciadorDeEquipamentos.Models
{
    public class DataColaborador
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataColaborador()
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
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string NomeCompleto { get; set; }

        [Required]
        [MinLength(8)]
        public string Senha { get; set; }

        [Required]
        [Compare("Senha", ErrorMessage = "Senhas não conferem!")]
        public string ConfirmarSenha { get; set; }


        [Required]
        [StringLength(14, ErrorMessage = "Tamanho maximo de 11 caracteres.")]
        public string CPF { get; set; }


        public string RG { get; set; }


        public string Contato { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public System.DateTime DataCadastro { get; set; }


        [Required]
        public string Email { get; set; }


        public Nullable<System.DateTime> UltimoAcesso { get; set; }


        [Required]
        public int AcessoId { get; set; }


        [Required]
        public int StatusId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public System.DateTime DataNascimento { get; set; }

        [Required]
        public int EquipeId { get; set; }

        public virtual Acessos Acessos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipamentos> Equipamentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipe> Equipe { get; set; }
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

        public static implicit operator DataColaborador(Pessoas v)
        {
            throw new NotImplementedException();
        }
    }
}