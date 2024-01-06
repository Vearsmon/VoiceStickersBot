using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Core.Contracts;
using VoiceStickersBot.Core.Repositories.StickerPacksRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

public class ShowAllSwitchKeyboardStickersCommandHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ShowAllSwitchKeyboardStickersCommandArguments commandArguments;
    private readonly IStickerPacksRepository stickerPacksRepository;

    public ShowAllSwitchKeyboardStickersCommandHandler(
        ShowAllSwitchKeyboardStickersCommandArguments commandArguments,
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

        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardStickers(
            "SA:SendSticker",
            stickers,
            pageFrom,
            pageTo,
            countOnPage);

        buttons.Add(new InlineKeyboardButtonDto("Назад", $"SA:SwKbdPc:{commandArguments.ChatId}:0:Increase:10"));

        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "SA:SwKbdSt",
            commandArguments.StickerPackId.ToString(),
            pageTo,
            countOnPage,
            stickers.Count);
        
        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new ShowAllSwitchKeyboardStickersResult(commandArguments.ChatId, keyboard,
            commandArguments.BotMessageId);
    }
}