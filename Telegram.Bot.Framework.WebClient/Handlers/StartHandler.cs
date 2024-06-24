using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.WebClient.Handlers;

/// <summary>
/// Handles /start message
/// </summary>
public class StartHandler : MessageHandler
{
    /// <inheritdoc/>
    public override bool CanHandle(Message message)
    {
        return message.Data is "/start";
    }

    /// <inheritdoc/>
    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Hello, I'm working!", cancellationToken: cancellationToken);
    }
}