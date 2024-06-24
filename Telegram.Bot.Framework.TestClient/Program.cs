using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Configuration;
using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;
using Telegram.Bot.Framework.TestClient.Handlers;
using Telegram.Bot.Framework.TestClient.Handlers.Middlewares;

var settings = new TelegramSettings
{
    Token = "7426407451:AAFwhrNZGMy5SHlv0bF_MlT_U91Qlj52kQo"
};

// initialize telegram client
ITelegramClient telegramClient = new TelegramClient(settings);

// define your handlers (note that order is important)
List<HandlerBase<Message>> handlers = 
[
    new SubscriptionMiddleware(),
    new StartHandler(),
    new TextHandler()
];

HandlersConfiguration.Configure(handlers);

await telegramClient.Start();