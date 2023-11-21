using Microsoft.AspNetCore.Http;

namespace DriveAnheu.Application.UseCases.Itens.Shared.Input
{
    public sealed class ItemUploadInput
    {
        public required Guid GuidPastaPai { get; set; }

        public required IFormFile Arquivo { get; set; }
    }
}