using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Message = Telegram.Bot.Framework.Handlers.Utils.Message;

namespace Telegram.Bot.Framework.Handlers.Internal;

public class UpdateHandler(
    IEnumerable<HandlerBase<Message>> messageHandlers,
    IEnumerable<HandlerBase<Message>>? callbackHandlers = null)
{
    /// <summary>
    /// Telegram updates handler
    /// </summary>
    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            // Handle different update types
            switch (update.Type)
            {
                case UpdateType.Message:
                {
                    // Text data
                    var message = update.Message;
                    // Sender info
                    var user = message!.From;
                    // Chat information
                    var chat = message.Chat;
                    
                    // Output for debug purposes with message and sender info
                    Console.WriteLine($"{user!.FirstName} ({user.Id}) sent: {message.Text} in chat {message.Chat.Id}");

                    // Handle different types of message
                    switch (message.Type)
                    {
                        case MessageType.Text:
                        {
                            var messageProcessor = new MessageProcessor(messageHandlers);
                            await messageProcessor.ProcessAsync(TextMessage.From(message), botClient, cancellationToken);
                            
                            return;
                        }

                        // If it's not handling type
                        default:
                        {
                            await botClient.SendTextMessageAsync(
                                chat.Id,
                                "Only text is supported!", cancellationToken: cancellationToken);
                            return;
                        }
                    }
                }
                case UpdateType.CallbackQuery:
                {
                    // Inline button information including callback id and text data
                    var callbackQuery = update.CallbackQuery;
                    // Sender info
                    var user = callbackQuery!.From;

                    // Output for debug purposes with button and sender info
                    Console.WriteLine($"{user.FirstName} ({user.Id}) pressed the button: {callbackQuery.Data}");

                    var messageProcessor = new MessageProcessor(callbackHandlers!);
                    await messageProcessor.ProcessAsync(CallbackMessage.From(callbackQuery), botClient, cancellationToken);
                    
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}