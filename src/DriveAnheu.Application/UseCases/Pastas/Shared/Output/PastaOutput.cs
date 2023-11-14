namespace DriveAnheu.Application.UseCases.Pastas.Shared.Output
{
    public sealed class PastaOutput
    {
        public Guid Guid { get; set; } = Guid.Empty;

        public string Nome { get; set; } = string.Empty;

        public int UsuarioId { get; set; }
    }
}