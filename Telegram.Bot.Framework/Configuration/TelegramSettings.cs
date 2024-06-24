namespace Telegram.Bot.Framework.Configuration;

/// <summary>
/// Settings for Telegram Bot API
/// </summary>
public sealed class TelegramSettings
{
    /// <summary>
    /// Authorization token
    /// </summary>
    public required string Token { get; init; }
}