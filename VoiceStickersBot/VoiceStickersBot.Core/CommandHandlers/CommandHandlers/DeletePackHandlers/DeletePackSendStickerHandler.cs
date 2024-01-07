using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeletePackResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeletePackHandlers;

public class DeletePackSendStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeletePack;

    private readonly DeletePackSendStickerArguments commandArguments;
    private readonly IStickersRepository stickersRepository;

    public DeletePackSendStickerHandler(
        DeletePackSendStickerArguments commandArguments, 
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
        
        return new DeletePackSendStickerResult(
            commandArguments.ChatId,
            sticker,
            commandArguments.StickerPackId);
    }
}