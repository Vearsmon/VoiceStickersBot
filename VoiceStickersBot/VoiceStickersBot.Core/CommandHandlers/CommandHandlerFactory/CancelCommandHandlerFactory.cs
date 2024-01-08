using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CancelCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.CancelHandlers;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class CancelCommandHandlerFactory : CommandHandlerFactoryBase<ICancelCommandArguments>
{
    public override CommandType CommandType => CommandType.Cancel;
    protected override ICommandHandler CreateCommandHandler(ICancelCommandArguments commandArguments)
    {
        return new CancelCancelHandler((CacncelCancelArguments)commandArguments);
    }
}