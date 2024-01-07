using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackSendImportInstructionsHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly SharePackSendImportInstructionsArguments commandArguments;

    public SharePackSendImportInstructionsHandler(SharePackSendImportInstructionsArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        return new SharePackSendImportInstructionsResult(commandArguments.ChatId);
    }
}