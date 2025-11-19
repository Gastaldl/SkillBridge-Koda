namespace SkillBridge.Domain.Interfaces
{
    public interface ITrilhaService
    {
        Task<IEnumerable<Trilha>> ListarTrilhas();
        Task<Trilha?> BuscarPorId(long id);
        Task<Trilha> CriarTrilha(Trilha trilha);
        Task AtualizarTrilha(long id, Trilha trilha);
        Task ExcluirTrilha(long id);
    }
}