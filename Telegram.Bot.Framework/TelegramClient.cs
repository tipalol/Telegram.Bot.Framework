using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Configuration;
using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Internal;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.Pipelines;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Framework;

/// <inheritdoc />
public sealed class TelegramClient : ITelegramClient
{
    /// <summary>
    /// Telegram Bot API client
    /// </summary>
    private readonly ITelegramBotClient _client;
    private UpdateHandler _updateHandler;

    public TelegramClient(TelegramSettings settings)
    {
        var token = settings.Token;
        
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentException("Telegram token is missing or invalid in the configuration.");
        }
        
        _client = new TelegramBotClient(token);
    }

    // Receiver options, specify update types we capture, timeout and so on
    private readonly ReceiverOptions _receiverOptions = new()
    {
        // 1 - Messages (text, photo/video, voice messages)
        // 2 - Inline buttons
        AllowedUpdates =
        [
            UpdateType.Message,
            UpdateType.CallbackQuery
        ],
        // Handle updates accused when offline
        // True - do not handle, False (default) - handle
        ThrowPendingUpdates = true
    };
    
    /// <inheritdoc/>
    public async Task Start()
    {
        using var cts = new CancellationTokenSource();
        
        // UpdateHandler - handler for incoming updates
        // ErrorHandler - Bot API error handler
        _client.StartReceiving(_updateHandler.Handle, ErrorHandler.Handle, _receiverOptions, cts.Token); // Запускаем бота
        
        await Task.Delay(Timeout.Infinite, cts.Token); // Delay for infinite bot receiving
    }

    public ITelegramClient ConfigureBasePipelines(IEnumerable<HandlerBase<Message>> messageHandlers,
        IEnumerable<HandlerBase<Message>>? callbackHandlers = null)
    {
        _updateHandler = new UpdateHandler(messageHandlers, callbackHandlers);
        
        return this;
    }
}