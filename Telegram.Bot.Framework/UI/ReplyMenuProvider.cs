using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Framework.UI;

/// <summary>
/// Провайдер reply меню
/// </summary>
public static class ReplyMenuProvider
{
    public static ReplyKeyboardMarkup Test { get; } = new ReplyKeyboardMarkup(
        new List<KeyboardButton[]>()
        {
            new KeyboardButton[]
            {
                new KeyboardButton("Привет!"),
                new KeyboardButton("Пока!"),
            },
            new KeyboardButton[]
            {
                new KeyboardButton("Позвони мне!")
            },
            new KeyboardButton[]
            {
                new KeyboardButton("Напиши моему соседу!")
            }
        })
    {
        // автоматическое изменение размера клавиатуры
        ResizeKeyboard = true,
    };
    
    public static ReplyKeyboardMarkup BotMenu { get; } = new ReplyKeyboardMarkup(
        new List<KeyboardButton[]>()
        {
            new KeyboardButton[]
            {
                new KeyboardButton("Моя подписка"),
                new KeyboardButton("Пока!"),
            },
            new KeyboardButton[]
            {
                new KeyboardButton("Позвони мне!")
            },
            new KeyboardButton[]
            {
                new KeyboardButton("Напиши моему соседу!")
            }
        })
    {
        // автоматическое изменение размера клавиатуры
        ResizeKeyboard = true,
    };
}