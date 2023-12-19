using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public abstract class CommandHandlerFactoryBase<TCommandArguments> : ICommandHandlerFactory
    where TCommandArguments : class, ICommandArguments
{
    public abstract CommandType CommandType { get; }

    public ICommandHandler CreateCommandHandler(ICommandArguments commandArguments)
    {
        if (commandArguments is not TCommandArguments typedCommand)
            throw new InvalidOperationException(
                $"Invalid command arguments type [{commandArguments.GetType()}] for [{CommandType}] command handler");

        return CreateCommandHandler(typedCommand);
    }

    protected abstract ICommandHandler CreateCommandHandler(TCommandArguments commandArguments);
}