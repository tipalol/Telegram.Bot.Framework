using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.Utils;

namespace Telegram.Bot.Framework.WebClient.Handlers.Callbacks;

public class SomeCallbackHandler : MessageHandler
{
    public override bool CanHandle(Message message)
    {
        return message.Data is "some_request";
    }

    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Alright, you?", cancellationToken: cancellationToken);
        
        // we must say OK to Telegram API, so it knows callback is handled
        await botClient.CallbackOk(message, cancellationToken);
    }
}