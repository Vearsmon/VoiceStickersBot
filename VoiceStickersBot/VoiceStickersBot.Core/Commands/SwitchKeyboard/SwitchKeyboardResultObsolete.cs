namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardResultObsolete : ICommandResultObsolete
{
    public string KeyboardCapture { get; }
    public InlineKeyboardDto InlineKeyboardDto { get; }

    public SwitchKeyboardResultObsolete(InlineKeyboardDto inlineKeyboardDto, string keyboardCapture = "")
    {
        InlineKeyboardDto = inlineKeyboardDto;
        KeyboardCapture = keyboardCapture;
    }
}