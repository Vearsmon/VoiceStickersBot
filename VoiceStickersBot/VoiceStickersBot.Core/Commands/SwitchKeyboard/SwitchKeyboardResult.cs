using System.Runtime.InteropServices.JavaScript;

namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardResult : ICommandResult
{
    public string KeyboardCapture { get; }
    public InlineKeyboardDto InlineKeyboardDto { get; }
    public UserBotState UserBotStateFrom { get; }

    public SwitchKeyboardResult(InlineKeyboardDto inlineKeyboardDto, UserBotState userBotState, string keyboardCapture = "")
    {
        InlineKeyboardDto = inlineKeyboardDto;
        UserBotStateFrom = userBotState;
        KeyboardCapture = keyboardCapture;
    }
}