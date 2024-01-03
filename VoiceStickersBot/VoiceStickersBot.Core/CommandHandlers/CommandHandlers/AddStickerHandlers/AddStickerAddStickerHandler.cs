using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerAddStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private AddStickerAddStickerArguments commandArguments;
    private StickersRepository stickersRepository;

    public AddStickerAddStickerHandler(AddStickerAddStickerArguments commandArguments, 
        StickersRepository stickersRepository)
    {
        this.commandArguments = commandArguments;
        this.stickersRepository = stickersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        await stickersRepository.CreateAsync(
                Guid.NewGuid(),
                commandArguments.StickerName,
                "location", // еще хз че в локэйшн указывать
                commandArguments.StickerPackId)
            .ConfigureAwait(false);

        return new AddStickerAddStickerResult(
            commandArguments.ChatId,
            commandArguments.StickerName,
            commandArguments.FileId);
    }
}