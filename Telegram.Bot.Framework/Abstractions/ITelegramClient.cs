using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Types.ReplyMarkups;

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

    Task SendText(long chatId, string text, InlineKeyboardMarkup? replyMarkup = null);
    
    Task SendImage(long chatId, string imageUrl, string text, InlineKeyboardMarkup? replyMarkup = null);
}