using NerdStore.Core.Messages;

namespace NerdStore.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T evento) where T : Event;

    Task<bool> SendCommand<T>(T command) where T : Command;

}