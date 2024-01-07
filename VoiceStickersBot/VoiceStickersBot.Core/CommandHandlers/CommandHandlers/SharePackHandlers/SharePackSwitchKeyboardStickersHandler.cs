using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackSwitchKeyboardStickersHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly SharePackSwitchKeyboardStickersArguments commandArguments;
    private readonly IStickerPacksRepository stickerPacksRepository;
    
    public SharePackSwitchKeyboardStickersHandler(
        SharePackSwitchKeyboardStickersArguments commandArguments,
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

        var callbackPrefix = "SP:SendSticker";
        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardStickers(
            callbackPrefix,
            stickers,
            pageFrom,
            pageTo,
            countOnPage);

        var actionRow = new List<InlineKeyboardButtonDto>
        {
            new ("Назад", "SP:SwKbdPc:0:Increase:10"),
            new ("Экспорт", $"SP:SendPackId:{commandArguments.StickerPackId}"),
        };
        buttons.Add(actionRow);
        
        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "SP:SwKbdSt",
            commandArguments.StickerPackId.ToString(),
            pageTo,
            countOnPage,
            stickers.Count);
        
        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new SharePackSwitchKeyboardStickersResult(
            commandArguments.ChatId,
            keyboard,
            commandArguments.StickerPackId,
            commandArguments.BotMessageId);
    }
}