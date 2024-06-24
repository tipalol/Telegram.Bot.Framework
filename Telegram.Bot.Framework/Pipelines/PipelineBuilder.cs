using Telegram.Bot.Framework.Handlers;
using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Pipelines;

public class PipelineBuilder
{
    private List<HandlerBase<Message>> Handlers { get; } = [];

    public PipelineBuilder WithMiddleware(Middleware middleware)
    {
        Handlers.Add(middleware);

        return this;
    }
        
    public PipelineBuilder WithMessageHandler(MessageHandler handler)
    {
        Handlers.Add(handler);

        return this;
    }
        
    public PipelineBuilder WithCustomHandler(HandlerBase<Message> handler)
    {
        Handlers.Add(handler);

        return this;
    }

    public IEnumerable<HandlerBase<Message>> Build()
    {
        return Handlers;
    }
}