using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.TestClient.Handlers.Middlewares;

/// <summary>
/// Checks user active subscription
/// </summary>
public class SubscriptionMiddleware : Middleware
{
    /// <inheritdoc />
    public override bool CanHandle(Message message)
    {
        return message.Data is not "/start";
    }

    /// <inheritdoc />
    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
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
}