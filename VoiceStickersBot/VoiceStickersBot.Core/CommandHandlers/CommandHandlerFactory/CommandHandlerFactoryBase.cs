using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;
using VoiceStickersBot.Core.Commands;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public abstract class CommandHandlerFactoryBase<TCommand> : ICommandHandlerFactory
    where TCommand : class, ICommand
{
    public abstract Type CommandType { get; }

    public ICommandHandler CreateCommandHandler(ICommand command)
    {
        if (command is not TCommand typedCommand)
            throw new InvalidOperationException(
                $"Invalid command type [{command.GetType()}] for [{CommandType}] command handler");

        return CreateCommandHandler(typedCommand);
    }

    protected abstract ICommandHandler CreateCommandHandler(TCommand command);
}