using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeletePackResults;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeletePackHandlers;

public class DeletePackSwitchKeyboardStickersHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeletePack;

    private readonly DeletePackSwitchKeyboardStickersArguments commandArguments;
    private readonly IStickerPacksRepository stickerPacksRepository;
    
    public DeletePackSwitchKeyboardStickersHandler(
        DeletePackSwitchKeyboardStickersArguments commandArguments,
        IStickerPacksRepository stickerPacksRepository)
    {
        this.commandArguments = commandArguments;
        this.stickerPacksRepository = stickerPacksRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var stickerPack = await stickerPacksRepository
            .GetStickerPackAsync(commandArguments.StickerPackId, true)
            .ConfigureAwait(false);
        var stickers = stickerPack.Stickers ?? new List<Sticker>();

        var pageFrom = commandArguments.PageFrom;
        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var countOnPage = commandArguments.PacksOnPage;

        var callbackPrefix = "DP:SendSticker";
        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardStickers(
            callbackPrefix,
            stickers,
            pageFrom,
            pageTo,
            countOnPage);


        var actionRow = new List<InlineKeyboardButtonDto>
        {
            new ("Назад", "DP:SwKbdPc:0:Increase:10"),
            new ("Удалить пак", $"DP:Confirm:{commandArguments.StickerPackId}")
        };
        buttons.Add(actionRow);
        
        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "DP:SwKbdSt",
            commandArguments.StickerPackId.ToString(),
            pageTo,
            countOnPage,
            stickers.Count);
        
        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new DeletePackSwitchKeyboardStickersResult(
            commandArguments.ChatId,
            keyboard,
            commandArguments.StickerPackId,
            commandArguments.BotMessageId);
    }
}