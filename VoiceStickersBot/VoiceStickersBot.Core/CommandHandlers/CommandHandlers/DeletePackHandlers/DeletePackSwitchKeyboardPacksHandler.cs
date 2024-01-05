using VoiceStickersBot.Core.CommandArguments;
using VoiceStickersBot.Core.CommandArguments.DeletePackCommandArguments;
using VoiceStickersBot.Core.CommandResults;
using VoiceStickersBot.Core.CommandResults.DeletePackResults;
using VoiceStickersBot.Core.Repositories.UsersRepository;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers.DeletePackHandlers;

public class DeletePackSwitchKeyboardPacksHandler : ICommandHandler
{
    public CommandType CommandType => CommandType.DeletePack;

    private DeletePackSwitchKeyboardPacksArguments commandArguments;
    private IUsersRepository usersRepository;

    public DeletePackSwitchKeyboardPacksHandler(
        DeletePackSwitchKeyboardPacksArguments commandArguments,
        IUsersRepository usersRepository)
    {
        this.commandArguments = commandArguments;
        this.usersRepository = usersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        var chatId = commandArguments.ChatId;
        
        var (result, packs) = await usersRepository
            .TryGetStickerPacksOwned(chatId.ToString(), false)
            .ConfigureAwait(false);

        if (!result)
            //TODO: поцы поправте этот кал, я хз че там должно быть (случай когда у юзера нет паков)
            return new DeletePackSwitchKeyboardPacksResult(
                chatId,
                new InlineKeyboardDto(
                    new List<InlineKeyboardButtonDto>(),
                    new List<InlineKeyboardButtonDto>()),
                commandArguments.BotMessageId);
        
        var pageFrom = commandArguments.PageFrom;
        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var countOnPage = commandArguments.PacksOnPage;

        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardPacks(
            "DP:Confirm",
            "",
            packs!,
            pageFrom,
            pageTo,
            countOnPage);

        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "DP:SwKbdPc",
            chatId.ToString(),
            pageTo,
            countOnPage,
            packs!.Count);

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new DeletePackSwitchKeyboardPacksResult(chatId, keyboard, commandArguments.BotMessageId);
    }
}