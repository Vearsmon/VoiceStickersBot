using VoiceStickersBot.Core.Commands;
using VoiceStickersBot.Core.Commands.SwitchKeyboard;

namespace VoiceStickersBot.Core.CommandHandlers.CommandHandlers;

public class SwitchKeyboardHandler : ICommandHandler
{
    private readonly SwitchKeyboardCommand command;

    private readonly string[] packs =
    {
        "Набор 1", "Набор 2", "Набор 3", "Набор 4", "Набор 5", "Набор 6", "Набор 7", "Набор 8", "Набор 9",
        "Набор 10",
        "Набор 11", "Набор 12", "Набор 13", "Набор 14", "Набор 15", "Набор 16", "Набор 17", "Набор 18", "Набор 19",
        "Набор 20",
        "Набор 21", "Набор 22", "Набор 23", "Набор 24", "Набор 25", "Набор 26", "Набор 27"
    };

    public SwitchKeyboardHandler(SwitchKeyboardCommand command)
    {
        this.command = command;
    }

    public Type CommandType => typeof(SwitchKeyboardCommand);

    public ICommandResult Handle()
    {
        var pageTo = command.PageChangeType == PageChangeType.Increase ? command.PageFrom + 1 : command.PageFrom - 1;
        var startIndex = command.PageChangeType == PageChangeType.Increase
            ? (pageTo - 1) * command.StickersOnPage
            : (command.PageFrom - 2) * command.StickersOnPage;
        var endIndex = command.PageChangeType == PageChangeType.Increase
            ? command.StickersOnPage * (command.PageFrom + 1)
            : pageTo * command.StickersOnPage;

        var buttons = new List<InlineKeyboardButtonDto>();
        for (var i = startIndex; i < packs.Length && i < endIndex; i++)
            buttons.Add(new InlineKeyboardButtonDto(packs[i], "pack_id"));

        var lastLineButtons = new List<InlineKeyboardButtonDto>();
        if (pageTo > 1)
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25c0\ufe0f", $"pageleft:{pageTo}"));

        lastLineButtons.Add(new InlineKeyboardButtonDto($"{pageTo}", "pagenum"));

        if (pageTo <= packs.Length / command.StickersOnPage)
            lastLineButtons.Add(new InlineKeyboardButtonDto("\u25b6\ufe0f", $"pageright:{pageTo}"));

        var keyboard = new InlineKeyboardDto(buttons, lastLineButtons);

        return new SwitchKeyboardResult(keyboard);
    }
}