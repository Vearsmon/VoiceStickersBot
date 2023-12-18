using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class SwitchKeyboardHandler : ICommandHandler
{
    private readonly SwitchKeyboardCommand command;

    //Этого тоже не должно быть и все достается из команды
    private readonly string[] packs =
    {
        "Набор 1", "Набор 2", "Набор 3", "Набор 4", "Набор 5", "Набор 6", "Набор 7", "Набор 8", "Набор 9",
        "Набор 10",
        "Набор 11", "Набор 12", "Набор 13", "Набор 14", "Набор 15", "Набор 16", "Набор 17", "Набор 18", "Набор 19",
        "Набор 20",
        "Набор 21", "Набор 22", "Набор 23", "Набор 24", "Набор 25", "Набор 26", "Набор 27"
    };
    public Type CommandType => typeof(SwitchKeyboardCommand);
    
    public SwitchKeyboardHandler(SwitchKeyboardCommand command)
    {
        this.command = command;
    }

    public ICommandResult Handle()
    {
        if (!command.PageFrom.HasValue) 
            throw new ArgumentException($"{nameof(command.PageFrom)} была null, ожидалось число");

        var pageFrom = command.PageFrom.Value;
        
        var pageTo = command.PageChangeType == PageChangeType.Increase ? pageFrom + 1 : pageFrom - 1;
        var startIndex = command.PageChangeType == PageChangeType.Increase
            ? (pageTo - 1) * command.StickersOnPage
            : (pageFrom - 2) * command.StickersOnPage;
        var endIndex = command.PageChangeType == PageChangeType.Increase
            ? command.StickersOnPage * (pageFrom + 1)
            : pageTo * command.StickersOnPage;

        var buttons = new List<InlineKeyboardButtonDto>();
        for (var i = startIndex; i < packs.Length && i < endIndex; i++)
            buttons.Add(new InlineKeyboardButtonDto(packs[i], "open_id:{pack/sticker_id}")); 
        //pack-sticker id - то что лежит в элементе command.EnumerableEntity = List<Entity>, где entity - какаято
        //сущность общая для паков и стикеров, содержащая имя и id (пока что так)

        var lastLineButtons = new List<InlineKeyboardButtonDto>();
        if (pageTo > 1)
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25c0\ufe0f", $"pageleft:{pageTo}"));

        lastLineButtons.Add(new InlineKeyboardButtonDto($"{pageTo}", $"page:{pageTo}"));

        if (pageTo <= packs.Length / command.StickersOnPage)
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25b6\ufe0f", $"pageright:{pageTo}"));

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);
        
        return new SwitchKeyboardResult(keyboard, command.RequestContext.UserBotState, command.KeyboardCapture);
    }
}