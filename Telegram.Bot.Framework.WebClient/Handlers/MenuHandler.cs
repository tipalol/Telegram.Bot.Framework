using Telegram.Bot.Framework.Enums;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.WebClient.Utils;

namespace Telegram.Bot.Framework.WebClient.Handlers;

public class MenuHandler : IMessageHandler<Message>
{
    public bool CanHandle(Message message)
    {
        return message.Data is "/menu";
    }

    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Main menu", replyMarkup: MenuProvider.MainMenu,
            cancellationToken: cancellationToken);
    }

    public HandlerType Type => HandlerType.MessageHandler;
    public bool Successful { get; }
}