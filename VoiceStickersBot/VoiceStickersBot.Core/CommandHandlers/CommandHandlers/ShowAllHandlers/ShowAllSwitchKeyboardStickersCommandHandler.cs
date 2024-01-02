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
    private readonly StickerPacksRepository stickerPacksRepository;

    public ShowAllSwitchKeyboardStickersCommandHandler(
        ShowAllSwitchKeyboardStickersCommandArguments commandArguments,
        StickerPacksRepository stickerPacksRepository)
    {
        this.commandArguments = commandArguments;
        this.stickerPacksRepository = stickerPacksRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var stickerPack = await stickerPacksRepository.GetStickerPackAsync(commandArguments.StickerPackId, true)
            .ConfigureAwait(false);
        var stickers = stickerPack.Stickers ?? new List<Sticker>();

        var pageFrom = commandArguments.PageFrom;

        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var startIndex = commandArguments.Direction == PageChangeDirection.Increase
            ? (pageTo - 1) * commandArguments.PacksOnPage
            : (pageFrom - 2) * commandArguments.PacksOnPage;
        var endIndex = commandArguments.Direction == PageChangeDirection.Increase
            ? commandArguments.PacksOnPage * (pageFrom + 1)
            : pageTo * commandArguments.PacksOnPage;

        var buttons = new List<InlineKeyboardButtonDto>();
        for (var i = startIndex; i < stickers.Count && i < endIndex; i++)
        {
            var buttonCallback = $"SA:SendSticker:{stickers[i].StickerFullId.StickerId}";
            buttons.Add(new InlineKeyboardButtonDto(stickers[i].Name!, buttonCallback));
        }

        var lastLineButtons = new List<InlineKeyboardButtonDto>();
        if (pageTo > 1)
        {
            var buttonCallback = $"SA:SwKbdSt:{commandArguments.StickerPackId}:{pageTo}:Decrease:10";
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25c0\ufe0f", buttonCallback));
        }

        lastLineButtons.Add(new InlineKeyboardButtonDto($"{pageTo}", $"page:{pageTo}"));

        if (pageTo <= stickers.Count / commandArguments.PacksOnPage)
        {
            var buttonCallback = $"SA:SwKbdSt:{commandArguments.StickerPackId}:{pageTo}:Increase:10";
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25b6\ufe0f", buttonCallback));
        }
        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new ShowAllSwitchKeyboardStickersResult(commandArguments.ChatId, keyboard,
            commandArguments.BotMessageId);
    }
}