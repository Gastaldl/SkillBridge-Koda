namespace SkillBridge.Domain.Interfaces
{
    public interface ITrilhaRepository
    {
        Task<IEnumerable<Trilha>> GetAllAsync();
        Task<Trilha?> GetByIdAsync(long id);
        Task<Trilha> AddAsync(Trilha trilha);
        Task UpdateAsync(Trilha trilha);
        Task DeleteAsync(long id);
    }
}