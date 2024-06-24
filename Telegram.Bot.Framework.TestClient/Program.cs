using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Configuration;
using Telegram.Bot.Framework.Pipelines;
using Telegram.Bot.Framework.TestClient.Handlers;
using Telegram.Bot.Framework.TestClient.Handlers.Middlewares;

var settings = new TelegramSettings
{
    Token = "your_api_token"
};

var handlers = new PipelineBuilder()
    .WithMiddleware(new SubscriptionMiddleware())
    .WithMessageHandler(new StartHandler())
    .WithMessageHandler(new TextHandler())
    .Build();

// initialize telegram client
ITelegramClient telegramClient = new TelegramClient(settings)
    .ConfigureBasePipelines(handlers);

await telegramClient.Start();