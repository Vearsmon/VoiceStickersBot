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
    private readonly UsersRepository usersRepository;

    public ShowAllSwitchKeyboardPacksCommandHandler(ShowAllSwitchKeyboardPacksCommandArguments commandArguments,
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
            //TODO: поцы поправте этот кал, я хз че там должно быть
            return new ShowAllSwitchKeyboardPacksResult(
                chatId,
                new InlineKeyboardDto(new List<InlineKeyboardButtonDto>(), new List<InlineKeyboardButtonDto>()),
                commandArguments.BotMessageId);

        var pageFrom = commandArguments.PageFrom;

        var pageTo = commandArguments.Direction == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var startIndex = commandArguments.Direction == PageChangeDirection.Increase
            ? (pageTo - 1) * commandArguments.PacksOnPage
            : (pageFrom - 2) * commandArguments.PacksOnPage;
        var endIndex = commandArguments.Direction == PageChangeDirection.Increase
            ? commandArguments.PacksOnPage * (pageFrom + 1)
            : pageTo * commandArguments.PacksOnPage;

        var buttons = new List<InlineKeyboardButtonDto>();
        for (var i = startIndex; i < packs.Count && i < endIndex; i++)
        {
            var buttonCallback = $"SA:SwKbdSt:{packs[i].Id}:0:Increase:10";
            buttons.Add(new InlineKeyboardButtonDto(packs[i].Name!, buttonCallback));
        }

        var lastLineButtons = new List<InlineKeyboardButtonDto>();
        if (pageTo > 1)
        {
            var buttonCallback = $"SA:SwKbdPc:{chatId}:{pageTo}:Decrease:10";
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25c0\ufe0f", buttonCallback));
        }

        lastLineButtons.Add(new InlineKeyboardButtonDto($"{pageTo}", $"page:{pageTo}"));

        if (pageTo <= packs.Count / commandArguments.PacksOnPage)
        {
            var buttonCallback = $"SA:SwKbdPc:{chatId}:{pageTo}:Increase:10";
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25b6\ufe0f", buttonCallback));
        }

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new ShowAllSwitchKeyboardPacksResult(chatId, keyboard, commandArguments.BotMessageId);
    }
}