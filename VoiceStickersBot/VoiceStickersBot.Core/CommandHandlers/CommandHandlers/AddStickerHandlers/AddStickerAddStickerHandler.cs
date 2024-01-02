using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerAddStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private AddStickerAddStickerArguments commandArguments;
    private StickerPacksRepository stickerPacksRepository;

    public AddStickerAddStickerHandler(AddStickerAddStickerArguments commandArguments, 
        StickerPacksRepository stickerPacksRepository)
    {
        this.commandArguments = commandArguments;
        this.stickerPacksRepository = stickerPacksRepository;
    }

    public Task<ICommandResult> Handle()
    {
        throw new NotImplementedException();
    }
}