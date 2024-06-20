using Microsoft.Extensions.Configuration;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Interfaces;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.TestClient.Handlers;
using Telegram.Bot.Framework.TestClient.Handlers.Middlewares;

// without appsettings.json
var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection([new KeyValuePair<string, string?>("TelegramSettings:Token", "your_access_token")])
    .Build();

// initialize telegram client
ITelegramClient telegramClient = new TelegramClient(configuration);

// define your handlers (note that order is important)
List<IMessageHandler<Message>> handlers = 
[
    new SubscriptionMiddleware(),
    new StartHandler(),
    new TextHandler()
];

HandlersConfiguration.Configure(handlers);

await telegramClient.Start();