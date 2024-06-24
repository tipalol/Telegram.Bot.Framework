using Telegram.Bot.Framework.Handlers.Base;

namespace Telegram.Bot.Framework.Handlers.Utils;

/// <summary>
/// Message handling pipeline
/// </summary>
/// <param name="handlers">Message handlers <see cref="IMessageHandler"/></param>
public class MessageProcessor(IEnumerable<HandlerBase<Message>> handlers)
{
    /// <summary>
    /// Process the message
    /// </summary>
    public async Task ProcessAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        foreach (var handler in handlers)
        {
            // Check if handler can process the message
            // if not - continue pipeline
            if (handler.CanHandle(message) is false) continue;
            
            // Handle the message
            await handler.HandleAsync(message, botClient, cancellationToken);
                
            // If this is middleware and everything is ok - continue
            if (handler is Middleware { Successful: true }) continue;
                
            return;
        }

        // If message was unhandled print error
        await botClient.SendTextMessageAsync(
            message.ChatId,
            "Unsupported message", cancellationToken: cancellationToken);
    }
}