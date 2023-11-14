using AutoMapper;
using DriveAnheu.Domain.Entities;

namespace DriveAnheu.Application.AutoMapper
{
    public sealed class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Outros;
            CreateMap<Log, LogOutput>();

            // Usuários;
            CreateMap<UsuarioInput, Usuario>();
            CreateMap<Usuario, UsuarioOutput>();

            // Principal;
            CreateMap<PastaInput, Pasta>();
            CreateMap<Pasta, PastaOutput>();

            CreateMap<ItemInput, Item>();
            CreateMap<Item, ItemOutput>();
        }
    }
}