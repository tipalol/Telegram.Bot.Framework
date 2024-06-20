using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.WebClient.Services.Abstractions;

namespace Telegram.Bot.Framework.WebClient.Handlers.Middlewares;

/// <summary>
/// Checks user active subscription
/// </summary>
public class SubscriptionMiddleware(IServiceProvider serviceProvider) : IMessageHandler<Message>
{
    /// <inheritdoc />
    public bool CanHandle(Message message)
    {
        return message.Data is not "/start";
    }

    /// <inheritdoc />
    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        var subscriptionService = serviceProvider.GetRequiredService<ISubscriptionService>();
        var subscribed = subscriptionService.CheckSubscription(message.ChatId);

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