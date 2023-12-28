/*using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlersObsolete.CommandHandlers;

public class SwitchKeyboardHandler : ICommandHandler
{
    private readonly SwitchKeyboardCommandObsolete commandObsolete;

    //Этого тоже не должно быть и все достается из команды
    private readonly string[] packs =
    {
        "Набор 1", "Набор 2", "Набор 3", "Набор 4", "Набор 5", "Набор 6", "Набор 7", "Набор 8", "Набор 9",
        "Набор 10",
        "Набор 11", "Набор 12", "Набор 13", "Набор 14", "Набор 15", "Набор 16", "Набор 17", "Набор 18", "Набор 19",
        "Набор 20",
        "Набор 21", "Набор 22", "Набор 23", "Набор 24", "Набор 25", "Набор 26", "Набор 27"
    };
    public Type CommandType => typeof(SwitchKeyboardCommandObsolete);
    
    public SwitchKeyboardHandler(SwitchKeyboardCommandObsolete commandObsolete)
    {
        this.commandObsolete = commandObsolete;
    }

    public ICommandResultObsoleteObsolete Handle()
    {
        if (!commandObsolete.PageFrom.HasValue) 
            throw new ArgumentException($"{nameof(commandObsolete.PageFrom)} была null, ожидалось число");

        var pageFrom = commandObsolete.PageFrom.Value;
        
        var pageTo = commandObsolete.PageChangeDirection == PageChangeDirection.Increase ? pageFrom + 1 : pageFrom - 1;
        var startIndex = commandObsolete.PageChangeDirection == PageChangeDirection.Increase
            ? (pageTo - 1) * commandObsolete.StickersOnPage
            : (pageFrom - 2) * commandObsolete.StickersOnPage;
        var endIndex = commandObsolete.PageChangeDirection == PageChangeDirection.Increase
            ? commandObsolete.StickersOnPage * (pageFrom + 1)
            : pageTo * commandObsolete.StickersOnPage;

        var buttons = new List<InlineKeyboardButtonDto>();
        for (var i = startIndex; i < packs.Length && i < endIndex; i++)
            buttons.Add(new InlineKeyboardButtonDto(packs[i], "open_id:{pack/sticker_id}")); 
        //pack-sticker id - то что лежит в элементе command.EnumerableEntity = List<Entity>, где entity - какаято
        //сущность общая для паков и стикеров, содержащая имя и id (пока что так)

        var lastLineButtons = new List<InlineKeyboardButtonDto>();
        if (pageTo > 1)
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25c0\ufe0f", $"pageleft:{pageTo}"));

        lastLineButtons.Add(new InlineKeyboardButtonDto($"{pageTo}", $"page:{pageTo}"));

        if (pageTo <= packs.Length / commandObsolete.StickersOnPage)
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25b6\ufe0f", $"pageright:{pageTo}"));

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);
        
        return new SwitchKeyboardResultObsoleteObsolete(keyboard, commandObsolete.RequestContext.UserBotState, commandObsolete.KeyboardCapture);
    }
}*/