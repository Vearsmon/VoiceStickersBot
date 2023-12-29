using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.CreatePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.CreatePackResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.CreatePackHandlers;

public class CreatePackSendInstructionsHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.CreatePack;
    private readonly CreatePackSendInstructionsArguments commandArguments;

    public CreatePackSendInstructionsHandler(CreatePackSendInstructionsArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        return new CreatePackSendInstructionsResult(commandArguments.ChatId);
    }
}