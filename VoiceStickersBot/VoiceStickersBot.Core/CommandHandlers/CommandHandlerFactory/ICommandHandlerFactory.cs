using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlerFactory;

public interface ICommandHandlerFactory<StepNameType>
    where StepNameType : Enum
{
    CommandType CommandType { get; }

    public ICommandHandler CreateCommandHandler(ICommandArguments<StepNameType> command);
}