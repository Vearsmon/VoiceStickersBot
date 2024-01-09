using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.SharePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.SharePackResults;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.SharePackHandlers;

public class SharePackSwitchKeyboardPacksHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.SharePack;

    private readonly SharePackSwitchKeyboardPacksArguments commandArguments;
    private readonly IUsersRepository usersRepository;

    public SharePackSwitchKeyboardPacksHandler(
        SharePackSwitchKeyboardPacksArguments commandArguments,
        IUsersRepository usersRepository)
    {
        this.commandArguments = commandArguments;
        this.usersRepository = usersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var chatId = commandArguments.ChatId;

        var (result, packs) = await usersRepository
            .TryGetStickerPacks(chatId.ToString(), false)
            .ConfigureAwait(false);

        if (!result)
            return new SharePackSwitchKeyboardPacksResult(
                chatId,
                new InlineKeyboardDto(new List<List<InlineKeyboardButtonDto>>(), new List<InlineKeyboardButtonDto>()),
                result,
                commandArguments.BotMessageId);

        var pageFrom = commandArguments.PageFrom;
        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var countOnPage = commandArguments.PacksOnPage;

        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardPacks(
            "SP:SwKbdSt",
            ":0:Increase:10",
            packs!,
            pageFrom,
            pageTo,
            countOnPage);

        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "SP:SwKbdPc",
            "",
            pageTo,
            countOnPage,
            packs!.Count);

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new SharePackSwitchKeyboardPacksResult(chatId, keyboard, result, commandArguments.BotMessageId);
    }
}