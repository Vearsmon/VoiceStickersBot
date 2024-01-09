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
            .TryGetStickerPacks(chatId.ToString(), false)
            .ConfigureAwait(false);

        if (!result)
            return new DeletePackSwitchKeyboardPacksResult(
                chatId,
                new InlineKeyboardDto(
                    new List<List<InlineKeyboardButtonDto>>(),
                    new List<InlineKeyboardButtonDto>()),
                result,
                commandArguments.BotMessageId);
        
        var pageFrom = commandArguments.PageFrom;
        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var countOnPage = commandArguments.PacksOnPage;

        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardPacks(
            "DP:SwKbdSt",
            ":0:Increase:10",
            packs,
            pageFrom,
            pageTo,
            countOnPage);

        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "DP:SwKbdPc",
            "",
            pageTo,
            countOnPage,
            packs.Count);

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new DeletePackSwitchKeyboardPacksResult(chatId, keyboard, result, commandArguments.BotMessageId);
    }
}