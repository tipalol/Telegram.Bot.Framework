using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.WebClient.Handlers;

/// <summary>
/// Handles all text messages
/// </summary>
public class TextHandler(IServiceProvider serviceProvider) : IMessageHandler<Message>
{
    public bool CanHandle(Message message)
    {
        return true;
    }

    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, $"You said: {message.Data}",
            cancellationToken: cancellationToken);
    }

    public HandlerType Type => HandlerType.MessageHandler;
    public bool Successful { get; }
}