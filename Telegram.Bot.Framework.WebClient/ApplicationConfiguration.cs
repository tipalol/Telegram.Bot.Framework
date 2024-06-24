using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Pipelines;
using Telegram.Bot.Framework.WebClient.Handlers;
using Telegram.Bot.Framework.WebClient.Handlers.Callbacks;
using Telegram.Bot.Framework.WebClient.Handlers.Middlewares;
using Telegram.Bot.Framework.WebClient.Services;
using Telegram.Bot.Framework.WebClient.Services.Abstractions;

namespace Telegram.Bot.Framework.WebClient;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureTelegram(ConfigureTelegramHandlers);
        
        return services;
    }
    
    /// <summary>
    /// Add Telegram client to DI and configure it
    /// </summary>
    private static IServiceCollection ConfigureTelegram(this IServiceCollection services, Func<IServiceProvider, bool> handlersSetter)
    {
        services.AddSingleton<ITelegramClient, TelegramClient>();
        services.AddSingleton<ISubscriptionService, SubscriptionService>();

        handlersSetter.Invoke(services.BuildServiceProvider());

        return services;
    }
    
    /// <summary>
    /// Configure Telegram handlers
    /// </summary>
    private static bool ConfigureTelegramHandlers(IServiceProvider serviceProvider)
    {
        var messageHandlers = new PipelineBuilder()
            .WithMiddleware(new SubscriptionMiddleware(serviceProvider))
            .WithMessageHandler<StartHandler>()
            .WithMessageHandler<MenuHandler>()
            .WithMessageHandler(new TextHandler(serviceProvider))
            .Build();

        var callbackHandlers = new PipelineBuilder()
            .WithMessageHandler<SomeCallbackHandler>()
            .Build();
        
        PipelinesManager.ConfigureBase(messageHandlers, callbackHandlers);
        
        return true;
    }
    
    /// <summary>
    /// Start receiving updates from Telegram
    /// </summary>
    public static void StartTelegram(this WebApplication app)
    {
        var client = app.Services.GetRequiredService<ITelegramClient>();
        Task.Run(client.Start);
    }
}