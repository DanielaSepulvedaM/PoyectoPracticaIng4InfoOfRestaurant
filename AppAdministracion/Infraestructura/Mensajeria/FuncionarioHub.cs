using Entidades.Infraestructura.Mensajeria;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAdministracion.Infraestructura.Mensajeria
{
    public class FuncionarioHub : Hub
    {
        public override async Task OnConnectedAsync() {
            if (Context.User.HasClaim(c => c.Type == "RestauranteID"))
            {
                var restauranteId = Context.User.FindFirst("RestauranteID").Value;
                await Groups.AddToGroupAsync(Context.ConnectionId, restauranteId);
                await base.OnConnectedAsync();
            }   
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.HasClaim(c => c.Type == "RestauranteID"))
            {
                var restauranteId = Context.User.FindFirst("RestauranteID").Value;
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, restauranteId);
                await base.OnConnectedAsync();
            }
        }
    }
}
