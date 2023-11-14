using DriveAnheu.Domain.Entities;

namespace DriveAnheu.Application.UseCases.Logs.CriarLog
{
    public interface ICriarLogCommand
    {
        Task Execute(Log input);
    }
}