using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.TestClient.Handlers;

/// <summary>
/// Handles all text messages
/// </summary>
public class TextHandler : MessageHandler
{
    public override bool CanHandle(Message message)
    {
        return true;
    }

    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, $"You said: {message.Data}",
            cancellationToken: cancellationToken);
    }
}