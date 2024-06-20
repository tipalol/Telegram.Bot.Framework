using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Framework.UI;

/// <summary>
/// Uses builder style to create InlineKeyboardMarkup
/// </summary>
public class InlineMenuBuilder
{
    private readonly List<InlineKeyboardButton> _buttons = [];

    public InlineKeyboardMarkup Build()
    {
        var menu = new InlineKeyboardMarkup(_buttons);

        return menu;
    }

    public InlineMenuBuilder WithButton(string text, string data)
    {
        var button = InlineKeyboardButton.WithCallbackData(text, data);
        _buttons.Add(button);

        return this;
    }
    
    public InlineMenuBuilder WithLink(string text, string url)
    {
        var button = InlineKeyboardButton.WithUrl(text, url);
        _buttons.Add(button);

        return this;
    }
}