using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeleteStickerResults;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeleteStickerHandlers;

public class DeleteStickerDeleteStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeleteSticker;

    private readonly DeleteStickerDeleteStickerArguments commandArguments;
    private readonly IStickersRepository stickersRepository;

    public DeleteStickerDeleteStickerHandler(
        DeleteStickerDeleteStickerArguments commandArguments,
        IStickersRepository stickersRepository)
    {
        this.commandArguments = commandArguments;
        this.stickersRepository = stickersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        await stickersRepository
            .DeleteAsync(commandArguments.StickerPackId, commandArguments.StickerId)
            .ConfigureAwait(false);

        return new DeleteStickerDeleteStickerResult(commandArguments.ChatId);
    }
}