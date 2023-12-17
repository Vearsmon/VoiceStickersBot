using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.EnterCommand;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class EnterCommandHandler : ICommandHandler
{
    public Type CommandType => typeof(EnterCommand);
    private readonly ICommand command;

    public EnterCommandHandler(ICommand command)
    {
        this.command = command;
    }

    public ICommandResult Handle()
    {
        throw new NotImplementedException();
    }
}