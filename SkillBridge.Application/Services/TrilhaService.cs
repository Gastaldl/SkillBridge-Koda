using SkillBridge.Domain;
using SkillBridge.Domain.Interfaces;
using SkillBridge.Domain.Exceptions;

namespace SkillBridge.Application.Services
{
    public class TrilhaService : ITrilhaService
    {
        private readonly ITrilhaRepository _repository;

        public TrilhaService(ITrilhaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Trilha>> ListarTrilhas()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Trilha?> BuscarPorId(long id)
        {
            var trilha = await _repository.GetByIdAsync(id);

            if (trilha == null)
            {
                throw new TrilhaNaoEncontradaException(id);
            }

            return trilha;
        }

        public async Task<Trilha> CriarTrilha(Trilha trilha)
        {
            if (trilha.CargaHoraria <= 0)
                throw new ArgumentException("A carga horária deve ser maior que zero.");

            var niveisValidos = new[] { "INICIANTE", "INTERMEDIARIO", "AVANCADO" };

            if (string.IsNullOrEmpty(trilha.Nivel) || !niveisValidos.Contains(trilha.Nivel.ToUpper()))
                throw new ArgumentException("Nível inválido. Use: INICIANTE, INTERMEDIARIO ou AVANCADO.");

            return await _repository.AddAsync(trilha);
        }

        public async Task AtualizarTrilha(long id, Trilha trilha)
        {
            if (id != trilha.Id)
                throw new ArgumentException("ID da URL diferente do ID do corpo da requisição.");

            var trilhaExistente = await _repository.GetByIdAsync(id);

            if (trilhaExistente == null)
            {
                throw new TrilhaNaoEncontradaException(id);
            }

            await _repository.UpdateAsync(trilha);
        }

        public async Task ExcluirTrilha(long id)
        {
            var trilha = await _repository.GetByIdAsync(id);

            if (trilha == null)
            {
                throw new TrilhaNaoEncontradaException(id);
            }

            await _repository.DeleteAsync(id);
        }
    }
}