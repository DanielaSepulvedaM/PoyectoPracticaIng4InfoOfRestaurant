using Entidades.Infraestructura.Mensajeria;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AppAdministracion.Infraestructura.Mensajeria
{
    public class RedisBroker : BackgroundService
    {
        private readonly ISubscriber sub = null;
        private readonly IHubContext<FuncionarioHub> hub;
        private readonly ILogger<RedisBroker> logger;

        public RedisBroker(IHubContext<FuncionarioHub> hub, ILogger<RedisBroker> logger)
        {
            this.hub = hub;
            this.logger = logger;

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            sub = redis.GetSubscriber();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogTrace("Iniciando broker");
            sub.Subscribe(Canales.ADMINISTRACION).OnMessage(async chnanel => {
                var jsonMsg = (string)chnanel.Message;
                var mensaje = JsonSerializer.Deserialize<Mensaje>(jsonMsg);
                await hub.Clients.Group(mensaje.RestauranteId.ToString()).SendAsync(mensaje.Comando, mensaje.IdentificadorMesa);
            });

            return Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
