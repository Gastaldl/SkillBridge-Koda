using SkillBridge.Domain;

namespace SkillBridge.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(long id);
        Task<Usuario?> GetByEmailAsync(string email); // Novo método útil
        Task<Usuario> AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(long id);
    }
}