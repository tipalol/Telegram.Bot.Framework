using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;
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
        List<HandlerBase<Message>> messageHandlers = 
        [
            new SubscriptionMiddleware(serviceProvider),
            new StartHandler(),
            new MenuHandler(),
            new TextHandler(serviceProvider)
        ];
        
        List<HandlerBase<Message>>? callbackHandlers = 
        [
            new SomeCallbackHandler()
        ];
        
        HandlersConfiguration.Configure(messageHandlers, callbackHandlers);
        
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