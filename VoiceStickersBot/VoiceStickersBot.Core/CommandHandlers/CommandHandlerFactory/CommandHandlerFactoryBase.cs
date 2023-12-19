using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public abstract class CommandHandlerFactoryBase<StepNameType, TCommandArguments> : ICommandHandlerFactory<StepNameType>
    where TCommandArguments : class, ICommandArguments<StepNameType>
    where StepNameType : Enum
{
    public abstract CommandType CommandType { get; }

    public ICommandHandler CreateCommandHandler(ICommandArguments<StepNameType> commandArguments)
    {
        if (commandArguments is not TCommandArguments typedCommand)
            throw new InvalidOperationException(
                $"Invalid command arguments type [{commandArguments.GetType()}] for [{CommandType}] command handler");

        return CreateCommandHandler(typedCommand);
    }

    protected abstract ICommandHandler CreateCommandHandler(TCommandArguments commandArguments);
}