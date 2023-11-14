using AutoMapper;
using DriveAnheu.Application.UseCases.Pastas.Shared.Input;
using DriveAnheu.Application.UseCases.Pastas.Shared.Output;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using DriveAnheu.Domain.Entities;

namespace DriveAnheu.Application.AutoMapper
{
    public sealed class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Usuários;
            CreateMap<Usuario, UsuarioOutput>();

            // Principal;
            CreateMap<PastaInput, Pasta>();
            CreateMap<Pasta, PastaOutput>();

            // CreateMap<ItemInput, Item>();
            // CreateMap<Item, ItemOutput>();
        }
    }
}