using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Utils;

public static class TelegramBotClientExtensions
{
    public static async Task CallbackOk(this ITelegramBotClient client, Message message, CancellationToken token = default)
    {
        if (message is CallbackMessage callback)
        {
            await client.AnswerCallbackQueryAsync(callback.Id, cancellationToken: token);
        }
        else
        {
            throw new InvalidOperationException($"Failed to process callback message {message.Data}");
        }
    }
    
    public static async Task CallbackMessage(this ITelegramBotClient client, Message message, string data, CancellationToken token = default)
    {
        if (message is CallbackMessage callback)
        {
            await client.AnswerCallbackQueryAsync(callback.Id, data, cancellationToken: token);
        }
        else
        {
            throw new InvalidOperationException($"Failed to process callback message {message.Data}");
        }
    }
    
    public static async Task CallbackAlert(this ITelegramBotClient client, Message message, string data, CancellationToken token = default)
    {
        if (message is CallbackMessage callback)
        {
            await client.AnswerCallbackQueryAsync(callback.Id, data, showAlert: true, cancellationToken: token);
        }
        else
        {
            throw new InvalidOperationException($"Failed to process callback message {message.Data}");
        }
    }
}