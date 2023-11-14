namespace DriveAnheu.Application.UseCases.Usuarios.Shared.Output
{
    public sealed class UsuarioOutput
    {
        public int UsuarioId { get; set; }

        public Guid Guid { get; set; } = Guid.Empty;

        public DateTime Data { get; set; }
    }
}