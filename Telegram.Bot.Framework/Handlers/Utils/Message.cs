namespace Telegram.Bot.Framework.Handlers.Utils;

/// <summary>
/// Base class for all messages
/// </summary>
public abstract class Message
{
    /// <summary>
    /// Chat id when message was sent
    /// </summary>
    public required long ChatId { get; init; }

    /// <summary>
    /// Text data inside message
    /// </summary>
    public required string Data { get; init; }
    
    /// <summary>
    /// Username if sender
    /// </summary>
    public string? UserName { get; init; }
}