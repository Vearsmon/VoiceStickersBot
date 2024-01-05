using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.ShowAllCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.ShowAllResults;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.ShowAllHandlers;

public class ShowAllSwitchKeyboardPacksCommandHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.ShowAll;

    private readonly ShowAllSwitchKeyboardPacksCommandArguments commandArguments;
    private readonly IUsersRepository usersRepository;

    public ShowAllSwitchKeyboardPacksCommandHandler(
        ShowAllSwitchKeyboardPacksCommandArguments commandArguments,
        IUsersRepository usersRepository)
    {
        this.commandArguments = commandArguments;
        this.usersRepository = usersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        //chatId==userId
        var chatId = commandArguments.ChatId;
        
        var (result, packs) = await usersRepository
            .TryGetStickerPacksOwned(chatId.ToString(), false)
            .ConfigureAwait(false);

        if (!result)
            //TODO: поцы поправте этот кал, я хз че там должно быть (случай когда у юзера нет паков)
            return new ShowAllSwitchKeyboardPacksResult(
                chatId,
                new InlineKeyboardDto(new List<InlineKeyboardButtonDto>(), new List<InlineKeyboardButtonDto>()),
                commandArguments.BotMessageId);

        var pageFrom = commandArguments.PageFrom;
        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var countOnPage = commandArguments.PacksOnPage;

        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardPacks(
            "SA:SwKbdSt",
            ":0:Increase:10",
            packs!,
            pageFrom,
            pageTo,
            countOnPage);

        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "SA:SwKbdPc",
            chatId.ToString(),
            pageTo,
            countOnPage,
            packs!.Count);

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new ShowAllSwitchKeyboardPacksResult(chatId, keyboard, commandArguments.BotMessageId);
    }
}