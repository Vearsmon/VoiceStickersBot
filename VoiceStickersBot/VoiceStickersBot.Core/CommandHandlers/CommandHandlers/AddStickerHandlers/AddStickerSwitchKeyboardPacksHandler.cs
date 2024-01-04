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
    private readonly UsersRepository usersRepository;

    public AddStickerSwitchKeyboardPacksHandler(
        AddStickerSwitchKeyboardPacksArguments commandArguments,
        UsersRepository usersRepository)
    {
        this.commandArguments = commandArguments;
        this.usersRepository = usersRepository;
    }

    public async Task<ICommandResult> Handle()
    {
        //chatId==userId
        var chatId = commandArguments.ChatId;
        
        var (result, packs) = await usersRepository
            .TryGetStickerPacksOwned(chatId.ToString())
            .ConfigureAwait(false);

        if (!result)
            //TODO: поцы поправте этот кал, я хз че там должно быть (случай когда у юзера нет паков)
            return new AddStickerSwitchKeyboardPacksResult(
                chatId,
                new InlineKeyboardDto(new List<InlineKeyboardButtonDto>(), new List<InlineKeyboardButtonDto>()),
                commandArguments.BotMessageId);

        var pageFrom = commandArguments.PageFrom;
        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var countOnPage = commandArguments.PacksOnPage;
        
        var callbackPrefix = "AS:SwKbdSt";
        var buttons = SwitchKeyboardExtensions.BuildMainKeyboardPacks(
            callbackPrefix,
            packs!,
            pageFrom,
            pageTo,
            countOnPage);
        
        var lastLineButtons = SwitchKeyboardExtensions.BuildLastLine(
            "AS:SwKbdPc",
            chatId.ToString(),
            pageTo,
            countOnPage,
            packs!.Count);

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new AddStickerSwitchKeyboardPacksResult(chatId, keyboard, commandArguments.BotMessageId);
    }
}