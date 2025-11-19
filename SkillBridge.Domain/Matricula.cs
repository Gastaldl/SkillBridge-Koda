using System.ComponentModel.DataAnnotations;

namespace SkillBridge.Domain
{
    public class Matricula
    {
        public long Id { get; set; } // [cite: 204]

        [Required]
        public long UsuarioId { get; set; } // [cite: 205]

        [Required]
        public long TrilhaId { get; set; } // [cite: 206]

        public DateTime DataInscricao { get; set; } // [cite: 207]

        [Required]
        [StringLength(50)]
        public string Status { get; set; } // Ex: ATIVA, CONCLUIDA, CANCELADA [cite: 208, 209, 212]

        // Propriedades de Navegação
        public virtual Usuario Usuario { get; set; }
        public virtual Trilha Trilha { get; set; }
    }
}