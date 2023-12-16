using System.Runtime.InteropServices.JavaScript;

namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardResult : ICommandResult
{
    public string KeyboardCapture { get; }
    public InlineKeyboardDto InlineKeyboardDto { get; }
    public bool EnsureSuccess { get; set; }
    
    public SwitchKeyboardResult(InlineKeyboardDto inlineKeyboardDto, string keyboardCapture="")
    {
        InlineKeyboardDto = inlineKeyboardDto;
        KeyboardCapture = keyboardCapture;
        EnsureSuccess = true;
    }
}