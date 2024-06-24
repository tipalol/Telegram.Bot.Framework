using Telegram.Bot.Exceptions;

namespace Telegram.Bot.Framework.Handlers.Internal;

public static class ErrorHandler
{
    /// <summary>
    /// Handle API Telegram errors
    /// </summary>
    public static Task Handle(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        // Error's code and error's message
        var errorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}