using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Pipelines;

public class PipelinesManager
{
    public static IEnumerable<HandlerBase<Message>> BaseMessageHandlers { get; private set; } = [];
    public static IEnumerable<HandlerBase<Message>>? BaseCallbacksHandlers { get; private set; } = [];

    public static void ConfigureBase(IEnumerable<HandlerBase<Message>> messageHandlers,
        IEnumerable<HandlerBase<Message>>? callbacksHandlers = null)
    {
        BaseMessageHandlers = messageHandlers;
        BaseCallbacksHandlers = callbacksHandlers;
    }
}