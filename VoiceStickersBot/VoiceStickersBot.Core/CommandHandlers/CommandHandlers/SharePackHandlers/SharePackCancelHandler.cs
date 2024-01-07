using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackCancelHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly SharePackCancelArguments commandArguments;

    public SharePackCancelHandler(SharePackCancelArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}