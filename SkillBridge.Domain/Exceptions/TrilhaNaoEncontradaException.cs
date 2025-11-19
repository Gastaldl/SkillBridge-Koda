namespace SkillBridge.Domain.Exceptions
{
    public class TrilhaNaoEncontradaException : Exception
    {
        public TrilhaNaoEncontradaException(long id)
            : base($"A trilha com o ID {id} não foi encontrada no sistema.")
        {
        }
    }
}