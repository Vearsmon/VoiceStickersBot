using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackSendPackIdHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly SharePackSendPackIdArguments commandArguments;

    public SharePackSendPackIdHandler(SharePackSendPackIdArguments commandArguments)
    {
        this.commandArguments = commandArguments;
    }

    public async Task<ICommandResult> Handle()
    {
        return new SharePackSendPackIdResult(commandArguments.ChatId, commandArguments.StickerPackId);
    }
}