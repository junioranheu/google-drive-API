namespace DriveAnheu.Application.UseCases.Itens.ObterTamanhoEmDiscoEmBytes
{
    public interface IObterTamanhoEmDiscoEmBytesQuery
    {
        Task<double> Execute(int usuarioId);
    }
}