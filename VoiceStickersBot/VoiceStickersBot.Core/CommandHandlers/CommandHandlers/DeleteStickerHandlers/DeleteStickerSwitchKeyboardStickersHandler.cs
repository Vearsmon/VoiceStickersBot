using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeleteStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeleteStickerResults;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeleteStickerHandlers;

public class DeleteStickerSwitchKeyboardStickersHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeleteSticker;

    private readonly DeleteStickerSwitchKeyboardStickersArguments commandArguments;
    private readonly IStickerPacksRepository stickerPacksRepository;

    public DeleteStickerSwitchKeyboardStickersHandler(
        DeleteStickerSwitchKeyboardStickersArguments commandArguments,
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
        var countOnPage = commandArguments.StickersOnPage;

        var callbackPrefix = "DS:SendSticker";
        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardStickers(
            callbackPrefix,
            stickers,
            pageFrom,
            pageTo,
            countOnPage);
        
        var actionRow = new List<InlineKeyboardButtonDto>
        {
            new ("Назад", "DS:SwKbdPc:0:Increase:10")
        };
        buttons.Add(actionRow);
        
        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "DS:SwKbdSt",
            commandArguments.StickerPackId.ToString(),
            pageTo,
            countOnPage,
            stickers.Count);
        
        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new DeleteStickerSwitchKeyboardStickersResult(
            commandArguments.ChatId,
            keyboard,
            commandArguments.StickerPackId,
            commandArguments.BotMessageId);
    }
}