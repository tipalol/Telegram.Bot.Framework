using Telegram.Bot.Types;

namespace Telegram.Bot.Framework.Handlers.Utils;

/// <summary>
/// Callback message occurs when user click on inline button 
/// </summary>
public class CallbackMessage : Message
{
    /// <summary>
    /// Callback id used to answer callback request
    /// </summary>
    public required string Id { get; init; }
    
    /// <summary>
    /// Map callback query to callback message type
    /// </summary>
    /// <param name="query">Telegram Bot API query or game query</param>
    public static CallbackMessage From(CallbackQuery query)
    {
        return new CallbackMessage()
        {
            Id = query.Id,
            // Support callback query and game query
            Data = query.Data ?? query.GameShortName,
            ChatId = query.Message!.Chat.Id
        };
    }
}