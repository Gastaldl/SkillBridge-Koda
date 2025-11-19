using SkillBridge.Domain;
using SkillBridge.Domain.Interfaces;

namespace SkillBridge.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Usuario?> BuscarPorId(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {
            // Regra de Negócio: Verificar email único
            var existente = await _repository.GetByEmailAsync(usuario.Email);
            if (existente != null)
            {
                throw new ArgumentException("Já existe um usuário cadastrado com este e-mail.");
            }

            // Define data de cadastro se não vier preenchida
            if (usuario.DataCadastro == default)
            {
                usuario.DataCadastro = DateTime.Now;
            }

            return await _repository.AddAsync(usuario);
        }

        public async Task AtualizarUsuario(long id, Usuario usuario)
        {
            if (id != usuario.Id)
                throw new ArgumentException("ID da rota não confere com o ID do usuário.");

            // Verifica se o usuário existe antes de atualizar
            var usuarioExistente = await _repository.GetByIdAsync(id);
            if (usuarioExistente == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            // Atualiza campos permitidos (preservando data de cadastro, por exemplo)
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.AreaAtuacao = usuario.AreaAtuacao;
            usuarioExistente.NivelCarreira = usuario.NivelCarreira;

            await _repository.UpdateAsync(usuarioExistente);
        }

        public async Task ExcluirUsuario(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}