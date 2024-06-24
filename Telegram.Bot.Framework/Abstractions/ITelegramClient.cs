using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Abstractions;

/// <summary>
/// Client for Telegram Bot API
/// </summary>
public interface ITelegramClient
{
    /// <summary>
    /// Start receiving messages
    /// </summary>
    Task Start();

    ITelegramClient ConfigureBasePipelines(IEnumerable<HandlerBase<Message>> messageHandlers,
        IEnumerable<HandlerBase<Message>>? callbackHandlers = null);
}