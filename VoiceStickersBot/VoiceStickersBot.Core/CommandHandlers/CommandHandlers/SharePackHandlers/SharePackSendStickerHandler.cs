using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackSendStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly SharePackSendStickerArguments commandArguments;
    private readonly IStickersRepository stickersRepository;

    public SharePackSendStickerHandler(
        SharePackSendStickerArguments commandArguments, 
        IStickersRepository stickersRepository)
    {
        this.commandArguments = commandArguments;
        this.stickersRepository = stickersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var sticker = await stickersRepository.GetAsync(
                commandArguments.StickerPackId,
                commandArguments.StickerId)
            .ConfigureAwait(false);
        
        return new SharePackSendStickerResult(
            commandArguments.ChatId,
            sticker,
            commandArguments.StickerPackId);
    }
}