using Telegram.Bot.Framework.Handlers.Base;
using Telegram.Bot.Framework.Handlers.Utils;

namespace Telegram.Bot.Framework.Handlers;

public abstract class Middleware : HandlerBase<Message>
{
    public bool Successful { get; protected set; }
}