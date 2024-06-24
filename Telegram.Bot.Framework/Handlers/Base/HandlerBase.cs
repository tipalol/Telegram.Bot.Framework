using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Handlers.Base;

public abstract class HandlerBase<T> where T : Message
{
    /// <summary>
    /// Check if it can handle specific message
    /// </summary>
    public abstract bool CanHandle(T message);
    
    /// <summary>
    /// Handle the message
    /// </summary>
    public abstract Task HandleAsync(T message, ITelegramBotClient botClient, CancellationToken cancellationToken);
}