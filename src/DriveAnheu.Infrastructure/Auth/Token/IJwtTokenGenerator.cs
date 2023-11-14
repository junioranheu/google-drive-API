
namespace DriveAnheu.Infrastructure.Auth.Token
{
    public interface IJwtTokenGenerator
    {
        string GerarToken(int usuarioId, Guid id);
    }
}