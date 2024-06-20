using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Handlers;

/// <summary>
/// Handlers configuration contains all message handlers to be process user's message in pipeline
/// <remarks>Important: Order them carefully</remarks> 
/// </summary>
public static class HandlersConfiguration
{
    public static List<IMessageHandler<Message>>? MessageHandlers { get; set; }
    public static List<IMessageHandler<Message>>? CallbacksHandlers { get; set; }

    /// <summary>
    /// Check if there is handlers configured
    /// </summary>
    public static bool IsValid => MessageHandlers?.Count != 0 || CallbacksHandlers?.Count != 0;
}