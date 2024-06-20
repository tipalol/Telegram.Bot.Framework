using Telegram.Bot.Framework.WebClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication(builder.Configuration);

var app = builder.Build();

app.StartTelegram();

app.Run();