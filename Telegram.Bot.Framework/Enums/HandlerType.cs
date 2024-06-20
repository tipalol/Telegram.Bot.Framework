namespace Telegram.Bot.Framework.Enums;

public enum HandlerType
{
    /// <summary>
    /// Base message handler
    /// </summary>
    MessageHandler = 0,
    /// <summary>
    /// Middleware handler for pipeline handling
    /// </summary>
    Middleware = 1
}