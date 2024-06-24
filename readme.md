# Telegram Bot API Framework

This is a simple and lightweight framework that helps you create Telegram bots faster. It uses the [Telegram Bot API](https://core.telegram.org/bots)

## Usage

Example projects can be found at:
- [Console Application example](https://github.com/tipalol/Telegram.Bot.Framework/tree/main/Telegram.Bot.Framework.TestClient)
- [Web Application example](https://github.com/tipalol/Telegram.Bot.Framework/tree/main/Telegram.Bot.Framework.WebClient)

Here's an example of how to use the framework to create a simple bot that replies the same text when a user sends a text message:

```c#
// Program.cs

var settings = new TelegramSettings
{
    Token = "your_api_token"
};

var handlers = new PipelineBuilder()
    .WithMiddleware<SubscriptionMiddleware>()
    .WithMessageHandler<StartHandler>()
    .WithMessageHandler<TextHandler>()
    .Build();

// initialize telegram client
ITelegramClient telegramClient = new TelegramClient(settings)
    .ConfigureBasePipelines(handlers);

await telegramClient.Start();
```

```c#
// TextHandler.cs.cs

/// <summary>
/// Handles all text messages
/// </summary>
public class TextHandler : MessageHandler
{
    public override bool CanHandle(Message message)
    {
        return true;
    }

    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, $"You said: {message.Data}",
            cancellationToken: cancellationToken);
    }
}
```

### Chat commands

You can add more commands by adding new handlers like this one:

```c#
// StartHandler.cs

/// <summary>
/// Handles /start message
/// </summary>
public class StartHandler : MessageHandler
{
    /// <inheritdoc/>
    public override bool CanHandle(Message message)
    {
        return message.Data is "/start";
    }

    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Hello, I'm working!", cancellationToken: cancellationToken);
    }
}
```

### Callback data

Callback data is used to pass information between different functions in your bot. For example, you could have a `some_request` callback called and then handle it in another function like this:

```c#
// SomeCallbackHandler.cs

public class SomeCallbackHandler : MessageHandler
{
    public override bool CanHandle(Message message)
    {
        return message.Data is "some_request";
    }

    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Alright, you?", cancellationToken: cancellationToken);
        
        // we must say OK to Telegram API, so it knows callback is handled
        await botClient.CallbackOk(message, cancellationToken);
    }
}
```

### Reply markups

Reply markups are used to provide additional options or buttons for users to interact with your bot. You can create as many reply markups as you need, and they will be displayed below the message you send.

```c#
//MenuHandler.cs

public class MenuHandler : MessageHandler
{
    /// <inheritdoc/>
    public override bool CanHandle(Message message)
    {
        return message.Data is "/menu";
    }

    /// <inheritdoc/>
    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(message.ChatId, "Main menu", replyMarkup: MenuProvider.MainMenu,
            cancellationToken: cancellationToken);
    }
}

// MenuProvider.cs

public static class MenuProvider
{
    public static InlineKeyboardMarkup MainMenu { get; } = new InlineMenuBuilder()
        .WithButton("What's up?", "some_request")
        .Build();
}
```

### Middlewares

```c#
//SubscriptionMiddleware.cs

public class SubscriptionMiddleware(IServiceProvider serviceProvider) : Middleware
{
    /// <inheritdoc />
    public override bool CanHandle(Message message)
    {
        return message.Data is not "/start";
    }

    /// <inheritdoc/>
    public override async Task HandleAsync(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken)
    {
        var subscriptionService = serviceProvider.GetRequiredService<ISubscriptionService>();
        var subscribed = subscriptionService.CheckSubscription(message.ChatId);

        if (subscribed)
        {
            Successful = true;
            return;
        }

        await botClient.SendTextMessageAsync(message.ChatId, 
            "We can't find your subscription. Access is denied", cancellationToken: cancellationToken);
    }
}
```