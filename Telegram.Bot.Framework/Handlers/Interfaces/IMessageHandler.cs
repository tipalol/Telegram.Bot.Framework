using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Handlers.Interfaces;

/// <summary>
/// Telegram message handler
/// </summary>
public interface IMessageHandler<in T> where T : Message
{
    /// <summary>
    /// Check if it can handle specific message
    /// </summary>
    bool CanHandle(T message);
    
    /// <summary>
    /// Обработка сообщения
    /// </summary>
    Task HandleAsync(T message, ITelegramBotClient botClient, CancellationToken cancellationToken);

    HandlerType Type { get; }
    
    bool Successful { get; }
}
