using DriveAnheu.Domain.Entities;
using DriveAnheu.Infrastructure.Data;

namespace DriveAnheu.Application.UseCases.Logs.CriarLog
{
    public sealed class CriarLogCommand(DriveAnheuContext _context) : ICriarLogCommand
    {
        public async Task Execute(Log input)
        {
            _context.ChangeTracker.Clear();

            await _context.AddAsync(input);
            await _context.SaveChangesAsync();
        }
    }
}