using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.StartCommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers.StartHandlers;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class StartCommandHandlerFactory : CommandHandlerFactoryBase<IStartCommandArguments>
{
    public override CommandType CommandType => CommandType.Start;
    protected override ICommandHandler CreateCommandHandler(IStartCommandArguments commandArguments)
    {
        return new StartStartHandler((StartStartArguments)commandArguments);
    }
}