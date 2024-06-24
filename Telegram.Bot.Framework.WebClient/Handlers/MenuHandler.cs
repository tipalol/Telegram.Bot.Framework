using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.WebClient.Utils;

namespace Telegram.Bot.Framework.WebClient.Handlers;

public class MenuHandler : MessageHandler
{
    /// <inheritdoc/>
    public override bool CanHandle(Message message)
    {
        return message.Data is "/menu";
    }

    /// <inheritdoc/>
    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Main menu", replyMarkup: MenuProvider.MainMenu,
            cancellationToken: cancellationToken);
    }
}