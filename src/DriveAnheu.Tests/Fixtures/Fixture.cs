using AutoMapper;
using DriveAnheu.Application.AutoMapper;
using DriveAnheu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DriveAnheu.Tests.Fixtures
{
    public static class Fixture
    {
        public static DriveAnheuContext CriarContext()
        {
            DbContextOptions<DriveAnheuContext> mock = new DbContextOptionsBuilder<DriveAnheuContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            DriveAnheuContext? context = new(mock);

            return context;
        }

        public static IMapper CriarMapper()
        {
            MapperConfiguration mockMapper = new(x =>
            {
                x.AddProfile(new AutoMapperConfig());
            });

            IMapper map = mockMapper.CreateMapper();

            return map;
        }
    }
}