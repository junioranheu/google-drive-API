using DriveAnheu.Application.UseCases.Usuarios.ObterUsuario;
using DriveAnheu.Application.UseCases.Usuarios.Shared.Output;
using Microsoft.Extensions.Caching.Memory;

namespace DriveAnheu.Application.UseCases.Usuarios.ObterUsuarioCache
{
    public sealed class ObterUsuarioCacheQuery : IObterUsuarioCacheQuery
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IObterUsuarioQuery _obterUsuarioQuery;

        public ObterUsuarioCacheQuery(
            IMemoryCache memoryCache,
            IObterUsuarioQuery obterUsuarioQuery)
        {
            _memoryCache = memoryCache;
            _obterUsuarioQuery = obterUsuarioQuery;
        }

        public async Task<UsuarioOutput?> Execute(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return null;
            }

            string keyCache = $"keyObterUsuarioCache_{guid}";
            if (!_memoryCache.TryGetValue(keyCache, out UsuarioOutput? usuario))
            {
                usuario = await _obterUsuarioQuery.Execute(id: null, guid: guid);
                _memoryCache.Set(keyCache, usuario, TimeSpan.FromMinutes(1));
            }

            return usuario;
        }
    }
}