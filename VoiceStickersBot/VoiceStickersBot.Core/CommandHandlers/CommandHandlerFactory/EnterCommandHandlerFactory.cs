using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands.EnterCommand;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public class EnterCommandHandlerFactory : CommandHandlerFactoryBase<EnterCommand>
{
    public override Type CommandType => typeof(EnterCommand);
    protected override ICommandHandler CreateCommandHandler(EnterCommand command)
    {
        return new EnterCommandHandler(command);
    }
}