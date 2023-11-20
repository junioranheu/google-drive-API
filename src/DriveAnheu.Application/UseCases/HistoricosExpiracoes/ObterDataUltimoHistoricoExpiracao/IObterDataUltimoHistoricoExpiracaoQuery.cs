
namespace DriveAnheu.Application.UseCases.HistoricosExpiracoes.ObterDataUltimoHistoricoExpiracao
{
    public interface IObterDataUltimoHistoricoExpiracaoQuery
    {
        Task<DateTime?> Execute();
    }
}