using Discord.WebSocket;
using System.Threading.Tasks;

namespace DCM
{
    public abstract class CommandHandler
    {
        public virtual void Handle(SocketMessage message) { }
        public virtual Task HandleAsync(SocketMessage message) => Task.CompletedTask;
    }
}
