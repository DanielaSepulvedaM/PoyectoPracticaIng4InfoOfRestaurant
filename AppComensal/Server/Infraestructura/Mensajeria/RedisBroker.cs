using Entidades.Infraestructura.Mensajeria;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppComensal.Server.Infraestructura.Mensajeria
{
    public class RedisBroker : ICanal
    {
        private readonly ISubscriber sub = null;

        public RedisBroker()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            sub = redis.GetSubscriber();
        }
        public Task EnviarMensaje(Mensaje mensaje)
        {
            var msg = JsonSerializer.Serialize(mensaje);
            return sub.PublishAsync(Canales.ADMINISTRACION, msg, CommandFlags.FireAndForget);
        }
    }
}
