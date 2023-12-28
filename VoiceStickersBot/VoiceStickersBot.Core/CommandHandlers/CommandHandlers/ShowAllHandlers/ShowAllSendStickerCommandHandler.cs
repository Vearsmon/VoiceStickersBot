using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

public class ShowAllSendStickerCommandHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ShowAllSendStickerCommandArguments commandArguments;
    private readonly StickerPacksRepository stickerPacksRepository;
    
    public ShowAllSendStickerCommandHandler(
        ShowAllSendStickerCommandArguments commandArguments, 
        StickerPacksRepository stickerPacksRepository)
    {
        this.commandArguments = commandArguments;
        this.stickerPacksRepository = stickerPacksRepository; //TODO: дима замути гет стикер 
    }
    
    public async Task<ICommandResult> Handle()
    {
        var stickerPack = await stickerPacksRepository
            .GetStickerPackAsync(commandArguments.StickerPackId, true)
            .ConfigureAwait(false);

        var sticker = stickerPack.Stickers!.First(p => p.StickerPackId == commandArguments.StickerId);
        return new ShowAllSendStickerResult(commandArguments.ChatId, sticker);
    }
}