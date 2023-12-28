namespace VoiceStickersBot.Core.Commands.SwitchKeyboard;

public class SwitchKeyboardResultObsoleteObsolete : ICommandResultObsoleteObsolete
{
    public string KeyboardCapture { get; }
    public InlineKeyboardDto InlineKeyboardDto { get; }

    public SwitchKeyboardResultObsoleteObsolete(InlineKeyboardDto inlineKeyboardDto, string keyboardCapture = "")
    {
        InlineKeyboardDto = inlineKeyboardDto;
        KeyboardCapture = keyboardCapture;
    }
}