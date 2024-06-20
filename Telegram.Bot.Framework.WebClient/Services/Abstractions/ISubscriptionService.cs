namespace Telegram.Bot.Framework.WebClient.Services.Abstractions;

/// <summary>
/// Service for managing user subscriptions
/// </summary>
public interface ISubscriptionService
{
    /// <summary>
    /// Check if user has active subscription
    /// </summary>
    /// <param name="telegramId">Telegram Chat Id</param>
    /// <returns>True if user has active subscription</returns>
    bool CheckSubscription(long telegramId);
}