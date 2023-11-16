using AutoMapper;
using DriveAnheu.Application.UseCases.Itens.Shared.Input;
using DriveAnheu.Application.UseCases.Itens.Shared.Output;
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
            CreateMap<ItemInput, Item>();
            CreateMap<Item, ItemOutput>();
        }
    }
}