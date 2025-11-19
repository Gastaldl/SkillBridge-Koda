using System.ComponentModel.DataAnnotations;

namespace SkillBridge.Domain
{
    public class Usuario
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(100)]
        public string? AreaAtuacao { get; set; }

        [StringLength(50)]
        public string? NivelCarreira { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}