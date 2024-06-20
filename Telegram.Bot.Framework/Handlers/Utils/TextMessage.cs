namespace Telegram.Bot.Framework.Handlers.Utils;

/// <summary>
/// Text message when user send text
/// </summary>
public class TextMessage : Message
{
    public required int Id { get; init; }

    public static TextMessage From(global::Telegram.Bot.Types.Message message)
    {
        return new TextMessage
        {
            Id = message.MessageId,
            Data = message.Text!,
            ChatId = message.Chat.Id,
            UserName = message.From?.Username
        };
    }
}