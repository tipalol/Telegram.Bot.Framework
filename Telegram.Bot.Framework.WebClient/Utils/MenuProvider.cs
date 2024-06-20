using Telegram.Bot.Framework.UI;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Framework.WebClient.Utils;

public static class MenuProvider
{
    public static InlineKeyboardMarkup MainMenu { get; } = new InlineMenuBuilder()
        .WithButton("What's up?", "some_request")
        .Build();
}