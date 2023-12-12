using System.Runtime.InteropServices.JavaScript;

namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardResult : ICommandResult
{
    public SwitchKeyboardResult(InlineKeyboardDto inlineKeyboardDto)
    {
        InlineKeyboardDto = inlineKeyboardDto;
        EnsureSuccess = true;
    }

    public InlineKeyboardDto InlineKeyboardDto { get; }
    public bool EnsureSuccess { get; set; }
}