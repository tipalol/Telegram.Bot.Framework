using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.Utils;

namespace Telegram.Bot.Framework.WebClient.Handlers.Callbacks;

public class SomeCallbackHandler : IMessageHandler<Message>
{
    public bool CanHandle(Message message)
    {
        return message.Data is "some_request";
    }

    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Alright, you?", cancellationToken: cancellationToken);
        
        // we must say OK to Telegram API, so it knows callback is handled
        await botClient.CallbackOk(message, cancellationToken);
    }

    public HandlerType Type => HandlerType.MessageHandler;
    public bool Successful { get; }
}