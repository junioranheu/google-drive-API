namespace DriveAnheu.Application.UseCases.Pastas.Shared.Input
{
    public sealed class PastaInput
    {
        public Guid Guid { get; set; } = Guid.Empty;

        public string Nome { get; set; } = string.Empty;

        public int UsuarioId { get; set; }
    }
}
