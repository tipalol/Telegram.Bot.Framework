# Telegram Bot API Framework

This is a simple and lightweight framework that helps you create Telegram bots faster. It uses the [Telegram Bot API](https://core.telegram.org/bots) 

## Usage

Here's an example of how to use the framework to create a simple bot that replies the same text when a user sends a text message:

```c#
// Program.cs

// initialize telegram client
ITelegramClient telegramClient = new TelegramClient(configuration);

// define your handlers (note that order is important)
List<IMessageHandler<Message>> handlers = 
[
    new TextHandler()
];

HandlersConfiguration.Configure(handlers);

await telegramClient.Start();
```

```c#
// TextHandler.cs.cs

/// <summary>
/// Handles all text messages
/// </summary>
public class TextHandler : IMessageHandler<Message>
{
    public bool CanHandle(Message message)
    {
        return true;
    }

    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, $"You said: {message.Data}",
            cancellationToken: cancellationToken);
    }

    public HandlerType Type => HandlerType.MessageHandler;
    public bool Successful { get; }
}
```

### Chat commands

You can add more commands by adding new handlers like this one:

```c#
// StartHandler.cs

/// <summary>
/// Handles /start message
/// </summary>
public class StartHandler : IMessageHandler<Message>
{
    /// <inheritdoc/>
    public bool CanHandle(Message message)
    {
        return message.Data is "/start";
    }

    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Hello, I'm working!", cancellationToken: cancellationToken);
    }

    public HandlerType Type => HandlerType.MessageHandler;
    public bool Successful { get; }
}
```

### Callback data

Callback data is used to pass information between different functions in your bot. For example, you could have a `some_request` callback called and then handle it in another function like this:

```c#
// SomeCallbackHandler.cs

public class SomeCallbackHandler : IMessageHandler<Message>
{
    public bool CanHandle(Message message)
    {
        return message.Data is "some_request";
    }

    public async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Alright, you?", cancellationToken: cancellationToken);
        
        // we must say OK to Telegram API, so it knows callback is handled
        await botClient.CallbackOk(message, cancellationToken);
    }

    public HandlerType Type => HandlerType.MessageHandler;
    public bool Successful { get; }
}
```

### Reply markups

Reply markups are used to provide additional options or buttons for users to interact with your bot. You can create as many reply markups as you need, and they will be displayed below the message you send.

```c#
//MenuHandler.cs

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

// MenuProvider.cs

public static class MenuProvider
{
    public static InlineKeyboardMarkup MainMenu { get; } = new InlineMenuBuilder()
        .WithButton("What's up?", "some_request")
        .Build();
}
```