using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.TestClient.Handlers;

/// <summary>
/// Handles /start message
/// </summary>
public class StartHandler : IMessageHandler<Message>
{
    /// <inheritdoc/>
    public bool CanHandle(Message message)
    {
        return message.Data is "/start";
    }

    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Hello, I'm working!", cancellationToken: cancellationToken);
    }

    public HandlerType Type => HandlerType.MessageHandler;

    public bool Successful { get; }
}