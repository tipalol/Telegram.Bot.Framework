using Telegram.Bot.Framework.WebClient.Services.Abstractions;

namespace Telegram.Bot.Framework.WebClient.Services;

/// <inheritdoc />
internal class SubscriptionService : ISubscriptionService
{
    /// <inheritdoc />
    public bool CheckSubscription(long telegramId)
    {
        return true;
    }
}