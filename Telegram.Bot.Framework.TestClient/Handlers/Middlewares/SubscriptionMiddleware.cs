using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.TestClient.Handlers.Middlewares;

/// <summary>
/// Checks user active subscription
/// </summary>
public class SubscriptionMiddleware : IMessageHandler<Message>
{
    /// <inheritdoc />
    public bool CanHandle(Message message)
    {
        return message.Data is not "/start";
    }

    /// <inheritdoc />
    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        // Check user subscription (for example in DB)
        var subscribed = await Task.FromResult(true);

        if (subscribed)
        {
            Successful = true;
            return;
        }

        await botClient.SendTextMessageAsync(message.ChatId, 
            "We can't find your subscription. Access is denied", cancellationToken: cancellationToken);
    }

    public HandlerType Type => HandlerType.Middleware;

    public bool Successful { get; private set; }
}