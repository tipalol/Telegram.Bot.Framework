using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Framework.UI;

public class ReplyMenuBuilder
{
    private readonly List<KeyboardButton> _buttons = [];

    public ReplyKeyboardMarkup Build()
    {
        var menu = new ReplyKeyboardMarkup(_buttons);

        return menu;
    }

    public ReplyMenuBuilder WithButton(string text)
    {
        var button = new KeyboardButton(text);
        _buttons.Add(button);

        return this;
    }
}