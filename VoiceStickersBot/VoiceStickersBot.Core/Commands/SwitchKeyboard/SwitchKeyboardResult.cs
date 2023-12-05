using System.Runtime.InteropServices.JavaScript;

namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardResult : ICommandResult
{
    public SwitchKeyboardResult(InlineKeyboardDto inlineKeyboardDto)
    {
        InlineKeyboardDto = inlineKeyboardDto;
        Error = new CommandError(42, "Bro smth wrong");
        EnsureSuccess = true;
    }

    public InlineKeyboardDto InlineKeyboardDto { get; }
    public bool EnsureSuccess { get; set; }
    public CommandError Error { get; set; }
}