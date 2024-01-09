using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.AddStickerCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.AddStickerResults;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.AddStickerHandlers;

public class AddStickerSwitchKeyboardPacksHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.AddSticker;

    private readonly AddStickerSwitchKeyboardPacksArguments commandArguments;
    private readonly IUsersRepository usersRepository;

    public AddStickerSwitchKeyboardPacksHandler(
        AddStickerSwitchKeyboardPacksArguments commandArguments,
        IUsersRepository usersRepository)
    {
        this.commandArguments = commandArguments;
        this.usersRepository = usersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var chatId = commandArguments.ChatId;

        var packs = await usersRepository
            .GetStickerPacksOwned(chatId.ToString(), false)
            .ConfigureAwait(false);

        var hasPacks = packs.Count != 0;
        if (packs.Count == 0)
            return new AddStickerSwitchKeyboardPacksResult(
                chatId,
                new InlineKeyboardDto(
                    new List<List<InlineKeyboardButtonDto>>(),
                    new List<InlineKeyboardButtonDto>()),
                hasPacks,
                commandArguments.BotMessageId);

        var pageFrom = commandArguments.PageFrom;
        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var countOnPage = commandArguments.PacksOnPage;

        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardPacks(
            "AS:SwKbdSt",
            ":0:Increase:10",
            packs,
            pageFrom,
            pageTo,
            countOnPage);

        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "AS:SwKbdPc",
            "",
            pageTo,
            countOnPage,
            packs!.Count);

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new AddStickerSwitchKeyboardPacksResult(
            chatId,
            keyboard,
            hasPacks,
            commandArguments.BotMessageId);
    }
}