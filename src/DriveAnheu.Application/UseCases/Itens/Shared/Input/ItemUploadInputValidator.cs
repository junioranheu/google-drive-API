using DriveAnheu.Domain.Consts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Input
{
    public sealed class ItemUploadInputValidator : AbstractValidator<ItemUploadInput>
    {
        public ItemUploadInputValidator()
        {
            RuleFor(x => x.GuidPastaPai).
                Must(ValidarGuid).WithMessage("GUID da pasta pai é inválido").
                When(x => x.GuidPastaPai != Guid.Empty); 

            RuleFor(x => x.Arquivo).
                NotNull().WithMessage("Nenhum arquivo foi selecionado").
                Must(x => x.Length > 0).WithMessage("O arquivo não pode ser vazio").
                Must(x => ValidarExtensaoArquivo(x)).WithMessage("Essa extensão não é permitida no momento").
                Must(x => x.Length <= SistemaConst.QtdLimiteMBsImportEmBytes).WithMessage($"O arquivo excedeu o tamanho limite de {SistemaConst.QtdLimiteMBsImportEmMB} MB");
        }

        private static bool ValidarGuid(Guid guid)
        {
            return guid == Guid.Empty || guid != Guid.Parse("00000000-0000-0000-0000-000000000000");
        }

        private static bool ValidarExtensaoArquivo(IFormFile arquivo)
        {
            List<string> listaExtensoesPermitidas = [".jpg", ".jpeg", ".png", ".webp", ".txt", ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".csv"];

            string extensao = Path.GetExtension(arquivo.FileName);
            bool isPermitido = listaExtensoesPermitidas.Contains(extensao, StringComparer.OrdinalIgnoreCase);

            return isPermitido;
        }
    }
}