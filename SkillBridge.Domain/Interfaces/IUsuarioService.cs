namespace SkillBridge.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ListarUsuarios();
        Task<Usuario?> BuscarPorId(long id);
        Task<Usuario> CadastrarUsuario(Usuario usuario);
        Task AtualizarUsuario(long id, Usuario usuario);
        Task ExcluirUsuario(long id);
    }
}