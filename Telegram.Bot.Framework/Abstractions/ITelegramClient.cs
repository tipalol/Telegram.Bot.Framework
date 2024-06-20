namespace Telegram.Bot.Framework.Abstractions;

/// <summary>
/// Client for Telegram Bot API
/// </summary>
public interface ITelegramClient
{
    /// <summary>
    /// Start receiving messages
    /// </summary>
    public Task Start();
}