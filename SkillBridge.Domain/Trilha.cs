using System.ComponentModel.DataAnnotations;

namespace SkillBridge.Domain
{
    public class Trilha
    {
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        public string? Descricao { get; set; }

        [Required]
        [StringLength(50)]
        public string Nivel { get; set; }

        [Range(1, int.MaxValue)]
        public int CargaHoraria { get; set; }

        [StringLength(100)]
        public string? FocoPrincipal { get; set; }

        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}