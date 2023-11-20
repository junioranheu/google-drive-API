namespace DriveAnheu.Application.UseCases.Itens.Shared.Output
{
    public sealed class FolderRotaOutput
    {
        public Guid Guid { get; set; } = Guid.Empty;

        public string Nome { get; set; } = string.Empty;
    }
}