using DriveAnheu.Application.Hubs.MiscHub.Models.Output;
using DriveAnheu.Application.Hubs.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static junioranheu_utils_package.Fixtures.Get;

namespace DriveAnheu.Application.Hubs.MiscHub
{
    [Authorize]
    public sealed class MiscHub : Hub
    {
        const string grupo = "_online";
        private static readonly List<UsuarioOnlineResponse> listaUsuarioOnline = [];

        public override async Task OnConnectedAsync()
        {
            if (!Context.User!.Identity!.IsAuthenticated)
            {
                throw new Exception($"Usuário não autenticado");
            }

            string usuarioId = Misc.ConverterObjetoParaString(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            string usuarioGuid = Misc.ConverterObjetoParaString(Context.User.FindFirst(ClaimTypes.Thumbprint)?.Value);
            string signalR_ConnectionId = Misc.ConverterObjetoParaString(Context.ConnectionId);

            // Adicionar o usuário (signalR_ConnectionId) no grupo (IGroupManager, nativo do SignalR);
            await Groups.AddToGroupAsync(signalR_ConnectionId, grupo);

            // Adicionar/atualizar usuário na lista de controle manual;
            UsuarioOnlineResponse? checkUsuarioOnline = listaUsuarioOnline.Where(x => x.UsuarioId == usuarioId).FirstOrDefault();

            if (checkUsuarioOnline is null)
            {
                UsuarioOnlineResponse u = new()
                {
                    UsuarioId = usuarioId,
                    UsuarioGuid = usuarioGuid,
                    ConnectionId = signalR_ConnectionId
                };

                listaUsuarioOnline.Add(u);
            }
            else
            {
                await Groups.RemoveFromGroupAsync(checkUsuarioOnline.ConnectionId, grupo); // Remover ConnectionId antigo;
                checkUsuarioOnline.ConnectionId = signalR_ConnectionId;
            }

            await ObterListaUsuariosOnline();

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string signalR_ConnectionId = Misc.ConverterObjetoParaString(Context.ConnectionId);
            UsuarioOnlineResponse? checkUsuario = listaUsuarioOnline.FirstOrDefault(x => x.ConnectionId == signalR_ConnectionId);

            if (checkUsuario is not null)
            {
                await Groups.RemoveFromGroupAsync(signalR_ConnectionId, grupo);
                listaUsuarioOnline.Remove(checkUsuario!);
            }

            await ObterListaUsuariosOnline();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ObterListaUsuariosOnline()
        {
            await Clients.Group(grupo).SendAsync(ObterNomeDoMetodo(), listaUsuarioOnline);
        }
    }
}