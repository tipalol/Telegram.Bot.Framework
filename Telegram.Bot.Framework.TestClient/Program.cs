﻿using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Configuration;
using Telegram.Bot.Framework.Pipelines;
using Telegram.Bot.Framework.TestClient.Handlers;
using Telegram.Bot.Framework.TestClient.Handlers.Middlewares;

var settings = new TelegramSettings
{
    Token = "7426407451:AAFwhrNZGMy5SHlv0bF_MlT_U91Qlj52kQo"
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