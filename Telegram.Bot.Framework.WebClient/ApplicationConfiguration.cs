using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Framework.Configuration;
using Telegram.Bot.Framework.Handlers.Base;
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
        
        services.AddSingleton<ISubscriptionService, SubscriptionService>();
        
        return services;
    }
    
    /// <summary>
    /// Add Telegram client to DI and configure it.
    /// </summary>
    private static IServiceCollection ConfigureTelegram(this IServiceCollection services, Func<IServiceProvider, (IEnumerable<HandlerBase<Framework.Handlers.Utils.Message>>, IEnumerable<HandlerBase<Framework.Handlers.Utils.Message>>?)> handlersFactory)
    {
        services.AddSingleton(new TelegramSettings { Token = "your_api_token" });
        
        services.AddSingleton<ITelegramClient>(provider =>
        {
            var settings = provider.GetRequiredService<TelegramSettings>();
            var (messageHandlers, callbackHandlers) = handlersFactory(provider);
            return new TelegramClient(settings, messageHandlers, callbackHandlers);
        });

        return services;
    }
    
    /// <summary>
    /// Configure Telegram handlers.
    /// </summary>
    private static (IEnumerable<HandlerBase<Framework.Handlers.Utils.Message>>, IEnumerable<HandlerBase<Framework.Handlers.Utils.Message>>?) ConfigureTelegramHandlers(IServiceProvider serviceProvider)
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
        
        return (messageHandlers, callbackHandlers);
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