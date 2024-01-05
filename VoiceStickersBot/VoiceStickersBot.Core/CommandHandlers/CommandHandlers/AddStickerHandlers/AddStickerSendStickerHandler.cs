using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;
using VoiceStickersBot.Core.Repositories.StickersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerSendStickerHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly AddStickerSendStickerArguments commandArguments;
    private readonly IStickersRepository stickersRepository;

    public AddStickerSendStickerHandler(
        AddStickerSendStickerArguments commandArguments, 
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
        
        return new AddStickerSendStickerResult(commandArguments.ChatId, sticker);
    }
}