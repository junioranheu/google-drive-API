using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Application.Hubs.MiscHub.Models.Output
{
    public sealed class UsuarioOnlineResponse
    {
        public string UsuarioId { get; set; } = string.Empty;

        public string UsuarioGuid { get; set; } = string.Empty;

        public string ConnectionId { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = GerarHorarioBrasilia();
    }
}